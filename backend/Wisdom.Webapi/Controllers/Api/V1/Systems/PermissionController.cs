using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisdom.Webapi.Extensions;
using Wisdom.Webapi.Models.Request.Systems;
using Sugar.Enties;
using SqlSugar;
using Wisdom.Webapi.Extensions.DataBase;
using Wisdom.Webapi.ViewModels.Systems.Permission;
using Wisdom.Webapi.Entities.QueryModels.Permission;

namespace Wisdom.Webapi.Controllers.Api.V1.Systems
{
    [Route("api/v1/[controller]/[action]")]
    public class PermissionController : BaseController
    {
        [HttpPost]
        public IActionResult List(PermissionRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            var query = base.db.Queryable<sys_permissions,sys_menus>((a,b) => new JoinQueryInfos(JoinType.Left, a.MenuId == b.Id));
            if (!string.IsNullOrEmpty(payload.permissionName)) query = query.Where(a => a.Name.Contains(payload.permissionName));
            if (payload.menuId != null)
            {
                var menus = base.db.Queryable<sys_menus>().ToList();
                var ids = GetAllSonIds(menus, (int)payload.menuId);
                query = query.Where(a => ids.Contains(a.MenuId));
            }
            if (payload.type != null) query = query.Where(a => a.Type == payload.type);
            int totalCount = 0;
            var list = query.Select<PermissionJsonViewTree>("b.NavTitle as MenuName,a.*").ToPageList(payload.PageNo, payload.PageSize, ref totalCount);
            response.SetData(list, totalCount);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Edit(sys_permissions model)
        {
            base.db.Updateable<sys_permissions>(model).IgnoreColumns(x => new { x.CreatedTime, x.CreatedUserId }).ExecuteCommand();
            var response = ResponseModelFactory.CreateInstance;
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Create(sys_permissions model)
        {
            model.CreatedTime = DateTime.Now; model.CreatedUserId = base.CurrentUser.Id;
            base.db.Insertable<sys_permissions>(model).ExecuteCommand();
            var response = ResponseModelFactory.CreateInstance;
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Remove(string ids) => Ok(base.db.Delete(ids, x => db.RemovePermission(x)));

        [HttpGet]
        /// <summary>
        /// 角色-权限菜单树
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public IActionResult PermissionTree(int id)
        {
            var response = ResponseModelFactory.CreateInstance;
            var role = base.db.Queryable<sys_roles>().First(x => x.Id == id);
            if (role is null)
            {
                response.SetFailed("角色不存在");
                return Ok(response);
            }
            var menu = base.db.Queryable<sys_menus>().OrderBy(x => x.Sort)
                .Select(x => new PermissionMenuTree
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Title = x.NavTitle
                }).ToList();
            var sql = @$"SELECT P.Id,P.MenuId,P.Name,P.ActionCode,IFNULL(S.RoleId,'') AS RoleId,(CASE WHEN S.PermissionId IS NOT NULL THEN 1 ELSE 0 END) AS IsAssigned FROM sys_permissions AS P LEFT JOIN (SELECT * FROM sys_rolepermissionmapping AS RPM WHERE RPM.roleid = {id}) AS S ON S.permissionid= P.id";
            if (role.RoleName is "超级管理员")
            {
                sql = @"SELECT P.Id,P.MenuId,P.Name,P.ActionCode,'SUPERADM' AS RoleId,1 AS IsAssigned FROM sys_permissions AS P";
            }
            var permissionList = base.db.SqlQueryable<PermissionWithAssignProperty>(sql).ToList();
            var tree = BuildPermissionMenuTree(menu, permissionList, 0, role.RoleName is "超级管理员");
            response.SetData(new { tree, selectedPermissions = permissionList.Where(x => x.IsAssigned == 1).Select(x => x.Id) });

            return Ok(response);
        }

        [NonAction]
        public static List<int> GetAllSonIds(List<sys_menus> menus, int id)
        {
            var lookup = menus.ToLookup(x => x.ParentId);
            var res = new List<int>();
            Func<int, List<int>> build = null;
            build = pid =>
            {
                var ids = lookup[pid].Select(x => x.Id).ToList();
                foreach (var id in ids)
                {
                    res.Add(id);
                    res = res.Concat(GetAllSonIds(menus, id)).ToList();
                }
                return res;
            };
            return build(id);
        }

        [NonAction]
        /// <summary>
        /// 构建权限菜单树
        /// </summary>
        /// <param name="menus">菜单集合</param>
        /// <param name="permissions">权限集合</param>
        /// <param name="pid">父级菜单Id</param>
        /// <param name="isSuperAdministrator">是否为超级管理员角色</param>
        /// <returns></returns>
        public static List<PermissionMenuTree> BuildPermissionMenuTree(List<PermissionMenuTree> menus, List<PermissionWithAssignProperty> permissions, int? pid, bool isSuperAdministrator = false)
        {
            List<PermissionMenuTree> recursiveObjects = new List<PermissionMenuTree>();
            foreach (var item in menus.Where(x => x.ParentId == pid))
            {
                var cur_p = permissions.Where(x => x.MenuId == item.Id);
                var children = new PermissionMenuTree
                {
                    AllAssigned = isSuperAdministrator ? true : (cur_p.Count(x => x.IsAssigned == 0) == 0),
                    Expand = true,
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Permissions = cur_p.Select(x => new PermissionElement
                    {
                        Name = x.Name,
                        Id = x.Id,
                        IsAssignedToRole = isSuperAdministrator ? true : x.IsAssigned == 1
                    }).ToList(),

                    Title = item.Title,
                    Children = BuildPermissionMenuTree(menus, permissions, item.Id)
                };
                recursiveObjects.Add(children);
            }
            return recursiveObjects;
        }
    }
}

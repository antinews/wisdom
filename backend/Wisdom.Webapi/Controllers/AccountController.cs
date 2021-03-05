using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisdom.Webapi.Auth.AuthContext;
using Wisdom.Webapi.Extensions;
using Sugar.Enties;
using Wisdom.Webapi.Entities.Enum;
using Wisdom.Webapi.ViewModels.Systems.Mneu;
using static Wisdom.Webapi.Entities.Enum.CommonEnum;
using Wisdom.Webapi.Entities.QueryModels.Permission;

namespace Wisdom.Webapi.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class AccountController : BaseController
    {
        /// <summary>
        /// 用户相关信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Profile(string token)
        {
            var response = ResponseModelFactory.CreateInstance;
            var user = AuthContextService.CurrentUser;
            if (user is null)
            {
                response.SetFailed("未找到登录用户");
                return Ok(response);
            }
            string sql = @"SELECT M.`Name` AS MenuName,P.ActionCode AS PermissionActionCode FROM sys_permissions AS P JOIN sys_menus AS M ON P.MenuId = M.Id";
            if (user.RoleId != (int)UserType.SuperAdministrator) sql += $" JOIN sys_rolepermissionmapping AS R ON R.PermissionId=P.id WHERE R.RoleId = {user.RoleId}";
            var permissions = base.db.SqlQueryable<PermissionWithMenu>(sql).ToList();
            var pagePermissions = permissions.GroupBy(x => x.MenuName).ToDictionary(g => g.Key, g => g.Select(x=>x.PermissionActionCode));
            response.SetData(new
            {
                avatar = "",
                username = user.TrueName,
                usertype = user.RoleId,
                permissions = pagePermissions,
                userid = user.Id
            });
            return Ok(response);
        }

        /// <summary>
        /// 为路由提供的菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Menu()
        {
            var response = ResponseModelFactory.CreateInstance;
            var sql = "SELECT * FROM sys_menus";
            if (!AuthContextService.IsSupperAdministator)
            {
                sql = @$"SELECT M.* FROM sys_rolepermissionmapping AS RPM LEFT JOIN sys_permissions AS P ON P.Id = RPM.PermissionId
                        INNER JOIN sys_menus AS M ON M.Id = P.MenuId
                        WHERE P.Type={(int)PermissionType.Page} AND RPM.RoleId={base.CurrentUser.RoleId}";
            }
            var menuList = base.db.SqlQueryable<sys_menus>(sql).ToList();
            var nodes = base.db.Queryable<sys_menus>().Where(x => x.IsPage == 0).ToList();
            foreach (var root in nodes)
            {
                if (menuList.Exists(x => x.Id == root.Id)) continue;
                menuList.Add(root);
            }
            menuList = menuList.OrderBy(x => x.Sort).ThenBy(x => x.CreatedTime).ToList();
            response.SetData(BuildTree(menuList, 0));
            return Ok(response);
        }

        /// <summary>
        /// 构建树
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        [NonAction]
        private List<MenuItem> BuildTree(List<sys_menus> menus, int pid)
        {
            var lookup = menus.ToLookup(x => x.ParentId);
            List<MenuItem> Build(int pid)
            {
                return lookup[pid].Select(node => new MenuItem
                {
                    Component = node.Component ?? "Layout",
                    Name = node.Name,
                    Path = node.LinkUrl,
                    Hidden = node.IsVisible == 0,
                    Redirect = string.IsNullOrEmpty(node.Redirect)? null:node.Redirect,
                    Meta = new MenuMeta()
                    {
                        Icon = node.IconUrl,
                        Title = node.NavTitle,
                        Affix = node.IsAffix == 1,
                        Badge = node.Badge,
                        NoKeepAlive = node.NoKeepAlive == 1
                    },
                    Children = Build(node.Id),
                }).ToList();


            }
            return Build(pid);
        }

    }

}

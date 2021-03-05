using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisdom.Webapi.Extensions;
using Wisdom.Webapi.Models.Request.Systems;
using Sugar.Enties;
using Wisdom.Webapi.Extensions.DataBase;
using Wisdom.Webapi.ViewModels.Systems.Role;

namespace Wisdom.Webapi.Controllers.Api.V1.Systems
{
    [Route("api/v1/[controller]/[action]")]
    public class RoleController : BaseController
    {
        [HttpPost]
        public IActionResult List(RoleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            var query = base.db.Queryable<sys_roles>();
            if (!string.IsNullOrEmpty(payload.kw.Trim()))
            {
                query = query.Where(x => x.RoleName.Contains(payload.kw.Trim()));
            }
            int totalCount = 0;
            var list = query.OrderBy(x => x.CreatedTime).ToPageList(payload.PageNo, payload.PageSize, ref totalCount);
            var data = base.Mapper.Map<List<sys_roles>, List<RoleJsonModel>>(list);
            response.SetData(data, totalCount);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Edit(sys_roles model) {
            var response = ResponseModelFactory.CreateInstance;
            if (model.RoleName.Trim().Length < 1)
            {
                response.SetFailed("请输入角色名称");
                return Ok(response);
            }
            model.ModifyTime = DateTime.Now; model.ModifyUserId = base.CurrentUser.Id;
            base.db.Updateable<sys_roles>(model).IgnoreColumns(x => new { x.CreatedTime, x.CreatedUserId }).ExecuteCommand();
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Create(sys_roles model) {
            var response = ResponseModelFactory.CreateInstance;
            if (base.db.Queryable<sys_roles>().First(x => x.RoleName == model.RoleName)!=null)
            {
                response.SetFailed("角色已存在");
                return Ok(response);
            }
            model.CreatedTime = DateTime.Now;
            model.CreatedUserId = base.CurrentUser.Id;
            model.CreatedUser = base.CurrentUser.TrueName;
            base.db.Insertable<sys_roles>(model).ExecuteCommand();
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Remove(string ids) => Ok(base.db.Delete(ids, x => db.RemoveRole(x)));

        /// <summary>
        /// 为用户管理提供的角色列表数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RolesByUserMg()
        {
            var response = ResponseModelFactory.CreateInstance;
            var data = base.db.Queryable<sys_roles>().OrderBy(x => x.SortNum).Select(x => new { x.Id, x.RoleName }).ToList();
            response.SetData(data);
            return Ok(response);
        }
        /// <summary>
        /// 为角色分配权限
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AssignPerimission(RoleAssignPermissionPayload payload)
        {
            var response = ResponseModelFactory.CreateInstance;
            var role = base.db.Queryable<sys_roles>().First(x => x.Id == payload.Id);
            if (role is null)
            {
                response.SetFailed("角色不存在");
                return Ok(response);
            }
            if (role.RoleName is "超级管理员") return Ok(response);
            response = base.db.UseTransaction(() =>
            {
                base.db.Deleteable<sys_rolepermissionmapping>(x => x.RoleId == role.Id).ExecuteCommand(); //删除现有权限
                if (payload.Permissions != null || payload.Permissions.Count > 0)
                {
                    var permissions = payload.Permissions.Select(x => new sys_rolepermissionmapping
                    {
                        CreatedTime = DateTime.Now,
                        CreatedUserId = base.CurrentUser.Id,
                        PermissionId = x,
                        RoleId = role.Id
                    }).ToList();
                    base.db.Insertable<sys_rolepermissionmapping>(permissions).ExecuteCommand();
                }
            });
            return Ok(response);
        }
        
    }
}

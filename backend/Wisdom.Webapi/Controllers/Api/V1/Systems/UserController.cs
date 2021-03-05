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
using Wisdom.Webapi.ViewModels.Systems.User;

namespace Wisdom.Webapi.Controllers.Api.V1.Systems
{
    [Route("api/v1/[controller]/[action]")]
    public class UserController : BaseController
    {
        [HttpPost]
        public IActionResult List(UserRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            var query = base.db.Queryable<sys_users,sys_departments>((a,b) => new JoinQueryInfos(JoinType.Left, a.DepartmentId == b.Id));
            if (!string.IsNullOrEmpty(payload.Name)) query = query.Where(a => a.TrueName.Contains(payload.Name));
            if (!string.IsNullOrEmpty(payload.Depart)) query = query.Where((a, b) => b.DepartmentName.Contains(payload.Depart));
            int totalCount = 0;
            var list = query.Select<UserJsonModel>("b.DepartmentName as DepartName,a.*").ToPageList(payload.PageNo, payload.PageSize, ref totalCount);
            response.SetData(list, totalCount);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Edit(sys_users model)
        {
            model.ModifyTime = DateTime.Now;
            model.ModifyUserId = base.CurrentUser.Id;
            base.db.Updateable<sys_users>(model).IgnoreColumns(x => new { x.CreatedTime, x.CreatedUserId }).ExecuteCommand();
            var response = ResponseModelFactory.CreateInstance;
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Create(sys_users model)
        {
            var response = ResponseModelFactory.CreateInstance;
            var query = base.db.Queryable<sys_users>();
            if (query.Where(x => x.UserName == model.UserName).Count() > 0)
            {
                response.SetFailedButOK("该用户名已被注册");
                return Ok(response);
            }
            // 用户名默认人员姓名全拼，密码是用户名加123
            model.UserName = model.UserName??model.TrueName.ToPinying();
            model.Password = model.Password??(model.UserName + "123").Md5Encrypt();
            model.CreatedTime = DateTime.Now; 
            model.CreatedUserId = base.CurrentUser.Id;
            base.db.Insertable<sys_users>(model).ExecuteCommand();
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Remove(string ids) => Ok(base.db.Delete(ids, x => db.RemoveUser(x)));

        [HttpGet]
        public IActionResult GenerateUserNameByTrueName(string trueName)
        {
            var response = ResponseModelFactory.CreateInstance;
            string username = trueName.ToPinying();
            response.SetData(username);
            return Ok(response);
        }

    }
}

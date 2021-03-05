using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sugar.Enties;
using Wisdom.Webapi.Extensions;

namespace Wisdom.Webapi.Controllers.Api.V1
{
    [Route("api/v1/[controller]/[action]")]
    public class CommonController : BaseController
    {
        /// <summary>
        /// 根据字点keycode获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDicsByKey(string key)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            var entity = base.db.Queryable<sys_diccategorys>().First(x => x.Code == key);
            if (entity is null) response.SetFailed($"键code：{key}不存在！");
            else
            {
                var query = base.db.Queryable<sys_dics>().Where(x => x.CategoryId == entity.Id);
                response.SetData(query.ToList());
            }
            return Ok(response);
        }

    }
}

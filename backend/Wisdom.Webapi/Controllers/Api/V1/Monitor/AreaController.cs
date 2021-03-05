using Entitys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wisdom.Webapi.Entities.Monitor;
using Wisdom.Webapi.Extensions;

namespace Wisdom.Webapi.Controllers.Api.V1.Monitor
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AreaController : BaseController
    {
        [HttpPost]
        public IActionResult List()
        {
            var response = ResponseModelFactory.CreateResultInstance;
            var list = base.db.Queryable<base_cityarea>().OrderBy(item=>item.OrderNum).ToList();
            response.SetData(list);
            return Ok(response);
        }
    }
}

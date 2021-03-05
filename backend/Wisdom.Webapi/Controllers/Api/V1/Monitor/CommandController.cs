using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edge.WebApi.Entity.Monitor;
using Edge.WebApi.Entity.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edge.WebApi.Controllers.Monitor
{
    [Route("api/Monitor/[controller]/[action]")]
    [ApiController]
    public class CommandController : Monitor_BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public CommandController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public class CommandPageRequest : PageRequest
        {
            /// <summary>
            /// 协议Id，不过滤请传入0
            /// </summary>
            public int CodecsId { get; set; }
            /// <summary>
            /// 搜索关键字moduleId模糊搜索
            /// </summary>
            public new string Kw { get; set; }
        }
        /// <summary>	
        /// 获取指令列表
        /// </summary>	
        /// <param name="payload">请求参数</param>	
        /// <returns></returns>	
        [HttpPost]
        public IActionResult List(CommandPageRequest payload)
        {
            var response = new PageResponse();
            int total = 0;
            //List<int> structIds = FindStruct();
            var list = db.Queryable<Monitor_Command>();//.Where(mo => structIds.Contains(mo.Id));

            if (payload.CodecsId != 0)
            {
                list = list.Where(x => x.CodecsId == payload.CodecsId);
            }
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                list = list.Where(x => x.ModuleId.Contains(payload.Kw));
            }
            var data = list.OrderBy(it => it.Id).ToPageList(payload.CurrentPage, payload.PageSize, ref total);
            response.SetData(data);
            response.TotalCount = total;
            return Ok(response);
        }

        /// <summary>	
        /// 新增指令
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(Monitor_Command model)
        {
            response.SetData(InsertableReturnIdentity(model));
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 获取单个指令详情
        /// </summary>	
        /// <param name="id">惟一编码</param>	
        /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int id)
        {
            var model = Single<Monitor_Command>(s => s.CodecsId == id);
            response.SetData(model);
            return Ok(response);
        }

        /// <summary>	
        /// 修改指令，务必先获取单个信息
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(Monitor_Command model)
        {
            response.SetData(Updateable(model));
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 删除指令
        /// </summary>	
        /// <param name="ids">角色ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            response.SetData(Deleteable<Monitor_Command>(s => id.Contains(s.CodecsId)));
            response.SetSuccess();
            return Ok(response);
        }
    }
}
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
    public class CodecsController : Monitor_BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public CodecsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public class CodecsPageRequest : PageRequest
        {
        }
        /// <summary>	
        /// 获取协议列表
        /// </summary>	
        /// <param name="payload">请求参数</param>	
        /// <returns></returns>	
        [HttpPost]
        public IActionResult List(CodecsPageRequest payload)
        {
            var response = new PageResponse();
            int total = 0;
            //List<int> structIds = FindStruct();
            var list = db.Queryable<Monitor_Codecs>();//.Where(mo => structIds.Contains(mo.Id));

            if (!string.IsNullOrEmpty(payload.Kw))
            {
                list = list.Where(x => x.CodecsName.Contains(payload.Kw));
            }
            var data = list.OrderBy(it => it.CodecsId).ToPageList(payload.CurrentPage, payload.PageSize, ref total);
            response.SetData(data);
            response.TotalCount = total;
            return Ok(response);
        }

        /// <summary>	
        /// 新增协议
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(Monitor_Codecs model)
        {
            response.SetData(Insertable(model));
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 获取单个协议详情
        /// </summary>	
        /// <param name="id">惟一编码</param>	
        /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int id)
        {
            var model = Single<Monitor_Codecs>(s => s.CodecsId == id);
            response.SetData(model);
            return Ok(response);
        }

        /// <summary>	
        /// 修改协议，务必先获取单个信息
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(Monitor_Codecs model)
        {
            response.SetData(Updateable(model));
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 删除协议
        /// </summary>	
        /// <param name="ids">角色ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            response.SetData(Deleteable<Monitor_Codecs>(s => id.Contains(s.CodecsId)));
            Deleteable<Monitor_Command>(s => id.Contains(s.CodecsId));//删所有所属指令
            response.SetSuccess();
            return Ok(response);
        }
    }
}
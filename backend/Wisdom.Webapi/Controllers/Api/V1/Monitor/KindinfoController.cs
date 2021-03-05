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
    public class KindinfoController : Monitor_BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public KindinfoController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public class KindinfoPageRequest : PageRequest
        {
            /// <summary>
            /// 结构物Id
            /// </summary>
            public int ProjectId { get; set; }
        }
        /// <summary>	
        /// 获取传感器类型列表
        /// </summary>	
        /// <param name="payload">请求参数</param>	
        /// <returns></returns>	
        [HttpPost]
        public IActionResult List(KindinfoPageRequest payload)
        {
            var response = new PageResponse();
            int total = 0;
            //List<int> structIds = FindStruct();
            var list = db.Queryable<Monitor_Kindinfo>();//.Where(mo => structIds.Contains(mo.Id));
            if (payload.ProjectId != 0)
            {
                list = list.Where(x => x.ProjectId==payload.ProjectId);
            }
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                list = list.Where(x => x.KindName.Contains(payload.Kw));
            }
            var data = list.OrderBy(it => it.Id).ToPageList(payload.CurrentPage, payload.PageSize, ref total);
            response.SetData(data);
            response.TotalCount = total;
            return Ok(response);
        }
        /// <summary>
        /// 获取传感器下含有测点的传感器数组
        /// </summary>
        /// <param name="pid">结构物Id</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult KindByIsActivePoint(int pid)
        {
            response.SetData(db.Queryable<Monitor_Kindinfo>().Where(w =>w.ProjectId== pid&& db.Queryable<Monitor_Points>().Where(z => z.IsActived == 1).Select(s=>s.MonitoringKindId).Distinct().ToList().Contains(w.Id)).ToList());
            return Ok(response);
        }

        /// <summary>	
        /// 新增传感器类型
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(Monitor_Kindinfo model)
        {
            model.TempId = 0;//表示自己新增到对应的结构物下的
            response.SetData(InsertableReturnIdentity(model));
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 获取单个传感器类型详情
        /// </summary>	
        /// <param name="id">惟一编码</param>	
        /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int id)
        {
            var model = Single<Monitor_Kindinfo>(s => s.Id == id);
            response.SetData(model);
            return Ok(response);
        }

        /// <summary>	
        /// 修改传感器类型，务必先获取单个信息
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(Monitor_Kindinfo model)
        {
            response.SetData(Updateable(model));
            response.SetSuccess();
            return Ok(response);
        }
        /// <summary>	
        /// 批量操作（测点）	
        /// </summary>	
        /// <param name="command"></param>	
        /// <param name="ids">ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            switch (command)
            {
                case "enable":
                    response.SetData(Updateable<Monitor_Kindinfo>(t => new Monitor_Kindinfo { IsActived = 1 }, w => id.Contains(w.Id)));
                    response.SetSuccess();
                    break;
                case "disable":
                    response.SetData(Updateable<Monitor_Kindinfo>(t => new Monitor_Kindinfo { IsActived = 0 }, w => id.Contains(w.Id)));
                    response.SetSuccess();
                    break;
                default:
                    break;
            }
            return Ok(response);
        }
        /// <summary>	
        /// 删除传感器类型
        /// </summary>	
        /// <param name="ids">角色ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            response.SetData(Deleteable<Monitor_Kindinfo>(s => id.Contains(s.Id)));
            response.SetSuccess();
            return Ok(response);
        }
    }
}
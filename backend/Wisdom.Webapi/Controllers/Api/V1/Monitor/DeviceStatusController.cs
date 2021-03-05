using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edge.WebApi.Entity.Monitor;
using Edge.WebApi.Entity.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Edge.WebApi.Controllers.Monitor
{
    /// <summary>
    /// 设备自诊断控制器
    /// </summary>
    [Route("api/Monitor/[controller]/[action]")]
    [ApiController]
    public class DeviceStatusController : Monitor_BaseController
    {

        private IHostingEnvironment _hostingEnvironment;
        public DeviceStatusController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// list参数类
        /// </summary>
        public class DeviceStatusPageRequest : PageRequest
        {
            /// <summary>
            /// 开始时间--上面的今天-7天-15天，也记得这样传：传入两个时间间断（标准时间格式，到秒）
            /// </summary>
            public DateTime? sTime { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime?  eTime { get; set; }
        }
        /// <summary>	
        /// 获取指令列表
        /// </summary>	
        /// <param name="payload">请求参数</param>	
        /// <returns></returns>	
        [HttpPost]
        public IActionResult List(DeviceStatusPageRequest payload)
        {
            var response = new PageResponse();
            int total = 0;
            //List<int> structIds = FindStruct();
            var list = db.Queryable<Monitor_DeviceStatus, Monitor_Devices>((mo, a) => new object[] {
            JoinType.Left,mo.DeviceId==a.Id
            }).Select<Monitor_DeviceStatusView>("mo.*,a.DeviceName DeviceName,concat(a.DeviceAddr,':',a.DevicePort) DeviceAddr");

            if (payload.sTime != null)
            {
                list = list.Where(mo => mo.ModiifyDate >= payload.sTime);
            }
            if (payload.eTime != null)
            {
                list = list.Where(mo => mo.ModiifyDate <= payload.eTime);
            }
            var data = list.OrderBy(mo => mo.Id,OrderByType.Desc).ToPageList(payload.CurrentPage, payload.PageSize, ref total);
            response.SetData(data);
            response.TotalCount = total;
            return Ok(response);
        }

        /// <summary>	
        /// 新增
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(Monitor_DeviceStatus model)
        {
            response.SetData(InsertableReturnIdentity(model));
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 获取单个详情
        /// </summary>	
        /// <param name="id">惟一编码</param>	
        /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int id)
        {
            var model = Single<Monitor_DeviceStatus>(s => s.Id == id);
            response.SetData(model);
            return Ok(response);
        }
        /// <summary>	
         /// 根据设备ID获取单个详情
         /// </summary>	
         /// <param name="id">惟一编码</param>	
         /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public object ByDeviceIdGet(int id)
        {
            var model = Single<Monitor_DeviceStatus>(s => s.DeviceId == id);
            return model;
        }

        /// <summary>	
        /// 修改，务必先获取单个信息
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(Monitor_DeviceStatus model)
        {
            response.SetData(Updateable(model));
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 删除
        /// </summary>	
        /// <param name="ids">ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            response.SetData(Deleteable<Monitor_DeviceStatus>(s => id.Contains(s.Id)));
            response.SetSuccess();
            return Ok(response);
        }
    }
}
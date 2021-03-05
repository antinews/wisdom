using System;
using System.Collections.Generic;
using System.IO;
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
    public class SchedulerTasksController : Monitor_BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public SchedulerTasksController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public class SchedulertasksPageRequest : PageRequest
        {

            /// <summary>
            /// 任务名称
            /// </summary>
            public new  string Kw { get; set; } = "";
            /// <summary>
            /// 任务说明：全选的就传入空字符串就行。非系统的，传入“系统生成”
            /// </summary>
            public string TaskDesc { get; set; } = "";
        }
        /// <summary>	
        /// 获取任务调度列表
        /// </summary>	
        /// <param name="payload">请求参数</param>	
        /// <returns></returns>	
        [HttpPost]
        public IActionResult List(SchedulertasksPageRequest payload)
        {
            var response = new PageResponse();
            int total = 0;
            //List<int> structIds = FindStruct();
            var list = db.Queryable<Monitor_Schedulertasks>().Select<Monitor_SchedulertasksView>();//.Where(mo => structIds.Contains(mo.Id));
            if (!string.IsNullOrEmpty(payload.TaskDesc))
            {
                list = list.Where(x => !x.TaskDesc.Contains(payload.TaskDesc));
            }
            if (!string.IsNullOrEmpty(payload.Kw))
            {
                list = list.Where(x => x.TaskName.Contains(payload.Kw));
            }
            var data = list.OrderBy(it => it.Id).ToPageList(payload.CurrentPage, payload.PageSize, ref total);
            foreach(var d in data)
            {
                if (!string.IsNullOrEmpty(d.ExecutePoint))
                {
                    List<int> pids = d.ExecutePoint.Split(",").Select(s => Convert.ToInt32(s)).ToList();
                    d.pointNames =string.Join(',',db.Queryable<Monitor_Points>().Where(w => pids.Contains(w.Id)).Select(s=>s.PointName).ToList());
                }
            }
            response.SetData(data);
            response.TotalCount = total;
            return Ok(response);
        }

        /// <summary>	
        /// 新增任务调度
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(Monitor_Schedulertasks model)
        {
            response.SetData(InsertableReturnIdentity(model));
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 获取单个任务调度	
        /// </summary>	
        /// <param name="id">惟一编码</param>	
        /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int id)
        {
            Monitor_Schedulertasks model = Single<Monitor_Schedulertasks>(s => s.Id == id);
            response.SetData(model);
            return Ok(response);
        }

        /// <summary>	
        ///编辑修改任务调度，务必先获取
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(Monitor_Schedulertasks model)
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
                    response.SetData(Updateable<Monitor_Schedulertasks>(t => new Monitor_Schedulertasks { IsActived = 1 }, w => id.Contains(w.Id)));
                    response.SetSuccess();
                    break;
                case "disable":
                    response.SetData(Updateable<Monitor_Schedulertasks>(t => new Monitor_Schedulertasks { IsActived = 0 }, w => id.Contains(w.Id)));
                    response.SetSuccess();
                    break;
                default:
                    break;
            }
            return Ok(response);
        }
        /// <summary>	
        /// 删除任务调度
        /// </summary>	
        /// <param name="ids">角色ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            response.SetData(Deleteable<Monitor_Schedulertasks>(s => id.Contains(s.Id)));
            response.SetSuccess();
            return Ok(response);
        }
        /// <summary>
        /// 文件上传（任务）
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadFile(IFormFileCollection files)
        {
            files = Request.Form.Files;
            if (files.Count == 0)
            {
                response.Message = "没有发现文件";
                response.Code = 403;
            }
            else
            {
                //long size = files.Sum(f => f.Length);
                var fileFolder = Path.Combine(_hostingEnvironment.ContentRootPath + "/wwwroot", "Uploads/Task");

                if (!Directory.Exists(fileFolder))
                    Directory.CreateDirectory(fileFolder);
                var file = files[0];
                if (file.Length > 0)
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                                   Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(fileFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    response.Data = new { url = "Uploads/Task/" + fileName, name = fileName };
                }
            }
            return Json(response);
        }
    }
}
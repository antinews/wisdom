using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Edge.WebApi.Entity.Monitor;
using Edge.WebApi.Entity.Web;
using Edge.WebApi.Entity.Yun;
using Entitys;
//using Maikebing.Data.Taos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NPOI.SS.Formula.Functions;
using SqlSugar;

namespace Edge.WebApi.Controllers.Monitor
{
    /// <summary>
    /// 结构物API
    /// </summary>
    [Route("api/Monitor/[controller]/[action]")]
    [ApiController]
    public class ProjectsController : Monitor_BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public IMemoryCache _cache;
        public ProjectsController(IHostingEnvironment hostingEnvironment, IMemoryCache memoryCache)
        {
            _hostingEnvironment = hostingEnvironment;
            _cache = memoryCache;
        }
        /// <summary>
        /// 列表获取参数(结构物)
        /// </summary>
        public class ProjectsPageRequest : PageRequest
        {
            public int Area {get ; set; }

            public int Type { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetAll()
        {
            var response = new PageResponse();
            var list = db.Queryable<base_buildbridge_main>().Select(item => new { id = item.KeyId, name = item.BridgeName }).ToList();
            response.SetData(list);
            return Ok(response);
        }

        /// <summary>	
        /// 列表分页获取
        /// </summary>	
        /// <param name="payload">请求参数</param>	
        /// <returns></returns>	
        [HttpPost]
        public IActionResult List(ProjectsPageRequest payload)
        {
            var response = new PageResponse();
            int total = 0;
            //List<int> structIds = FindStruct();
            var list = db.Queryable<base_buildbridge_main, base_ass_bridgetype>((mo, a) => new object[] {
            JoinType.Left,mo.BridgeType == a.BridgeTypeName
            }).Select<base_buildbridge_main>("mo.*"); ;//.Where(mo => structIds.Contains(mo.Id));

            if (!string.IsNullOrEmpty(payload.Kw))
            {
                list = list.Where(mo => mo.BridgeName.Contains(payload.Kw));
            }
            if (payload.Area > 0)
            {
                list = list.Where(mo => mo.CityAreaId == payload.Area);
            }
            if (payload.Type > 0)
            {
                list = list.Where(mo => mo.BridgeTypeId == payload.Type);
            }
            var data = list.OrderBy(mo => mo.CreatedTime).ToPageList(payload.CurrentPage, payload.PageSize, ref total);
            //foreach(var d in data)
            //{
            //    var v = ProjectTypes(d.Id);
            //    d.AbnormalPoints = v.Count();
            //    d.AbnormalNumber = v.Sum(s=>s.number);
            //}
            response.SetData(data);
            response.TotalCount = total;
            return Ok(response);
        }
        public class AlarmNumber
        {
            /// <summary>
            /// 测点
            /// </summary>
            public int pointId { get; set; }
            /// <summary>
            /// 测点报警数
            /// </summary>
            public int number { get; set; }
        }
        /// <summary>	
        /// 获取结构物下的报警记录数
        /// </summary>	
        /// <returns></returns>	
        [NonAction]
        public List<AlarmNumber> ProjectTypes(int pid)
        {
            var cdata = _cache.Get($"ProjectTypes-{pid}");
            if (cdata != null)
            {
                return cdata as List<AlarmNumber>;
            }

            List<AlarmNumber> nums = db.SqlQueryable<AlarmNumber>("SELECT pointId,count(*) number from monitor_dataalarm where ProjectId=95 and state=0 GROUP BY pointId").ToList();
            _cache.Set($"ProjectTypes-{pid}", nums, new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddHours(1) });
            return nums;
        }

        /// <summary>	
        /// 新增(结构物)
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(base_buildbridge_main model)
        {
            model.KeyId = 0;
            model.CreatedTime = DateTime.Now;
            model.CreatedUserId = CurrentUser.Id;
            model.KeyId = InsertableReturnIdentity(model);
            response.SetData(model.KeyId);
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 获取单个结构物信息
        /// </summary>	
        /// <param name="id">惟一编码</param>	
        /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int id)
        {
            var model = Single<Monitor_Projects>(s => s.Id == id);
            response.SetData(model);
            return Ok(response);
        }
        /// <summary>	
        /// 获取结构物类型数组
        /// </summary>	
        /// <returns></returns>	
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult ProjectTypes()
        {
            var list = db.Queryable<base_ass_bridgetype>().ToList();
            var data = list.GroupBy(item => item.BuildType)
                .Select(item => new {label = item.Key, value = item.Key,children = item.ToList().Select(it=>new { label=it.BridgeTypeName,value=it.KeyId}) });
            response.SetData(data);
            return Ok(response);
        }
        public class TempKindByProject
        {
            public int id { get; set; }
            public string text { get; set; }
            public int number { get; set; }
        }
        /// <summary>	
        /// 获取单个结构物各个传感器类型数量
        /// </summary>	
        /// <param name="id">惟一编码</param>	
        /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult KindByProject(int id)
        {
            var points = db.Queryable<Monitor_Points, Monitor_Kindinfo>((w, a) => new object[] {
            JoinType.Left,w.MonitoringKindId==a.Id
            }).Where(w => w.ProjectId == id && w.MonitoringKindId != null && w.MonitoringKindId > 0 && w.IsActived == 1).Select<Monitor_PointsView>("w.*,a.Alias Alias").ToList();
            List<TempKindByProject> result = new List<TempKindByProject>();
            foreach (var p in points)
            {
                if (result.FirstOrDefault(f => f.id == p.MonitoringKindId) == null)
                {
                    result.Add(new TempKindByProject() { id = p.MonitoringKindId, text = p.Alias, number = points.Where(w => w.MonitoringKindId == p.MonitoringKindId).Count() });
                }
            }
            response.SetData(result);
            return Ok(response);
        }
        /// <summary>	
        /// 修改单个结构物信息  切记：先获取，再修改，不管你改没改其他信息，都需要传回来
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(base_buildbridge_main model)
        {
            //var model2 = Single<Monitor_Projects>(s => s.Id == model.Id);

            response.SetData(Updateable(model));
            //if (model2.ProjectType != model.ProjectType)
            //{
            //    //传感器
            //    Deleteable<Monitor_Kindinfo>(s => s.ProjectId == model.Id && s.TempId == 0);//删数
            //    Insertable(db.Queryable<Monitor_KindinfoTemp>().Where(w => w.ProjectType == model.ProjectType).Select(s => new Monitor_Kindinfo { IsActived = 1, Alias = s.Alias, KindName = s.KindName, ProjectId = model.Id, Unit = s.Unit, Precision = s.Precision, MinValue = s.MinValue, MaxValue = s.MaxValue, TempId = s.Id, KindCode = s.KindCode }).ToList());
            //}
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 删除结构物	
        /// </summary>	
        /// <param name="ids">ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            //List<Monitor_Devices_Appsystem> structsyss = db.Queryable<Monitor_Devices_Appsystem>().Where(s => id.Contains(s.ProjectId)).ToList();
 
            //Deleteable<Monitor_Computeformulainfo>(s => db.Queryable<Monitor_Points>().Where(w => id.Contains(w.ProjectId)).Select(e => e.ComputeFormulaId).ToList().Contains(s.Id));//删测点
            //Deleteable<Monitor_Streamtasks>(s => db.Queryable<Monitor_Devices>().Where(w => id.Contains(w.ProjectId)).Select(se => se.Id).ToList().Contains(s.DeviceId));
            //Deleteable<Monitor_Schedulertasks>(s => db.Queryable<Monitor_Devices>().Where(w => id.Contains(w.ProjectId)).Select(se => se.Id).ToList().Contains(s.DeviceId));
            //Deleteable<Monitor_Devices_Appsystem>(s => id.Contains(s.ProjectId));//删数据库配置
            //Deleteable<Monitor_Kindinfo>(s => id.Contains(s.ProjectId));//删数传感器
            //Deleteable<Monitor_Points>(s => id.Contains(s.ProjectId));//删测点
            //Deleteable<Monitor_Devices>(s => id.Contains(s.ProjectId));//删模块
            response.SetData(Deleteable<base_buildbridge_main>(s => id.Contains(s.KeyId)));
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 批量操作（结构物）
        /// </summary>	
        /// <param name="command">enable启用，disable禁用</param>	
        /// <param name="ids">ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)
            ).ToList();
            switch (command.ToLower())
            {
                case "enable":
                    response.SetData(Updateable<Monitor_Projects>(t => new Monitor_Projects { IsActived = 1 }, w => id.Contains(w.Id)));
                    response.SetSuccess();
                    break;
                case "disable":
                    response.SetData(Updateable<Monitor_Projects>(t => new Monitor_Projects { IsActived = 0 }, w => id.Contains(w.Id)));
                    response.SetSuccess();
                    break;
                case "pull"://拉取
                    try
                    {
                        SqlSugarClient yundb = new SqlSugarClient(
                            new ConnectionConfig()
                            {
                                ConnectionString = Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("YunConnstring"),
                                DbType = DbType.SqlServer,//设置数据库类型
                                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                            });
                        foreach (int d in id)
                        {
                            Console.Write("connstring:" + yundb.Ado.Connection.ConnectionString);

                            var pro = db.Queryable<Monitor_Projects>().Where(w => w.Id == d).Single();

                            Console.Write("pro:" + db.Ado.Connection.ConnectionString);

                            var devices = db.Queryable<Monitor_Devices>().Where(w => w.ProjectId == d).ToList();
                            var localpoints = db.Queryable<Monitor_Points>().Where(w => w.ProjectId == d).ToList();
                            var localkinds = db.Queryable<Monitor_Kindinfo>().Where(w => w.ProjectId == d).ToList();
                            PointsController pcontr = new PointsController(_hostingEnvironment);
                            DevicesController dcontr = new DevicesController(_hostingEnvironment);


                            Console.Write("dcontr:");
                            #region//测站
                            var stations = yundb.Queryable<BsriMonitorStation, BsriStructure>((w, s) => new object[] {
                            JoinType.Left,w.StructureId==s.No
                            }).Where((w, s) => s.StructureName == pro.ProjectName).Select<BsriMonitorStationView>("w.*,s.StructureName StructureName").ToList();

                            Console.Write("stations:" + stations);
                            foreach (var s in stations)
                            {
                                var f = devices.FirstOrDefault(f => f.DeviceName == s.StationName);
                                if (f == null)//add
                                {
                                    var temp = new Monitor_Devices()
                                    {
                                        ProjectId = d,
                                        DeviceName = s.StationName,
                                        NetProtocol = s.DeviceStyle == 2 ? "UDP" : "TCP",
                                        DeviceAddr = s.DeviceAddress,
                                        DevicePort = s.DevicePort,
                                        ReportAddr = s.RemoteAddress,
                                        ReportPort = s.RemotePort,
                                        EdgeMiddleAddr = s.WebSocketAddress,
                                        EdgeMiddlePort = s.WebSocketPort,
                                        PointNums = s.GoodFieldNum ?? 0,
                                        CollectCommand = s.Command,
                                        CollectInterval = s.Interval ?? 0,
                                        IsActived = s.IsActived,
                                        CodecsId = s.SolveType ?? 0,
                                        LastCollectTime = DateTime.MinValue,
                                        ModuleId = s.ModuleID,
                                        YunId = s.ID,
                                        YunTableName = s.TableName
                                    };
                                    temp.Id = dcontr.CreateMethod(temp, false);
                                    devices.Add(temp);
                                }
                                else//update
                                {
                                    f.NetProtocol = s.DeviceStyle == 2 ? "UDP" : "TCP";
                                    f.DeviceAddr = s.DeviceAddress;
                                    f.DevicePort = s.DevicePort;
                                    f.ReportAddr = s.RemoteAddress;
                                    f.ReportPort = s.RemotePort;
                                    f.EdgeMiddleAddr = s.WebSocketAddress;
                                    f.EdgeMiddlePort = s.WebSocketPort;
                                    f.PointNums = s.GoodFieldNum ?? 0;
                                    f.CollectCommand = s.Command;
                                    f.CollectInterval = s.Interval ?? 0;
                                    f.IsActived = s.IsActived;
                                    f.CodecsId = s.SolveType ?? 0;
                                    f.LastCollectTime = DateTime.MinValue;
                                    f.ModuleId = s.ModuleID;
                                    f.YunId = s.ID;
                                    f.YunTableName = s.TableName;
                                    dcontr.Edit(f);
                                }
                            }
                            #endregion
                            #region//测点
                            var points = yundb.Queryable<BsriMonitorPoint, BsriStructure, BsriMonitorStation, BsriSolveParam, BsriMonitorCategory>((w, s, st, b, ca) => new object[] {
                            JoinType.Left,w.StructureId==s.No,
                            JoinType.Left,w.StationId==st.ID,
                            JoinType.Left,w.Id==b.PointId,
                            JoinType.Left,w.CategoryId==ca.ID
                            }).Where((w, s) => s.StructureName == pro.ProjectName).Select<BsriMonitorPointView>("w.*,s.StructureName StructureName,st.StationName StationName,b.ParaName ParaName,b.ParaValue ParaValue,ca.CategoryCode CategoryCode").ToList();
                            foreach (var s in points)
                            {
                                var f = localpoints.FirstOrDefault(f => f.DeviceName == s.StationName && f.PointName == s.PointName && f.PointCode == s.PointCode);
                                var k = string.IsNullOrEmpty(s.CategoryCode) ? 0 : localkinds.FirstOrDefault(f => f.TempId.ToString() == s.CategoryCode)?.Id;//f.ProjectId == d &&
                                if (f == null)//add
                                {
                                    var tdevice = devices.FirstOrDefault(ff => ff.DeviceName == stations.FirstOrDefault(f => f.ID == s.StationId).StationName);
                                    pcontr.Create(new Monitor_Points()
                                    {
                                        ProjectId = d,
                                        DeviceId = tdevice.Id,
                                        DeviceName = tdevice.DeviceName,
                                        PointCode = s.PointCode,
                                        PointName = s.PointName,
                                        PointDesc = "云同步",
                                        InitChanel = s.ChannelId ?? 0,
                                        Chanel = s.ChannelId ?? 0,
                                        ComputeFormulaId = 0,
                                        ComputeFormula = new Monitor_Computeformulainfo() { FormulaName = s.ParaName, FormulaDesc = "云同步", FormulaKey = string.IsNullOrEmpty(s.ParaName) ? "k,b" : s.ParaName, FormulaValue = string.IsNullOrEmpty(s.ParaValue) ? "1,0" : s.ParaValue },
                                        MonitoringKindId = k ?? 0,//Convert.ToInt32(string.IsNullOrEmpty(s.CategoryId) ? "0" : s.CategoryId),
                                        IsActived = s.IsActived ?? 0,
                                        Range1Min = Convert.ToDecimal(s.Range1Min),
                                        Range1Max = Convert.ToDecimal(s.Range1Max),
                                        Range2Min = Convert.ToDecimal(s.Range2Min),
                                        Range2Max = Convert.ToDecimal(s.Range2Max),
                                        MaxValue = Convert.ToDecimal(s.MaxValue),
                                        MinValue = Convert.ToDecimal(s.MinValue),
                                        IsReasonableValidate = s.IsReasonableValidate ?? 0,
                                        IsThresholdAlarm = s.IsThresholdAlarm ?? 0,
                                        IsBasePoint = s.IsBase ?? 0,
                                        YunId = s.Id
                                    });
                                }
                                else//update
                                {
                                    f.PointCode = s.PointCode;
                                    f.PointName = s.PointName;
                                    f.PointDesc = "云同步";
                                    f.Chanel = s.ChannelId ?? 0;
                                    f.ComputeFormula = new Monitor_Computeformulainfo() { Id = f.ComputeFormulaId, FormulaName = s.ParaName, FormulaDesc = "云同步", FormulaKey = string.IsNullOrEmpty(s.ParaName) ? "k,b" : s.ParaName, FormulaValue = string.IsNullOrEmpty(s.ParaValue) ? "1,0" : s.ParaValue };
                                    f.MonitoringKindId = k ?? 0;//Convert.ToInt32(string.IsNullOrEmpty(s.CategoryId) ? "0" : s.CategoryId);
                                    f.IsActived = s.IsActived ?? 0;
                                    f.Range1Min = Convert.ToDecimal(s.Range1Min);
                                    f.Range1Max = Convert.ToDecimal(s.Range1Max);
                                    f.Range2Min = Convert.ToDecimal(s.Range2Min);
                                    f.Range2Max = Convert.ToDecimal(s.Range2Max);
                                    f.MaxValue = Convert.ToDecimal(s.MaxValue);
                                    f.MinValue = Convert.ToDecimal(s.MinValue);
                                    f.IsReasonableValidate = s.IsReasonableValidate ?? 0;
                                    f.IsThresholdAlarm = s.IsThresholdAlarm ?? 0;
                                    f.IsBasePoint = s.IsBase ?? 0;
                                    f.YunId = s.Id;
                                    pcontr.Edit(f);
                                }
                            }
                            #endregion
                        }
                        response.SetData("成功");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        response.SetError("失败" + ex.Message + "\r\n" + ex.StackTrace);
                    }
                    break;
                case "push"://推送
                    try
                    {
                        response.SetData("成功");
                    }
                    catch (Exception ex)
                    {
                        response.SetError("失败" + ex.Message + ex.StackTrace);
                    }
                    break;
                default:
                    break;
            }
            return Ok(response);
        }
        /// <summary>
        /// 文件上传/图片（结构物）
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadImg(IFormFileCollection files)
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
                var fileFolder = Path.Combine(_hostingEnvironment.ContentRootPath + "/wwwroot", "Uploads/Project");

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
                    response.Data = new { url = "Uploads/Project/" + fileName, name = fileName };
                }
            }
            return Json(response);
        }

    }
}
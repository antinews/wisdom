using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edge.WebApi.Entity.Monitor;
using Edge.WebApi.Entity.Web;
using Entitys;
//using Maikebing.Data.Taos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Wisdom.Webapi.Utils;

namespace Edge.WebApi.Controllers.Monitor
{
    [Route("api/Monitor/[controller]/[action]")]
    [ApiController]
    public class DevicesController : Monitor_BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public DevicesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public class DevicesPageRequest : PageRequest
        {
            /// <summary>	
            /// 状态 1启用0禁用-1默认/全部	
            /// </summary>	
            public int IsActived = -1;
            /// <summary>
            /// 结构物Id
            /// </summary>
            public int ProjectId { get; set; }

            public int AreaId { get; set; }

            public string DeviceName { get; set; }

            public string ReportAddr { get; set; }

            public string ReportPort { get; set; }
        }
        /// <summary>	
        /// 获取测站列表
        /// </summary>	
        /// <param name="payload">请求参数</param>	
        /// <returns></returns>	
        [HttpPost]
        public IActionResult List(DevicesPageRequest payload)
        {
            var response = new PageResponse();
            int total = 0;
            var list = db.Queryable<Monitor_Devices, base_buildbridge_main, Monitor_Codecs>((mo, st,c) => new object[] {
            JoinType.Left,mo.ProjectId==st.KeyId,
            JoinType.Left,mo.CodecsId == c.CodecsId
            });
            if (payload.ProjectId != 0)
            {
                list = list.Where(mo => mo.ProjectId == payload.ProjectId);
            }
            if (payload.AreaId != 0)
            {
                list = list.Where((mo,st,c) => st.CityAreaId == payload.AreaId);
            }
            if (payload.DeviceName!= null && payload.DeviceName.Trim()!="")
            {
                list = list.Where(mo => mo.DeviceName.Contains(payload.DeviceName));
            }
            if (payload.ReportAddr != null && payload.ReportAddr.Trim() != "")
            {
                list = list.Where(mo => mo.ReportAddr.Contains(payload.ReportAddr));
            }
            if (payload.ReportPort != null && payload.ReportPort.Trim() != "")
            {
                list = list.Where(mo => mo.ReportPort == Convert.ToInt32(payload.ReportPort));
            }
            if (payload.IsActived > -1)
            {
                list = list.Where(mo => mo.IsActived == payload.IsActived);
            }
            var data = list.Select<Monitor_Devices>("mo.*,st.BridgeName ProjectName,st.CityArea Area,c.CodecsName CodecsName")
                .OrderBy(mo => mo.Id, OrderByType.Desc).ToPageList(payload.CurrentPage, payload.PageSize, ref total);
            response.SetData(data);
            response.TotalCount = total;
            return Ok(response);
        }

        /// <summary>
        /// 服务于后台设备信息调取
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Monitor_Devices> Get(string ip, int port)
        {
            var deviceinfos = db.Queryable<Monitor_Devices, Monitor_Projects>((w, a) => new object[] {
            JoinType.Left,w.ProjectId==a.Id
            }).Where(w => w.ReportAddr == ip && w.ReportPort == port).Select<Monitor_Devices>("w.*,a.ProjectName ProjectName").ToList();
            foreach (var deviceinfo in deviceinfos)
            {
                deviceinfo.Codecs = db.Queryable<Monitor_Codecs>().Where(w => w.CodecsId == deviceinfo.CodecsId).Single();
                deviceinfo.Points = db.Queryable<Monitor_Points>().Where(w => w.DeviceId == deviceinfo.Id).ToList();
                List<int> ComputeFormulaIds = deviceinfo.Points.Where(w => w.ComputeFormulaId != null).Select(s => Convert.ToInt32(s.ComputeFormulaId)).ToList();
                List<Monitor_Computeformulainfo> ComputeFormulaInfos = db.Queryable<Monitor_Computeformulainfo>().Where(w => ComputeFormulaIds.Contains(w.Id)).ToList();
                foreach (var p in deviceinfo.Points)
                {
                    p.ComputeFormula = ComputeFormulaInfos.FirstOrDefault(f => f.Id == p.ComputeFormulaId);
                }
                List<int> MonitoringKindInfoIds = deviceinfo.Points.Where(w => w.MonitoringKindId != null).Select(s => Convert.ToInt32(s.MonitoringKindId)).ToList();
                List<Monitor_Kindinfo> MonitoringKindInfos = db.Queryable<Monitor_Kindinfo>().Where(w => MonitoringKindInfoIds.Contains(w.Id)).ToList();
                foreach (var p in deviceinfo.Points)
                {
                    p.MonitoringKind = MonitoringKindInfos.FirstOrDefault(f => f.Id == p.MonitoringKindId);
                }
                deviceinfo.StreamComputingTasks = db.Queryable<Monitor_Streamtasks>().Where(w => w.DeviceId == deviceinfo.Id && w.IsActived == 1).ToList();
                deviceinfo.DeviceAppSystem = db.Queryable<Monitor_Devices_Appsystem>().Where(w => w.ProjectId == deviceinfo.ProjectId).Single();
                deviceinfo.TaskSchedulerSettings = db.Queryable<Monitor_Schedulertasks>().Where(w => w.DeviceId == deviceinfo.Id && w.IsActived == 1).ToList();
            }
            return deviceinfos;
        }
        /// <summary>	
        /// 新增（测站）
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(Monitor_Devices model)
        {
            if (model.ProjectId <= 0)
            {
                response.SetError("结构物没选");
                return Ok(response);
            }
            if (model.CodecsId <= 0)
            {
                response.SetError("解算方式没选");
                return Ok(response);
            }
            response.SetData(CreateMethod(model));
            return Ok(response);
        }
        /// <summary>
        ///  创建的主体
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isCreatePoint"></param>
        [NonAction]
        public int CreateMethod(Monitor_Devices model, bool isCreatePoint = true)
        {
            if (string.IsNullOrEmpty(model.NetPluginPath))
            {
                model.NetPluginPath = "sparrow.EdgeDriver.Transport.dll";
            }
            if (model.NetProtocol != null)
            {
                if (model.NetProtocol == "UDP" || model.NetProtocol == "TCPClient")
                {
                    model.UploadMethod = "autoupload";
                }
                else
                {
                    model.UploadMethod = "polling";
                }
            }
            string oldReportAddr = model.ReportAddr;
            //model.ReportAddr = Configs.AppSettings.AppSetting("UDPTrans");
            //model.EdgeMiddleAddr = Configs.AppSettings.AppSetting("UDPTrans");
            model.Id = InsertableReturnIdentity(model);
            Monitor_Devices_Appsystem structsyss = db.Queryable<Monitor_Devices_Appsystem>().Where(s => model.ProjectId == s.ProjectId).Single();
            //TaosConnectionStringBuilder builder = new TaosConnectionStringBuilder()
            //{
            //    DataSource = structsyss.DataSource,
            //    DataBase = structsyss.Database,
            //    Username = structsyss.UserName,
            //    Password = structsyss.Password,
            //    Port = Convert.ToInt32(Configs.AppSettings.AppSetting("Port"))
            //};
            //using (var connection = new TaosConnection(builder.ConnectionString))
            //{
            //    connection.Open();
            //    try
            //    {
            //        var sql = $"create table IF NOT EXISTS {builder.DataBase}.T{model.Id} (ts timestamp, ";
            //        for (int i = 0; i < model.PointNums; i++) //按照设备接入最大数目创建表结构 表名不允许单独为数字，手工添加T作为前置
            //        {
            //            sql += $"col{i + 1} DOUBLE,";
            //        }
            //        if (model.Id > 0)
            //            sql = sql.Remove(sql.Length - 1, 1);

            //        sql += ");";
            //        var res = connection.CreateCommand(sql).ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug("创建表失败:" + ex.Message);
            //    }
            //    connection.Close();
            //}
            //List<string> tp = new List<string> { "min", "min10", "hour", "day" };
            //List<string> lx = new List<string> { "avg", "max", "min", "var" };
            //builder = new TaosConnectionStringBuilder()
            //{
            //    DataSource = structsyss.DataSource,
            //    DataBase = structsyss.Database.Replace("raw", "analy"),
            //    Username = structsyss.UserName,
            //    Password = structsyss.Password,
            //    Port = Convert.ToInt32(Configs.AppSettings.AppSetting("Port"))
            //};
            //using (var connection = new TaosConnection(builder.ConnectionString))
            //{
            //    connection.Open();
            //    try
            //    {
            //        foreach (var t in tp)
            //        {
            //            foreach (var l in lx)
            //            {
            //                var sql = $"create table IF NOT EXISTS {builder.DataBase}.T{model.Id}_{t}_{l} (ts timestamp, ";
            //                for (int i = 0; i < model.PointNums; i++) //按照设备接入最大数目创建表结构 表名不允许单独为数字，手工添加T作为前置
            //                {
            //                    sql += $"col{i + 1} DOUBLE,";
            //                }
            //                if (model.Id > 0)
            //                    sql = sql.Remove(sql.Length - 1, 1);

            //                sql += ");";
            //                var res = connection.CreateCommand(sql).ExecuteNonQuery();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug("创建表失败:" + ex.Message);
            //    }
            //    connection.Close();
            //}
            //新增测点
            //if (isCreatePoint)
            //{
            //    List<Monitor_Points> points = new List<Monitor_Points>();
            //    for (int i = 1; i < model.PointNums + 1; i++)
            //    {
            //        Monitor_Computeformulainfo temppamar = new Monitor_Computeformulainfo()
            //        {
            //            FormulaName = "K,B",
            //            FormulaDesc = "K,B",
            //            FormulaKey = "K,B",
            //            FormulaValue = "1,0"
            //        };
            //        temppamar.Id = InsertableReturnIdentity(temppamar);
            //        points.Add(new Monitor_Points()
            //        {
            //            DeviceId = model.Id,
            //            DeviceName = model.DeviceName,
            //            ProjectId = model.ProjectId,
            //            PointCode = model.ProjectId + "-" + model.Id + "-" + i,
            //            PointName = "测点" + i,
            //            PointDesc = "自动生成",
            //            InitChanel = i,
            //            Chanel = i,
            //            IsActived = 1,
            //            ComputeFormulaId = temppamar.Id,
            //            MonitoringKindId = 0
            //        }); ;
            //    }
            //    Insertable(points);
            //    UpdatePointsNumber(model.ProjectId);

            //}
            //else//云拉入才需要，界面新增其实调用了editstrams，会重复hex，因为前端传入的都是id=0,由于新增
            //{
            //    //数据存储-FILESQL
            //    Monitor_Streamtasks steamfilesql = new Monitor_Streamtasks()
            //    {
            //        DeviceId = model.Id,
            //        TaskName = "数据存储-FILESQL",
            //        TaskDesc = model.DeviceName + "文件存储",
            //        ExecuteMethod = "Native",
            //        ExecuteLanguage = "asp.net",
            //        ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
            //        ExecuteParams = Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("FILESQLConnstring"),
            //        IsActived = 1
            //    };
            //    InsertableReturnIdentity(steamfilesql);
            //    //UDP转发-After
            //    Monitor_Streamtasks steamudpafter = new Monitor_Streamtasks()
            //    {
            //        DeviceId = model.Id,
            //        TaskName = "UDP转发-After",
            //        TaskDesc = model.DeviceName + "UDP转发-After",
            //        ExecuteMethod = "Native",
            //        ExecuteLanguage = "asp.net",
            //        ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
            //        ExecuteParams = oldReportAddr + ":" + model.ReportPort,
            //        IsActived = 0
            //    };
            //    InsertableReturnIdentity(steamudpafter);
            //}
            UpdateStationNumber(model.ProjectId);
            ////steamtask
            //Monitor_Streamtasks steam = new Monitor_Streamtasks()
            //{
            //    DeviceId = model.Id,
            //    TaskName = "数据存储",
            //    TaskDesc = model.DeviceName + "数据存储",
            //    ExecuteMethod = "Native",
            //    ExecuteLanguage = "asp.net",
            //    ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
            //    IsActived = 1
            //};
            //InsertableReturnIdentity(steam);


            ////task
            //Monitor_Schedulertasks hand = new Monitor_Schedulertasks()
            //{
            //    DeviceId = model.Id,
            //    TaskName = "每分钟统计数据",
            //    TaskMode = "Clocked",
            //    ClockedInterval = 60000,
            //    TimingMode = "Hour",
            //    TimingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //    GrpcSrvUrl = "127.0.0.1:50051",
            //    TaskPluginMethod = "Native",
            //    TaskPluginLanguage = "Python",
            //    TaskPluginFilePath = "sparrow.EdgeApp.TaskScheduler.Hybrid.dll",
            //    ExecuteMethod = "Hybird",
            //    ExecuteFilePath = "task_statistics.py",
            //    ExecuteLanguage = "Python",
            //    IsActived = 1
            //};
            //InsertableReturnIdentity(hand);
            //TaskSchedulerSyncToCloud
            //Monitor_Schedulertasks TaskSchedulerSyncToCloud = new Monitor_Schedulertasks()
            //{
            //    DeviceId = model.Id,
            //    TaskName = "定时同步分析数据至云端",
            //    TaskMode = "Clocked",
            //    ClockedInterval = 60000,
            //    TimingMode = "Hour",
            //    TimingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //    GrpcSrvUrl = "127.0.0.1:50051",
            //    TaskPluginMethod = "TaskSchedulerSyncToCloud",
            //    TaskPluginLanguage = "C#",
            //    TaskPluginFilePath = "sparrow.EdgeApp.TaskScheduler.Native.dlll",
            //    ExecuteMethod = "Native",
            //    ExecuteFilePath = "",
            //    ExecuteLanguage = "C#",
            //    ExecuteParams = Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("TaskSchedulerSyncToCloud"),
            //    IsActived = 0
            //};
            //InsertableReturnIdentity(TaskSchedulerSyncToCloud);
            ////TaskSchedulerStaticsFileSQLImp
            //Monitor_Schedulertasks TaskSchedulerStaticsFileSQLImp = new Monitor_Schedulertasks()
            //{
            //    DeviceId = model.Id,
            //    TaskName = "分析数据统计",
            //    TaskMode = "Clocked",
            //    ClockedInterval = 60000,
            //    TimingMode = "Hour",
            //    TimingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //    GrpcSrvUrl = "127.0.0.1:50051",
            //    TaskPluginMethod = "TaskSchedulerStaticsFileSQLImp",
            //    TaskPluginLanguage = "C#",
            //    TaskPluginFilePath = "sparrow.EdgeApp.TaskScheduler.Native.dlll",
            //    ExecuteMethod = "Native",
            //    ExecuteFilePath = "",
            //    ExecuteLanguage = "C#",
            //    ExecuteParams = Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("TaskSchedulerStaticsFileSQLImp"),
            //    IsActived = 1
            //};
            //InsertableReturnIdentity(TaskSchedulerStaticsFileSQLImp);
            return model.Id;
        }

        /// <summary>	
        /// 单个数据（测站）
        /// </summary>	
        /// <param name="id">惟一编码</param>	
        /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(int id)
        {
            response.SetData(Single<Monitor_Devices>(s => s.Id == id));
            return Ok(response);
        }
        /// <summary>	
        /// 获取该测站下含有4种存储任务，哪怕没有，我也会创建一个不启用
        /// </summary>	
        /// <param name="id">惟一编码</param>	
        /// <returns></returns>	
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetStreams(int id)
        {
            var steams = db.Queryable<Monitor_Streamtasks>().Where(w => w.DeviceId == id).ToList();
            //数据存储-FILESQL
            Monitor_Streamtasks file = steams.FirstOrDefault(f => f.TaskName == "数据存储-FILESQL");
            if (file == null)
            {
                file = new Monitor_Streamtasks()
                {
                    DeviceId = id,
                    TaskName = "数据存储-FILESQL",
                    TaskDesc = id + "文件存储",
                    ExecuteMethod = "Native",
                    ExecuteLanguage = "asp.net",
                    ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
                    ExecuteParams = Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("FILESQLConnstring"),
                    IsActived = 0
                };
                file.Id = InsertableReturnIdentity(file);
            }
            //UDP转发-Hex
            Monitor_Streamtasks hex = steams.FirstOrDefault(f => f.TaskName == "UDP转发-Hex");
            if (hex == null)
            {
                hex = new Monitor_Streamtasks()
                {
                    DeviceId = id,
                    TaskName = "UDP转发-Hex",
                    TaskDesc = id + "UDP转发-Hex",
                    ExecuteMethod = "Native",
                    ExecuteLanguage = "asp.net",
                    ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
                    ExecuteParams = "192.168.22.22:222",
                    IsActived = 0
                };
                hex.Id = InsertableReturnIdentity(hex);
            }
            //UDP转发-Before
            Monitor_Streamtasks Before = steams.FirstOrDefault(f => f.TaskName == "UDP转发-Before");
            if (Before == null)
            {
                Before = new Monitor_Streamtasks()
                {
                    DeviceId = id,
                    TaskName = "UDP转发-Before",
                    TaskDesc = id + "UDP转发-Before",
                    ExecuteMethod = "Native",
                    ExecuteLanguage = "asp.net",
                    ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
                    ExecuteParams = "192.168.22.22:222",
                    IsActived = 0
                };
                Before.Id = InsertableReturnIdentity(Before);
            }
            //UDP转发-After
            Monitor_Streamtasks After = steams.FirstOrDefault(f => f.TaskName == "UDP转发-After");
            if (After == null)
            {
                After = new Monitor_Streamtasks()
                {
                    DeviceId = id,
                    TaskName = "UDP转发-After",
                    TaskDesc = id + "UDP转发-After",
                    ExecuteMethod = "Native",
                    ExecuteLanguage = "asp.net",
                    ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
                    ExecuteParams = "192.168.22.22:222",
                    IsActived = 0
                };
                After.Id = InsertableReturnIdentity(After);
            }
            response.SetData(new tempstreams() { file = file, hex = hex, before = Before, after = After });
            return Ok(response);
        }
        /// <summary>
        /// 4个streams临时类
        /// </summary>
        public class tempstreams
        {
            public Monitor_Streamtasks file { get; set; }
            public Monitor_Streamtasks hex { get; set; }
            public Monitor_Streamtasks before { get; set; }
            public Monitor_Streamtasks after { get; set; }
        }

        /// <summary>	
        ///编辑修改streams
        /// </summary>	
        /// <param name="streams"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult EditStreams(tempstreams tempstreams)
        {
            List<Monitor_Streamtasks> streams = new List<Monitor_Streamtasks>();
            if (tempstreams.file != null)
            {
                if (tempstreams.file.Id == 0)
                {
                    tempstreams.file = new Monitor_Streamtasks()
                    {
                        DeviceId = tempstreams.file.DeviceId,
                        TaskName = "数据存储-FILESQL",
                        TaskDesc = tempstreams.file.DeviceId + "文件存储",
                        ExecuteMethod = "Native",
                        ExecuteLanguage = "asp.net",
                        ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
                        ExecuteParams = string.IsNullOrEmpty(tempstreams.file.ExecuteParams) ? Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("FILESQLConnstring") : tempstreams.file.ExecuteParams,
                        IsActived = tempstreams.file.IsActived
                    };
                    tempstreams.file.Id = InsertableReturnIdentity(tempstreams.file);
                }
                streams.Add(tempstreams.file);
            }
            if (tempstreams.hex != null)
            {
                if (tempstreams.hex.Id == 0)
                {
                    tempstreams.hex = new Monitor_Streamtasks()
                    {
                        DeviceId = tempstreams.file.DeviceId,
                        TaskName = "UDP转发-Hex",
                        TaskDesc = tempstreams.file.DeviceId + "UDP转发-Hex",
                        ExecuteMethod = "Native",
                        ExecuteLanguage = "asp.net",
                        ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
                        ExecuteParams = tempstreams.hex.ExecuteParams,
                        IsActived = tempstreams.hex.IsActived
                    };
                    tempstreams.hex.Id = InsertableReturnIdentity(tempstreams.hex);
                }
                streams.Add(tempstreams.hex);
            }
            if (tempstreams.before != null)
            {
                if (tempstreams.before.Id == 0)
                {
                    tempstreams.before = new Monitor_Streamtasks()
                    {
                        DeviceId = tempstreams.file.DeviceId,
                        TaskName = "UDP转发-Before",
                        TaskDesc = tempstreams.file.DeviceId + "UDP转发-Before",
                        ExecuteMethod = "Native",
                        ExecuteLanguage = "asp.net",
                        ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
                        ExecuteParams = tempstreams.before.ExecuteParams,
                        IsActived = tempstreams.before.IsActived
                    };
                    tempstreams.before.Id = InsertableReturnIdentity(tempstreams.before);
                }
                streams.Add(tempstreams.before);
            }
            if (tempstreams.after != null)
            {
                if (tempstreams.after.Id == 0)
                {
                    tempstreams.after = new Monitor_Streamtasks()
                    {
                        DeviceId = tempstreams.file.DeviceId,
                        TaskName = "UDP转发-After",
                        TaskDesc = tempstreams.file.DeviceId + "UDP转发-After",
                        ExecuteMethod = "Native",
                        ExecuteLanguage = "asp.net",
                        ExecuteFilePath = "sparrow.EdgeApp.Stream.Native.dll",
                        ExecuteParams = tempstreams.after.ExecuteParams,
                        IsActived = tempstreams.after.IsActived
                    };
                    tempstreams.after.Id = InsertableReturnIdentity(tempstreams.after);
                }
                streams.Add(tempstreams.after);
            }
            Updateable(streams);
            response.SetSuccess();
            return Ok(response);
        }
        /// <summary>	
        ///编辑修改（测站） 切记：先获取，再修改，不管你改没改其他信息，都需要传回来
        /// </summary>	
        /// <param name="model"></param>	
        /// <returns></returns>	
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(Monitor_Devices model)
        {
            if (model.ProjectId <= 0)
            {
                response.SetError("结构物没选");
                return Ok(response);
            }
            if (model.CodecsId <= 0)
            {
                response.SetError("解算方式没选");
                return Ok(response);
            }
            if (model.NetProtocol != null)
            {
                if (model.NetProtocol == "UDP" || model.NetProtocol == "TCPClient")
                {
                    model.UploadMethod = "autoupload";
                }
                else
                {
                    model.UploadMethod = "polling";
                }
            }
            //model.ReportAddr = Configs.AppSettings.AppSetting("UDPTrans");
            //model.EdgeMiddleAddr = Configs.AppSettings.AppSetting("UDPTrans");
            //var solve = Single<Monitor_Codecs>(s => s.CodecsId == model.CodecsId);
            //model.SersorNumber = solve.SensorNumber;
            //model.Command = solve.Remark;
            //model.ModifyDate = DateTime.Now;
            //model.ModifyUserId = AuthContextService.CurrentUserNew.UserId;
            response.SetData(Updateable(model));
            UpdateStationNumber(model.ProjectId);
            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 删除测站/设备
        /// </summary>	
        /// <param name="ids">角色ID,多个以逗号分隔</param>	
        /// <returns></returns>	
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            List<int> id = ids.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            List<Monitor_Devices> devices = db.Queryable<Monitor_Devices>().Where(s => id.Contains(s.Id)).ToList();
            List<int> proids = devices.Select(s => s.ProjectId).ToList();
            List<Monitor_Devices_Appsystem> structsyss = db.Queryable<Monitor_Devices_Appsystem>().Where(s => proids.Contains(s.ProjectId)).ToList();
            List<string> lx = new List<string> { "avg", "max", "min", "var" };
            foreach (var s in structsyss)
            {
                //TaosConnectionStringBuilder builder = new TaosConnectionStringBuilder()
                //{
                //    DataSource = s.DataSource,
                //    DataBase = s.Database,
                //    Username = s.UserName,
                //    Password = s.Password,
                //    Port = Convert.ToInt32(Configs.AppSettings.AppSetting("Port"))
                //};
                ////删除表操作
                ////
                //using (var connection = new TaosConnection(builder.ConnectionString))
                //{
                //    connection.Open();
                //    try
                //    {
                //        var des = devices.Where(w => w.ProjectId == s.ProjectId).ToList();
                //        foreach (var d in des)
                //        {
                //            var createdbsql = $"DROP TABLE IF EXISTS {builder.DataBase}.T{d.Id}";

                //            var res = connection.CreateCommand(createdbsql).ExecuteNonQuery();
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Debug("删除数据库失败:" + ex.Message);
                //    }
                //    connection.Close();
                //}
                ////分析库
                //builder = new TaosConnectionStringBuilder()
                //{
                //    DataSource = s.DataSource,
                //    DataBase = s.Database.Replace("raw", "analy"),
                //    Username = s.UserName,
                //    Password = s.Password,
                //    Port = Convert.ToInt32(Configs.AppSettings.AppSetting("Port"))
                //};
                ////删除表操作
                ////
                //using (var connection = new TaosConnection(builder.ConnectionString))
                //{
                //    connection.Open();
                //    try
                //    {
                //        var des = devices.Where(w => w.ProjectId == s.ProjectId).ToList();
                //        foreach (var d in des)
                //        {
                //            StringBuilder sql = new StringBuilder();
                //            foreach (var l in lx)
                //            {
                //                sql.Append($"DROP TABLE IF EXISTS {builder.DataBase}.T{d.Id}_min_{l};")
                //                   .Append($"DROP TABLE IF EXISTS {builder.DataBase}.T{d.Id}_min10_{l};")
                //                   .Append($"DROP TABLE IF EXISTS {builder.DataBase}.T{d.Id}_hour_{l};")
                //                   .Append($"DROP TABLE IF EXISTS {builder.DataBase}.T{d.Id}_day_{l};");
                //            }
                //            var res = connection.CreateCommand(sql.ToString()).ExecuteNonQuery();
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Debug("删除数据库失败:" + ex.Message);
                //    }
                //    connection.Close();
                //}
            }
            Deleteable<Monitor_Computeformulainfo>(s => db.Queryable<Monitor_Points>().Where(w => id.Contains(w.DeviceId)).Select(e => e.ComputeFormulaId).ToList().Contains(s.Id));//删测点
            Deleteable<Monitor_Points>(s => id.Contains(s.DeviceId));//删测点
            response.SetData(Deleteable<Monitor_Devices>(s => id.Contains(s.Id)));//删模块
            Deleteable<Monitor_Streamtasks>(s => id.Contains(s.DeviceId));
            Deleteable<Monitor_Schedulertasks>(s => id.Contains(s.DeviceId));

            foreach (var item in devices)
            {
                Stop(item);
            }

            response.SetSuccess();
            return Ok(response);
        }

        /// <summary>	
        /// 批量操作（测站）
        /// </summary>	
        /// <param name="command">enable启用，disable禁用</param>	
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
                    response.SetData(Updateable<Monitor_Devices>(t => new Monitor_Devices { IsActived = 1 }, w => id.Contains(w.Id)));
                    response.SetSuccess();
                    break;
                case "disable":
                    response.SetData(Updateable<Monitor_Devices>(t => new Monitor_Devices { IsActived = 0 }, w => id.Contains(w.Id)));
                    response.SetSuccess();
                    break;
                case "public"://删除发布
                    try
                    {
                        string PublishPath = Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("PublishPath");
                        var des = db.Queryable<Monitor_Devices>().Where(w => id.Contains(w.Id)).ToList();
                        foreach (var d in des)
                        {
                            //先杀死
                            try
                            {
                                var pk = new ProcessStartInfo
                                {
                                    FileName = "/bin/bash",
                                    UseShellExecute = false,
                                    RedirectStandardOutput = true,
                                    Arguments = string.Format("-c \"{0}\"", "ps -ef | grep edge" + d.ReportAddr + "_" + d.ReportPort + " | grep -v 'grep' | awk '{print $2}' | xargs kill -9")
                                };
                                var prock = Process.Start(pk);
                                if (prock == null)
                                {
                                    Console.WriteLine(d.DeviceName + "杀死失败--");
                                }
                                else
                                {
                                    Console.WriteLine("-------------Start read standard output-------cagy-------");
                                    ////开始读取
                                    //using (var sr = prock.StandardOutput)
                                    //{
                                    //    string result = "";
                                    //    while (!sr.EndOfStream)
                                    //    {
                                    //        result += sr.ReadLine();
                                    //    }
                                    //    Console.WriteLine("-> " + result);

                                    //}
                                    Console.WriteLine(d.DeviceName + "杀死成功-> ");
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug("杀死异常" + d.DeviceName + ex.Message + "--");
                            }


                            string destDir = PublishPath + "/edge"+d.ReportAddr+"_" + d.ReportPort;
                            DirectoryInfo destDirectory = new DirectoryInfo(destDir);
                            if (destDirectory.Exists)
                            {
                                destDirectory.Delete(true);
                            }
                            Common.CopyDirectory(PublishPath + "/demo", destDir);
                            System.IO.File.Move(  destDir + "/sparrow.EdgeCore",   destDir + "/edge" + d.ReportAddr + "_" + d.ReportPort);


                            System.IO.File.WriteAllText(  destDir + "/appsettings.json",
                               Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("PublishDemon").Replace("@addr", d.ReportAddr.ToString()).Replace("@port", d.ReportPort.ToString()));
                            start(d);
                        }
                        response.SetData("成功");
                    }
                    catch (Exception ex)
                    {
                        response.SetError("失败" + ex.Message + ex.StackTrace);
                    }
                    break;
                case "restart":
                    try
                    {
                        var des = db.Queryable<Monitor_Devices>().Where(w => id.Contains(w.Id)).ToList();
                        StringBuilder str = new StringBuilder();
                        foreach (var d in des)
                        {
                            str.Append(start(d));
                        }
                        response.SetData("启动成功:" + str.ToString());
                    }
                    catch (Exception ex)
                    {
                        response.SetError("失败" + ex.Message);
                    }
                    break;
                default:
                    break;
            }
            return Ok(response);
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [NonAction]
        public string start(Monitor_Devices d)
        {

            string PublishPath = Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("PublishPath");
            StringBuilder str = new StringBuilder();

            //string file = PublishPath + "/edge" + d.ReportPort;

            //var cmd = "cd " + file + " && chmod +777 * && nohup " + file + "/edge" + d.ReportPort+" & exit";
            //System.IO.File.WriteAllText(PublishPath + "/start.sh", cmd);

            //先杀死
            try
            {
                //"/edge"+d.ReportAddr+"@" + d.ReportPort
                var pk = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    Arguments = string.Format("-c \"{0}\"", "ps -ef | grep edge" + d.ReportAddr + "_"  + d.ReportPort + " | grep -v 'grep' | awk '{print $2}' | xargs kill -9")
                };
                var prock = Process.Start(pk);
                if (prock == null)
                {
                    str.Append(d.DeviceName + "杀死失败--");
                }
                else
                {
                    Console.WriteLine("-------------Start read standard output-------cagy-------");
                    //开始读取
                    //using (var sr = prock.StandardOutput)
                    //{
                    //    string result = "";
                    //    while (!sr.EndOfStream)
                    //    {
                    //        result += sr.ReadLine();
                    //    }
                    //    str.Append(d.DeviceName + "杀死结果：" + result + "--");
                    //    Console.WriteLine(d.DeviceName + "杀死结果-> " + result);

                    //}
                    str.Append(d.DeviceName + "杀死成功--");
                }
            }
            catch (Exception ex)
            {
                str.Append("杀死异常" + d.DeviceName + ex.Message + "--");
            }

            var p = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = string.Format("-c \"{0}\"", "cd " + PublishPath + "/edge" + d.ReportAddr + "_" + d.ReportPort + " && chmod +777 * && nohup " + PublishPath + "/edge" + d.ReportAddr + "_" + d.ReportPort + "/edge" + d.ReportAddr + "_" + d.ReportPort + " >edge+" + d.ReportPort + ".log 2>&1 &")
            };
            var proc = Process.Start(p);


            if (proc == null)
            {
                str.Append(d.DeviceName + "启动失败--");
            }
            else
            {
                Console.WriteLine("-------------Start read standard output-------cagy-------");
                //开始读取
                //using (var sr = proc.StandardOutput)
                //{

                //    while (!sr.EndOfStream)
                //    {
                //        string result = "";
                //        while (!sr.EndOfStream)
                //        {
                //            result += sr.ReadLine();
                //        }
                //        str.Append(d.DeviceName + "启动结果" + result + "--");
                //        Console.WriteLine(d.DeviceName + "启动结果-> " + result);
                //    }

                //}
                str.Append(d.DeviceName + "启动成功--");
            }
            return str.ToString();
        }

        [NonAction]
        public string Stop(Monitor_Devices d)
        {
            string PublishPath = Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("PublishPath");
            StringBuilder str = new StringBuilder();
            //先杀死
            try
            {
                var pk = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    Arguments = string.Format("-c \"{0}\"", "ps -ef | grep edge" + d.ReportAddr + "_" + d.ReportPort + " | grep -v 'grep' | awk '{print $2}' | xargs kill -9")
                };
                var prock = Process.Start(pk);
                if (prock == null)
                {
                    Console.WriteLine(d.DeviceName + "杀死失败--");
                    str.Append(d.DeviceName + "杀死失败--");
                }
                else
                {
                    Console.WriteLine("-------------Start read standard output-------cagy-------");
                    ////开始读取
                    //using (var sr = prock.StandardOutput)
                    //{
                    //    string result = "";
                    //    while (!sr.EndOfStream)
                    //    {
                    //        result += sr.ReadLine();
                    //    }
                    //    Console.WriteLine("-> " + result);

                    //}
                    Console.WriteLine(d.DeviceName + "杀死成功-> ");
                    str.Append(d.DeviceName + "杀死成功--");
                }
            }
            catch (Exception ex)
            {
                Debug("杀死异常" + d.DeviceName + ex.Message + "--");
            }


            string destDir = PublishPath + "/edge" + d.ReportPort;
            DirectoryInfo destDirectory = new DirectoryInfo(destDir);
            if (destDirectory.Exists)
            {
                destDirectory.Delete(true);
            }
            str.Append(d.DeviceName + "删除成功--");
            return str.ToString();
        }
        [NonAction]
        private string Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            Console.WriteLine("result -> " + result);
            process.WaitForExit();
            return result;
        }

        [NonAction]
        string RunCommand(string command, string args)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (string.IsNullOrEmpty(error)) { return output; }
            else { return error; }
        }

        /// <summary>
        /// 文件上传/图片（测站）
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
                var fileFolder = Path.Combine(_hostingEnvironment.ContentRootPath + "/wwwroot", "Uploads/Device");

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
                    response.Data = new { url = "Uploads/Device/" + fileName, name = fileName };
                }
            }
            return Json(response);
        }
    }
}
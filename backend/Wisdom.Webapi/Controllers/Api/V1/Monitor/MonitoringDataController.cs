using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Edge.WebApi.Entity.Monitor;
//using Maikebing.Data.Taos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Wisdom.Webapi.Utils;

namespace Edge.WebApi.Controllers.Monitor
{
    [Route("api/Monitor/[controller]/[action]")]
    [ApiController]
    public class MonitoringDataController : Monitor_BaseController
    {

        /// <summary>
        /// 历史数据（point：测点Id  type：数据种类：min-1、min10-2、hour-3、day-4、原始-default/0  lx：数据类型：最小1 最大2 方差3 平均0 开始/结束时间，没有请传入""）
        /// </summary>
        /// <param name="point">测点Id</param>
        /// <param name="type">数据种类：min-1、min10-2、hour-3、day-4、原始-default/0</param>
        /// <param name="lx">数据类型：最小1 最大2 方差3 平均0</param>
        /// <param name="stime">开始时间，没有请传入null</param>
        /// <param name="etime">结束时间，没有请传入null</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLineData(int point, int type, int lx, DateTime? stime, DateTime? etime)
        {
            if (point <= 0)
            {
                response.SetError("意外测点");
                return Ok(response);
            }
            var data = db.Queryable<Monitor_Points, Monitor_Projects, Monitor_Kindinfo, Monitor_Devices>((mo, a, b, c) => new object[] {
            JoinType.Left,mo.ProjectId==a.Id,
            JoinType.Left,mo.MonitoringKindId==b.Id,
            JoinType.Left,mo.DeviceId==c.Id,
            }).Where((mo, a, b, c) => mo.Id == point).Select<Monitor_PointsView>("mo.*,a.ProjectName ProjectName,b.`Precision` `Precision`,c.CollectInterval").Single();
            if (data.CollectInterval <= 1000 && type == 0)//查文件
            {
                TxtFind(data, type, lx, stime, etime);
            }
            else//查数据库
            {
                DBFind(data, type, lx, stime, etime);
            }
            return Ok(response);
        }
        private void DBFind(Monitor_PointsView data, int type, int lx, DateTime? stime, DateTime? etime)
        {

            Monitor_Devices_Appsystem structsyss = db.Queryable<Monitor_Devices_Appsystem>().Where(s => data.ProjectId == s.ProjectId).Single();
            string sql = "";
            string table = "";
            string constring = "";
            string lxstring = "";
            switch (lx)
            {
                case 1://max
                    lxstring = "_max";
                    break;
                case 2://min
                    lxstring = "_min";
                    break;
                case 3://var
                    lxstring = "_var";
                    break;
                default://平均
                    lxstring = "_avg";
                    break;
            }
            switch (type)
            {
                case 1://min
                    table = data.DeviceId + "_min" + lxstring;
                    constring = string.Format(Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("StaticStorgeConnstring"), data.ProjectId + "_analy");
                    break;
                case 2://10min
                    table = data.DeviceId + "_10min" + lxstring;
                    constring = string.Format(Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("StaticStorgeConnstring"), data.ProjectId + "_analy");
                    break;
                case 3://hour
                    table = data.DeviceId + "_hour" + lxstring;
                    constring = string.Format(Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("StaticStorgeConnstring"), data.ProjectId + "_analy");
                    break;
                case 4://day
                    table = data.DeviceId + "_day" + lxstring;
                    constring = string.Format(Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("StaticStorgeConnstring"), data.ProjectId + "_analy");
                    break;
                default://原始
                    table = data.DeviceId.ToString();
                    constring = string.Format(Wisdom.Webapi.Configurations.AppSettings.GetAppSeetingSectionByKey("StaticStorgeConnstring"), data.ProjectId + "_c");
                    break;
            }
            if (stime == null || etime == null)//没选任何一个时间,默认最新的倒数3000条
            {
                sql = $@"select col0 time,col{data.Chanel} value from `{table}` order by col0 desc LIMIT 0,10000";
            }
            else
            {
                sql = $@"select col0 time,col{data.Chanel} value from `{table}` where col0<='{Convert.ToDateTime(etime).ToString("yyyy-MM-dd-HH-mm-ss.fff")}' and col0>='{Convert.ToDateTime(stime).ToString("yyyy-MM-dd-HH-mm-ss.fff")}' order by col0 desc LIMIT 0,10000";
            }
            SqlSugarClient datadb = new SqlSugarClient(
                new ConnectionConfig()
                {
                    ConnectionString = Common.ConnectionStringsReplace(constring),
                    DbType = DbType.MySql,//设置数据库类型
                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                });
            //response.SetData(new { line = datadb.SqlQueryable<TempLineData>(sql).OrderBy(o => o.time, OrderByType.Asc).ToList().Select(s => new LineData { time = (Common.StrTransDateTime(s.time).Ticks - 621356256000000000) / 10000, value = s.value }).ToList(), Precision = data.Precision });
            response.SetData(new { line = datadb.SqlQueryable<TempLineData>(sql).OrderBy(o => o.time, OrderByType.Asc).ToList().Select(s => new LineData { time = (Common.StrTransDateTime(s.time).Ticks - 621356256000000000) / 10000, value = (s.value.ToString().Contains("e") || s.value.ToString().Contains("E")) ? Decimal.Parse(s.value.ToString(), System.Globalization.NumberStyles.Float) : Convert.ToDecimal(s.value.ToString()) }).ToList(), Precision = data.Precision });
        }
        private void TxtFind(Monitor_PointsView data, int type, int lx, DateTime? stime, DateTime? etime)
        {
            string DataRootPath = "";
            string splitflag = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                DataRootPath = "/root/data/" + data.DeviceId;
                splitflag = "/";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                DataRootPath = "C:\\data\\" + data.DeviceId;
                splitflag = "\\";
            }
            string filepath = "";
            if (stime == null || etime == null)//没选任何一个时间,默认最新的倒数3000条
            {
                if (!System.IO.Directory.Exists(DataRootPath))
                {
                    return;
                }
                var filelist = Directory.GetFiles(DataRootPath, "*.txt").ToList().OrderByDescending(o => o).ToList();
                if (filelist.Count == 0)
                {
                    response.SetError("没有找到数据文件");
                    return;
                }
                filepath = filelist[0];

            }
            else
            {
                var nowtime = Convert.ToDateTime(etime).Minute / 5 * 5;
                filepath = DataRootPath + splitflag + Convert.ToDateTime(etime).ToString("yyyy-MM-dd-HH-") + nowtime.ToString().PadLeft(2, '0') + ".txt";
                if (!System.IO.File.Exists(filepath))
                {
                    response.SetData(new { line = new List<object>(), Precision = data.Precision });
                    return;
                }
            }
            FileInfo fileInfo1 = new FileInfo(filepath);
            FileStream fs = fileInfo1.Open(FileMode.Open);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);

            var dataline = new List<LineData>();
            if (fs.Length > 0)
                do
                {
                    string ss = sr.ReadLine();
                    var arrs = ss.Split(' ');
                    //dataline.Add(new LineData() { time = (Common.StrTransDateTime(arrs[0]).Ticks - 621356256000000000) / 10000, value = Convert.ToDecimal(arrs[data.Chanel]) });
                    dataline.Add(new LineData() { time = (Common.StrTransDateTime(arrs[0]).Ticks - 621356256000000000) / 10000, value = (arrs[data.Chanel].Contains("e") || arrs[data.Chanel].Contains("E")) ? Decimal.Parse(arrs[data.Chanel], System.Globalization.NumberStyles.Float) : Convert.ToDecimal(arrs[data.Chanel]) });
                    
                    int a = sr.Peek();
                }

                while (sr.Peek() != -1);
            fs.Close();
            response.SetData(new { line = dataline.OrderByDescending(o => o.time).Take(10000).ToList(), Precision = data.Precision });
        }
        public class TempLineData
        {
            /// <summary>
            /// 时间
            /// </summary>
            public string time { get; set; }
            /// <summary>
            /// 值
            /// </summary>
            public object value { get; set; }
        }
        public class LineData
        {
            /// <summary>
            /// 时间
            /// </summary>
            public long time { get; set; }
            /// <summary>
            /// 值
            /// </summary>
            public object value { get; set; }
        }
    }
}
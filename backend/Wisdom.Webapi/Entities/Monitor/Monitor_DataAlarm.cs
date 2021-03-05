using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    /// <summary>
    /// 报警记录
    /// </summary>
    public class Monitor_DataAlarm
    {
        /// <summary>
        /// 自增长主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 结构物Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 测站Id
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// 测点Id
        /// </summary>
        public int PointId { get; set; }
        /// <summary>
        /// 报警级别
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 报警开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 报警结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 报警值
        /// </summary>
        public string AlarmValue { get; set; } = "";

        /// <summary>
        /// 超阈值情况
        /// </summary>
        public string AlarmRange { get; set; } = "";

        /// <summary>
        /// 报警状态
        /// </summary>
        public int State { get; set; }
    }
}

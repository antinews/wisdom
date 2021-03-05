using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Models.Export
{
    [Description("考勤记录")]
    public class AttendanceRecordExportModel
    {
        [Description("部门")]
        public string DepartmentName { get; set; }
        [Description("人员")]
        public string UserName { get; set; }
        [Description("出勤时间")]
        public DateTime? StartTime { get; set; }
        [Description("总时长")]
        public int? TotalTime { get; set; }
        [Description("总距离(公里)")]
        public float? Distance { get; set; }
    }
}

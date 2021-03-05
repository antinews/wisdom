using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.ViewModels.Maintenance.Attendance
{
    public class AttendanceRecordJsonModel
    {
        public string Department { get; set; }
        public string Info { get; set; }
        public int? TotalTime { get; set; }
        public float? Distance { get; set; }
    }
}

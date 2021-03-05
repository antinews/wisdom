using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Models.Request.PollingInspectionCustody.Attendance
{
    public class AttendanceRecordRequestPayload:RequestPayload
    {
        public int? userId { get; set; }
        public DateTime? stime { get; set; }
        public DateTime? etime { get; set; }
    }
}

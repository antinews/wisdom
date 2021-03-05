using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Models.Request.PollingInspectionCustody.Inspection
{
    public class DailyInspectionRequestPayload:RequestPayload
    {
        public int? departId { get; set; }
        public int? userId { get; set; }
        public int? areaId { get; set; }
        public int? bridgeId { get; set; }
        public DateTime? stime { get; set; }
        public DateTime? etime { get; set; }
        public string status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Models.Request.Systems
{
    public class PermissionRequestPayload : RequestPayload
    {
        public string permissionName { get; set; }
        public int? menuId { get; set; }
        public int? type { get; set; }
    }
}

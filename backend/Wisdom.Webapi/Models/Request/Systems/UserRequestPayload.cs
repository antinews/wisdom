using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Models.Request.Systems
{
    public class UserRequestPayload:RequestPayload
    {
        /// <summary>
        /// 用户真名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Depart { get; set; }
    }
}

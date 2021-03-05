using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Models.Request.Systems
{
    public class RoleAssignPermissionPayload
    {
        public RoleAssignPermissionPayload()
        {
            Permissions = new List<int>();
        }
        /// <summary>
        /// 角色Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<int> Permissions { get; set; }
    }
}

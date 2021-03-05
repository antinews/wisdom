using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.ViewModels.Systems.Permission
{
    public class PermissionEditModel
    {
        public int MenuId { get; set; }
        public List<Permission> Permissions { get; set; }
    }
    public class Permission
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

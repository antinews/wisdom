using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.ViewModels.Systems.Permission
{
    public class PermissionJsonViewTree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MenuName { get; set; }
        public int MenuId { get; set; }
        public string ActionCode { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int Type { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.ViewModels.Systems.Role
{
    public class RoleJsonModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int? SortNum { get; set; }
        public int? IsDefault { get; set; }
        public string Remark { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedUser { get; set; }
    }
}

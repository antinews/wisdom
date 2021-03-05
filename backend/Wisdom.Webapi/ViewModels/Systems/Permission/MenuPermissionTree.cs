using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.ViewModels.Systems.Permission
{
    public class MenuPermissionTree :PermissionJsonViewTree
    {
        public List<PermissionJsonViewTree> Children { get; set; }
    }
}

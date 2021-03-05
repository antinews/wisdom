using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Entities.QueryModels.Permission
{
    /// <summary>
    /// 权限实体类
    /// </summary>
    public class PermissionWithMenu
    {
        /// <summary>
        /// 权限操作码
        /// </summary>
        public string PermissionActionCode { get; set; }
        /// <summary>
        /// 页面Name
        /// </summary>
        public string MenuName { get; set; }
    }
}

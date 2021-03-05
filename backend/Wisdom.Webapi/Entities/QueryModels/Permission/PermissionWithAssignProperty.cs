using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Entities.QueryModels.Permission
{
    public class PermissionWithAssignProperty
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限关联的菜单Id
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 权限操作码
        /// </summary>
        public string ActionCode { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 权限是否已分配到当前角色
        /// </summary>
        public int IsAssigned { get; set; }
    }
}

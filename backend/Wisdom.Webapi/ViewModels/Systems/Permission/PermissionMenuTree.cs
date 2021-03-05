using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.ViewModels.Systems.Permission
{
    /// <summary>
    /// 用于角色权限的菜单树
    /// </summary>
    public class PermissionMenuTree
    {
        public PermissionMenuTree()
        {
            Permissions = new List<PermissionElement>();
            Children = new List<PermissionMenuTree>();
        }
        public int Id { get; set; }
        public int? ParentId { get; set; }
        /// <summary>
        /// 标题(菜单名称)
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否展开子节点
        /// </summary>
        public bool Expand { get; set; }
        /// <summary>
        /// 禁掉响应
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 禁掉 checkbox
        /// </summary>
        public bool DisableCheckbox { get; set; }
        /// <summary>
        /// 是否选中子节点	
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 是否勾选(如果勾选，子节点也会全部勾选)
        /// </summary>
        public bool Checked { get; set; }
        /// <summary>
        /// 当前菜单的所有权限都已分配到指定角色
        /// </summary>
        public bool AllAssigned { get; set; }
        /// <summary>
        /// 当前菜单拥有的权限功能
        /// </summary>
        public List<PermissionElement> Permissions { get; set; }
        /// <summary>
        /// 子节点属性数组
        /// </summary>
        public List<PermissionMenuTree> Children { get; set; }
    }

    public class PermissionElement
    {
        public int Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否已分配到指定角色
        /// </summary>
        public bool IsAssignedToRole { get; set; }
    }
}

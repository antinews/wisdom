using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.ViewModels.Systems.Mneu
{
    /// <summary>
    /// 用于Element-Tree
    /// </summary>
    public class MenuTree
    {
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 标题(菜单名称)
        /// </summary>
        public string Label { get; set; }
        public int Level { get; set; }
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
        /// 子节点属性数组
        /// </summary>
        public List<MenuTree> Children { get; set; }
    }
}

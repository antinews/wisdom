using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.ViewModels.Systems.Mneu
{
    public class MenuNode
    {
        public int Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string NavTitle { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LinkUrl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IconUrl { get; set; }

        /// <summary>
        /// Desc:是否显示
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int IsVisible { get; set; }

        /// <summary>
        /// Desc:父节点ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int IsPage { get; set; }
        public int Level { get; set; }
        public int Sort { get; set; }
        public string Component { get; set; }
        public string Redirect { get; set; }
        public int NoKeepAlive { get; set; }
        public string Badge { get; set; }
        public int IsAffix { get; set; }
        public List<MenuNode> Children { get; set; }
    }
}

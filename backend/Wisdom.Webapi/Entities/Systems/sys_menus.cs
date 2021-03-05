using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///系统菜单表
    ///</summary>
    [SugarTable("sys_menus")]
    public partial class sys_menus
    {
        public sys_menus()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Desc:菜单标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string NavTitle { get; set; }

        /// <summary>
        /// Desc:路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LinkUrl { get; set; }

        /// <summary>
        /// Desc:图标URL
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IconUrl { get; set; }

        /// <summary>
        /// Desc:是否显示，默认是
        /// Default:1
        /// Nullable:True
        /// </summary>           
        public int? IsVisible { get; set; }

        /// <summary>
        /// Desc:父节点ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        /// <summary>
        /// Desc:vue组件
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Component { get; set; }

        /// <summary>
        /// Desc:层级
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Level { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Sort { get; set; }

        /// <summary>
        /// Desc:菜单name
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:0
        /// </summary>           
        public int IsPage { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? CreatedUserId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ModifyUserId { get; set; }

        /// <summary>
        /// Desc:重定向
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Redirect { get; set; }

        /// <summary>
        /// Desc:不缓存？默认否
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? NoKeepAlive { get; set; }

        /// <summary>
        /// Desc:标记
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Badge { get; set; }

        /// <summary>
        /// Desc:是否固定
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? IsAffix { get; set; }

    }
}

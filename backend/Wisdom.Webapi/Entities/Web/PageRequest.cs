using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Web
{
    /// <summary>
    /// 列表获取参数(基类)
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Kw { get; set; } = "";
    }
}

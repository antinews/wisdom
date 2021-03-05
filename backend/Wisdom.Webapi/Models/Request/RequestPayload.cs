using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Models.Request
{
    /// <summary>
    /// 请求实体(基类)
    /// </summary>
    public class RequestPayload
    {
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNo { get; set; }
    }
}

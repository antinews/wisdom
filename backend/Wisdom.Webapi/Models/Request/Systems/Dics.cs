using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Models.Request.Systems
{
    public class Dics
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string   Title { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 字典描述
        /// </summary>
        public string Reamrk { get; set; }
        /// <summary>
        /// 批量ID
        /// </summary>
        public string IDs { get; set; }
    }
}

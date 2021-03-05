using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Models.Request.Systems
{
    public class Diccategorys
    {
        /// <summary>
        /// 字典类型主键
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 字典类型名
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 类型编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int SortNum { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserID { get; set; }

    }
}

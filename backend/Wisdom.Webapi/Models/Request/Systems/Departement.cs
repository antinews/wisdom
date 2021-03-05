using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Models.System.Departement
{
    public class Departement
    {

        public int Id { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public int createdUserId { get; set; }
        /// <summary>
        /// 部门名
        /// </summary>
        public string departmentName { get; set; }
        /// <summary>
        /// 上级ID
        /// </summary>
        public int parentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int sortnum { get; set; }

        public string IDs { get; set; }
    }
}

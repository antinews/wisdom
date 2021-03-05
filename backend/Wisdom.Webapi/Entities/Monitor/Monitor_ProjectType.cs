using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    /// <summary>
    /// 结构物类型
    /// </summary>
    public class Monitor_ProjectType
    {
        /// <summary>
        /// 结构物类型ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; } = "";
        /// <summary>
        /// 类型描述
        /// </summary>
        public string TypeDesc { get; set; } = "";
    }
}

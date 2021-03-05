using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    public class Monitor_Groups
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; } = "";

        /// <summary>
        /// 分组时间
        /// </summary>
        public string GroupDesc { get; set; } = "";
    }
}

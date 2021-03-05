using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    public class Monitor_Tags
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; } = "";

        /// <summary>
        /// 标签说明
        /// </summary>
        public string TagDesc { get; set; } = "";
    }
}

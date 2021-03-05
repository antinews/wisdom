using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Yun
{
    public class BsriMonitorCategory
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }
        /// <summary>
        /// CategoryCode
        /// </summary>
        /// <returns></returns>
        public string CategoryCode { get; set; }
    }
}

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Yun
{
    /// <summary>
    /// 云测点参数
    /// </summary>
    public class BsriSolveParam
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string ID { get; set; }
        /// <summary>
        /// 测点
        /// </summary>
        /// <returns></returns>
        public int PointId { get; set; }
        /// <summary>
        /// 监测类型ID
        /// </summary>
        /// <returns></returns>
        public string SolveType { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        /// <returns></returns>
        public string ParaName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        /// <returns></returns>
        public string ParaValue { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// ModifyUserId
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
    }
}

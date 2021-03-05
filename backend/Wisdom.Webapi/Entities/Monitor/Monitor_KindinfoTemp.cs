using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    public class Monitor_KindinfoTemp
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string KindName { get; set; } = "";
        /// <summary>
        /// 结构物类型Id
        /// </summary>
        public int ProjectType { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; } = "";
        /// <summary>
        /// 传感器类型代码
        /// </summary>
        public string KindCode { get; set; } = "";

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = "";

        /// <summary>
        /// 采集值精度
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public decimal MaxValue { get; set; }
    }
}

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    public class Monitor_Kindinfo
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string KindName { get; set; } = "";
        /// <summary>
        /// 结构物Id
        /// </summary>
        public int ProjectId { get; set; }

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
        /// <summary>
        /// 父tempId
        /// </summary>
        public int TempId { get; set; }
        /// <summary>
        /// 是否启用，0禁用，1启用
        /// </summary>

        public int IsActived { get; set; }

        /// <summary>
        /// 正常图标
        /// </summary>
        public string NormalIcon { get; set; }

        /// <summary>
        /// 不可以图标
        /// </summary>
        public string LoseIcon { get; set; }

        /// <summary>
        /// 一级警报图标
        /// </summary>
        public string EarlyWarningIcon { get; set; }

        /// <summary>
        /// 二级警报图标
        /// </summary>
        public string AlarmIcon { get; set; }
    }
}

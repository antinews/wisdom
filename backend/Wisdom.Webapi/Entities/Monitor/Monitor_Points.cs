using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    /// <summary>
    /// 测点
    /// </summary>
    public class Monitor_Points
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 云Id
        /// </summary>
        public int YunId { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DeviceName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 所属项目名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string ProjectName { get; set; } = "";

        [SugarColumn(IsIgnore = true)]
        public string Area { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string PointCode { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string PointName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string PointDesc { get; set; } = "";

        /// <summary>
        /// 初始通道：不可变更
        /// </summary>
        public int InitChanel { get; set; }

        /// <summary>
        /// 通道号，可以变更，以应对单通道故障的问题
        /// </summary>
        public int Chanel { get; set; }
        /// <summary>
        /// 是否启用，0禁用，1启用
        /// </summary>

        public int IsActived { get; set; }
        /// <summary>
        /// 公式ID
        /// </summary>
        public int ComputeFormulaId { get; set; } = 0;
        /// <summary>
        /// 测点照片/Uploads/Points/xxx.png
        /// </summary>
        public string Pic { get; set; } = "";

        /// <summary>
        /// 一级阈值下限
        /// </summary>
        public decimal Range1Min { get; set; } = 0m;
        /// <summary>
        /// 一级阈值上限
        /// </summary>
        public decimal Range1Max { get; set; } = 0m;
        /// <summary>
        /// 二级阈值下限
        /// </summary>
        public decimal Range2Min { get; set; } = 0m;
        /// <summary>
        /// 二级阈值上限
        /// </summary>
        public decimal Range2Max { get; set; } = 0m;
        /// <summary>
        /// 最小值
        /// </summary>
        public decimal MaxValue { get; set; } = 0m;
        /// <summary>
        /// 最大值
        /// </summary>
        public decimal MinValue { get; set; } = 0m;
        /// <summary>
        /// 合理值启用
        /// </summary>
        public int IsReasonableValidate { get; set; } = 0;
        /// <summary>
        /// 是否阈值报警
        /// </summary>
        public int IsThresholdAlarm { get; set; } = 0;
        /// <summary>
        /// 是否基准点
        /// </summary>
        public int IsBasePoint { get; set; } = 0;

        /// <summary>
        /// 计算公式
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Monitor_Computeformulainfo ComputeFormula { get; set; }

        public int MonitoringKindId { get; set; } = 0;
        /// <summary>
        /// 测点监测类型
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Monitor_Kindinfo MonitoringKind { get; set; }

        /// <summary>
        /// 测点扩展属性
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public PointExtendInfo PointExtend { get; set; }
    }
    public class Monitor_PointsView: Monitor_Points
    {
        [SugarColumn(IsIgnore = true)]
        public int Precision { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string Alias { get; set; } = "";
        [SugarColumn(IsIgnore = true)]
        public string EdgeMiddleAddr { get; set; } = "";

        [SugarColumn(IsIgnore = true)]
        public string Unit { get; set; } = "";
        /// <summary>
        /// 采样间隔，单位：毫秒
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public double CollectInterval { get; set; }
    }
    /// <summary>
    /// 测点扩展信息
    /// </summary>
    public class PointExtendInfo
    {
        public Monitor_Groups Group { get; set; }

        public Monitor_Tags Tag { get; set; }


    }
}

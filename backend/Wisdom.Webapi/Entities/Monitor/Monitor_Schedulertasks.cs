using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    public class Monitor_Schedulertasks
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public int DeviceId { get; set; }

        public string TaskName { get; set; } = "";

        /// <summary>
        /// 任务模式 Clocked 周期触发   Timing 定期触发
        /// </summary>
        public string TaskMode { get; set; } = "";

        /// <summary>
        /// 定期触发的周期 单位 毫秒
        /// </summary>
        public int ClockedInterval { get; set; }

        /// <summary>
        /// 定期触发方式 Hour 每小时  Day 每天 Week 每周 Month 每月
        /// </summary>
        public string TimingMode { get; set; } = "";

        /// <summary>
        /// 定期触发时间
        /// </summary>
        public string TimingTime { get; set; } = "";

        /// <summary>
        /// 跨语言执行的GRPC SERVER URL地址
        /// </summary>
        public string GrpcSrvUrl { get; set; } = "";

        /// <summary>
        /// 任务插件的方法  Native 原始代码  Hybird 混合代码执行
        /// </summary>
        public string TaskPluginMethod { get; set; } = "";

        public string TaskPluginLanguage { get; set; } = "";

        /// <summary>
        /// 插件执行文件
        /// </summary>
        public string TaskPluginFilePath { get; set; } = "";

        /// <summary>
        /// 执行方法  Native 原始代码  Hybird 混合代码执行
        /// </summary>
        public string ExecuteMethod { get; set; } = "";

        /// <summary>
        /// asp.net javscript python
        /// </summary>
        public string ExecuteLanguage { get; set; } = "";

        /// <summary>
        /// 执行文件路径
        /// </summary>
        public string ExecuteFilePath { get; set; } = "";
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsActived { get; set; }
        /// <summary>
        /// 任务说明
        /// </summary>
        public string TaskDesc { get; set; } = "";
        /// <summary>
        /// 执行状态 1 成功 0 异常
        /// </summary>
        public int ExecStauts { get; set; }
        /// <summary>
        /// 上次执行时间(可空时间类型)
        /// </summary>
        public DateTime? LastExecTime { get; set; }
        /// <summary>
        /// 执行参数 以key:value;的形式存入
        /// </summary>
        public string ExecuteParams { get; set; } = "";
        /// <summary>
        /// 测点ID集合，以,隔开
        /// </summary>
        public string ExecutePoint { get; set; } = "";
    }
    public class Monitor_SchedulertasksView: Monitor_Schedulertasks
    {
        [SugarColumn(IsIgnore = true)]
        public string pointNames { get; set; } = "";
    }
}

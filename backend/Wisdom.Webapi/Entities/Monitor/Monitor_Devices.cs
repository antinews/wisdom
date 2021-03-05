using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    public class Monitor_Devices
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 拉取的云平台Id
        /// </summary>
        public int YunId { get; set; } = 0;

        /// <summary>
        /// YunTableName
        /// </summary>
        public string YunTableName { get; set; } = "";
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; } = "";

        /// <summary>
        /// 设备说明
        /// </summary>
        public string DeviceDesc { get; set; } = "";

        /// <summary>
        /// 所属项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 所属项目名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string ProjectName { get; set; } = "";

        /// <summary>
        /// 所属项目名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Area { get; set; } = "";

        [SugarColumn(IsIgnore = true)]
        public int AreaId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string CodecsName { get; set; } = "";

        

        /// <summary>
        /// 网络接入协议 TCP UDP MQTT DTU（请前端直接在下拉框填入这些值，key-value等同）
        /// </summary>
        public string NetProtocol { get; set; } = "";

        /// <summary>
        /// 网络协议插件所在路径
        /// </summary>
        public string NetPluginPath { get; set; } = "";
        public int CodecsId { get; set; }

        /// <summary>
        /// 设备自身地址：设备有固定IP地址时才有效
        /// </summary>
        public string DeviceAddr { get; set; } = "";

        /// <summary>
        /// 设备自身端口：设备有固定IP地址时才有效
        /// </summary>
        public int DevicePort { get; set; }

        /// <summary>
        /// 设备上报地址
        /// </summary>
        public string ReportAddr { get; set; } = "";

        /// <summary>
        /// 设备上报端口
        /// </summary>
        public int ReportPort { get; set; }

        /// <summary>
        /// 边缘端中间件地址
        /// </summary>
        public string EdgeMiddleAddr { get; set; } = "";

        /// <summary>
        /// 边缘端中间件端口
        /// </summary>
        public int EdgeMiddlePort { get; set; }

        /// <summary>
        /// 设备最多接入监测点个数
        /// </summary>
        public int PointNums { get; set; }

        /// <summary>
        /// 上报方式：autoupload 自动上报 polling 定期轮询
        /// </summary>
        public string UploadMethod { get; set; } = "";

        /// <summary>
        /// 是否启用 编码器处理发送的采集指令 true 启用 false 不启用
        /// </summary>
        public bool ActivedCodecsCommand { get; set; } = false;

        /// <summary>
        /// 模块ID
        /// </summary>
        public string ModuleId { get; set; } = "";
        /// <summary>
        /// 采集指令 自动上报时字段为空，如果设备有子设备，以子设备集合的采集指令为准
        /// </summary>
        public string CollectCommand { get; set; } = "";

        /// <summary>
        /// 是否包含子设备集合 比如DTU设备，一个端口对应多个采集模块，每个模块有个采集指令（子设备）
        /// </summary>
        public bool HasDeviceSubset { get; set; }

        /// <summary>
        /// 协议解析
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Monitor_Codecs Codecs { get; set; } = new Monitor_Codecs();

        /// <summary>
        /// 子设备集合
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<DeviceSubsetInfo> DeviceSubsets { get; set; } = new List<DeviceSubsetInfo>();

        /// <summary>
        /// 采样间隔，单位：毫秒
        /// </summary>
        public double CollectInterval { get; set; }
        /// <summary>
        /// 最后采样时间
        /// </summary>
        public DateTime? LastCollectTime { get; set; }
        /// <summary>
        /// 测站照片/Uploads/Device/xxx.png
        /// </summary>
        public string Pic { get; set; } = "";

        /// <summary>
        /// 是否启用 1启用 0 不启用
        /// </summary>
        public int IsActived { get; set; }
        /// <summary>
        /// 设备状态 1上线 0 下线
        /// </summary>
        public int DeviceStatus { get; set; }

        /// <summary>
        /// 设备扩展信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public DeviceExtendInfo DeviceExtend { get; set; } = new DeviceExtendInfo();

        /// <summary>
        /// 设备应用配置信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Monitor_Devices_Appsystem DeviceAppSystem { get; set; } = new Monitor_Devices_Appsystem();

        /// <summary>
        /// 流式计算任务集
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Monitor_Streamtasks> StreamComputingTasks { get; set; } = new List<Monitor_Streamtasks>();


        /// <summary>
        /// 定时任务集
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Monitor_Schedulertasks> TaskSchedulerSettings { get; set; } = new List<Monitor_Schedulertasks>();

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Monitor_Points> Points { get; set; } = new List<Monitor_Points>();

    }/// <summary>
     /// 设备扩展信息
     /// </summary>
    public class DeviceExtendInfo
    {
        /// <summary>
        /// 设备分组
        /// </summary>
        public Monitor_Groups Group { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public Monitor_Tags Tag { get; set; }

    }

}

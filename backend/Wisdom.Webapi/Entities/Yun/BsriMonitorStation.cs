using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Yun
{
    /// <summary>
    /// 云平台测站
    /// </summary>
    public class BsriMonitorStation
    {
        /// <summary>
        /// 标识
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }
        /// <summary>
        /// 结构物ID
        /// </summary>
        /// <returns></returns>
        public int StructureId { get; set; }
        /// <summary>
        /// 采集/网关名称
        /// </summary>
        /// <returns></returns>
        public string StationName { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        /// <returns></returns>
        public string TelNo { get; set; }
        /// <summary>
        /// 解算方式
        /// </summary>
        /// <returns></returns>
        public int? SolveType { get; set; }
        /// <summary>
        /// 采样间隔时间
        /// </summary>
        /// <returns></returns>
        public double? Interval { get; set; }
        /// <summary>
        /// 采样频率
        /// </summary>
        /// <returns></returns>
        public Single? Rate { get; set; }
        /// <summary>
        /// 采样方式(0-连续采集；1-定时采集)
        /// </summary>
        /// <returns></returns>
        public int? TakeMode { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        /// <returns></returns>
        public string TakeTimes { get; set; }
        /// <summary>
        /// 模块号
        /// </summary>
        /// <returns></returns>
        public string ModuleID { get; set; }
        /// <summary>
        /// 指令
        /// </summary>
        /// <returns></returns>
        public string Command { get; set; }
        /// <summary>
        /// 存储表
        /// </summary>
        /// <returns></returns>
        public string TableName { get; set; }
        /// <summary>
        /// 已安装传感器数目
        /// </summary>
        /// <returns></returns>
        public int? GoodFieldNum { get; set; }
        /// <summary>
        /// 测站状态：0 未启动 （配置测站时） 1 已启动（守护进程启动采集程序，启动失败恢复0） 2 上线 3 未上线 4已发送未获取数据
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        /// <returns></returns>
        public string Pic { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// ModifyUserName
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }

        /// <summary>
        /// 远程采集端的IP
        /// </summary>
        public string RemoteAddress { get; set; }

        /// <summary>
        /// 远程采集端端口
        /// </summary>
        public int RemotePort { get; set; }

        /// <summary>
        /// 上下线报警 0否 1 是
        /// </summary>
        public int ActivedAlarm { get; set; }

        /// <summary>
        /// 上下线报警 0否 1 是
        /// </summary>
        public int DTUStyle { get; set; }

        /// <summary>
        /// 是否启用 0否 1 是
        /// </summary>
        public int IsActived { get; set; }

        /// <summary>
        /// 数据类型 1静态数据 2动态数据
        /// </summary>
        public int DeviceStyle { get; set; }

        /// <summary>
        /// 设备的IP
        /// </summary>
        public string DeviceAddress { get; set; }

        /// <summary>
        /// 设备端口
        /// </summary>
        public int DevicePort { get; set; }

        /// <summary>
        /// 事实数据采集地址
        /// </summary>
        public string WebSocketAddress { get; set; }

        /// <summary>
        /// 事实数据采集端口
        /// </summary>
        public int WebSocketPort { get; set; }

        /// <summary>
        /// 采集传输设备
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 原始数据库存储连接字符串
        /// </summary>
        /// <returns></returns>
        public string RawConString { get; set; }
        /// <summary>
        /// 处理数据库存储连接字符串
        /// </summary>
        /// <returns></returns>
        public string ProConString { get; set; }
        /// <summary>
        /// 实时数据库存储连接字符串
        /// </summary>
        /// <returns></returns>
        public string RealConString { get; set; }
        /// <summary>
        /// 分析数据库存储连接字符串
        /// </summary>
        /// <returns></returns>
        public string AnaConString { get; set; }

        public string OriConString { get; set; }
    }
    public class BsriMonitorStationView : BsriMonitorStation
    {
        /// <summary>
        /// 桥梁名称
        /// </summary>
        /// <returns></returns>
        public string StructureName { get; set; }
    }
}

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    /// <summary>
    /// 设备自诊断
    /// </summary>
    public class Monitor_DeviceStatus
    {
        /// <summary>
        /// 自增长ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceId{ get; set; }
        /// <summary>
        /// 网络状态：0异常/1正常
        /// </summary>
        public int NetworkStatus{ get; set; }
        /// <summary>
        /// 网络PING值
        /// </summary>
        public int Ping{ get; set; }
        /// <summary>
        /// 理论记录数量
        /// </summary>
        public double TheoreticalRecord{ get; set; }
        /// <summary>
        /// 实际记录数量
        /// </summary>
        public double RecordCount{ get; set; }
        /// <summary>
        /// 最后一次采集时间
        /// </summary>
        public DateTime? LastCollectTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModiifyDate { get; set; }

    }
    /// <summary>
    /// 设备自诊断扩展类
    /// </summary>
    public class Monitor_DeviceStatusView: Monitor_DeviceStatus
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备地址
        /// </summary>
        public string DeviceAddr { get; set; }
    }
}

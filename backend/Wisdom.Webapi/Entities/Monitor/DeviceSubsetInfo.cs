using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    public class DeviceSubsetInfo
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// 父设备ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; } = "";

        /// <summary>
        /// 模块ID
        /// </summary>
        public string ModuleId { get; set; } = "";

        /// <summary>
        /// 是否启用 编码器处理发送的采集指令 true 启用 false 不启用
        /// </summary>
        public bool ActivedCodecsCommand { get; set; } = false;

        /// <summary>
        /// 采集指令
        /// </summary>
        public string CollectCommand { get; set; } = "";

        /// <summary>
        /// 子设备的解算方式
        /// </summary>
        public Monitor_Codecs Codecs { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public List<Monitor_Points> Points { get; set; } = new List<Monitor_Points>();

        /// <summary>
        /// 子设备之间连续采集需要间隔的时间，单位毫秒，默认500毫秒
        /// </summary>
        public int CollectSleep { get; set; } = 500;

        /// <summary>
        /// 子设备最大接入的测点数量
        /// </summary>
        public int PointNums { get; set; }
    }
}

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    /// <summary>
    /// 结构物/项目
    /// </summary>
    public class Monitor_Projects
    {
        /// <summary>
        /// 结构物ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 结构名称
        /// </summary>
        public string ProjectName { get; set; } = "";

        /// <summary>
        /// 结构类型名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string ProjectTypeName { get; set; } = "";
        /// <summary>
        /// 结构物描述
        /// </summary>
        public string ProjectDesc { get; set; } = "";
        /// <summary>
        /// 项目封面图片
        /// </summary>
        public string ProjectCover { get; set; } = "";
        /// <summary>
        /// 项目所在的位置经度
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// 项目所在的位置纬度
        /// </summary>
        public decimal Latitude { get; set; }
        /// <summary>
        /// 位置全称
        /// </summary>
        public string Location { get; set; } = "";
        /// <summary>
        /// 位置省份
        /// </summary>
        public string Province { get; set; } = "";
        /// <summary>
        /// 位置城市
        /// </summary>
        public string City { get; set; } = "";
        /// <summary>
        /// 位置区域
        /// </summary>
        public string Area { get; set; } = "";
        /// <summary>
        /// CAD俯视图
        /// </summary>
        public string CADVerticalView { get; set; } = "";
        /// <summary>
        /// CAD正面图
        /// </summary>
        public string CADFrontView { get; set; } = "";
        /// <summary>
        /// 是否启用 1 启用 0 不启用
        /// </summary>
        public int IsActived { get; set; }
        /// <summary>
        /// 结构物类型
        /// </summary>
        public int ProjectType { get; set; }
        /// <summary>
        /// 测站个数
        /// </summary>
        public int DeviceNumber { get; set; }
        /// <summary>
        /// 异常测站个数
        /// </summary>
        public int AbnormalDeviceNumber { get; set; }
        /// <summary>
        /// 测点个数
        /// </summary>
        public int PointsNumber { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreateUserId { get; set; } = "";
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; } = "";
        public DateTime? ModifyDate { get; set; }

        public string Region { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Monitor_Devices_Appsystem DeviceAppSystem { get; set; } = new Monitor_Devices_Appsystem();

    }
}

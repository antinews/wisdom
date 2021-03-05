using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Yun
{
    /// <summary>
    /// 云平台测点
    /// </summary>
    public class BsriMonitorPoint
    {
        /// <summary>
        /// 标识
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 桥梁ID
        /// </summary>
        /// <returns></returns>
        public int StructureId { get; set; }
        /// <summary>
        /// 采集/网关标识
        /// </summary>
        /// <returns></returns>
        public int StationId { get; set; }
        /// <summary>
        /// 是否基准点 0 否 1是
        /// </summary>
        /// <returns></returns>
        public int? IsBase { get; set; }
        /// <summary>
        /// 初始值
        /// </summary>
        /// <returns></returns>
        public float InitValue { get; set; }
        /// <summary>
        /// 修正值（默认0）
        /// </summary>
        /// <returns></returns>
        public int? ReviseValue { get; set; }
        /// <summary>
        /// 分组ID
        /// </summary>
        /// <returns></returns>
        public string GroupId { get; set; }
        /// <summary>
        /// IsRelativeValue
        /// </summary>
        /// <returns></returns>
        public int? IsRelativeValue { get; set; }
        /// <summary>
        /// PointCode
        /// </summary>
        /// <returns></returns>
        public string PointCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string PointName { get; set; }
        /// <summary>
        /// 截面ID
        /// </summary>
        /// <returns></returns>
        public string CrossId { get; set; }
        /// <summary>
        /// 监测类型ID
        /// </summary>
        /// <returns></returns>
        public int? CategoryId { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        public string Unit { get; set; }
        /// <summary>
        /// 截面X坐标
        /// </summary>
        /// <returns></returns>
        public float CrossX { get; set; }
        /// <summary>
        /// 截面Y坐标
        /// </summary>
        /// <returns></returns>
        public float CrossY { get; set; }
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
        /// <summary>
        /// 分析数据库存储连接字符串
        /// </summary>
        public string OriConString { get; set; }
        /// <summary>
        /// 数据表
        /// </summary>
        /// <returns></returns>
        public string TableName { get; set; }
        /// <summary>
        /// 通道ID
        /// </summary>
        /// <returns></returns>
        public int? ChannelId { get; set; }
        /// <summary>
        /// 数据表字段col值
        /// </summary>
        /// <returns></returns>
        public string ColValue { get; set; }
        /// <summary>
        /// 是否进行合理性验证
        /// </summary>
        /// <returns></returns>
        public int? IsReasonableValidate { get; set; }
        /// <summary>
        /// 量程上限
        /// </summary>
        /// <returns></returns>
        public float? MaxValue { get; set; }
        /// <summary>
        /// 量程下限
        /// </summary>
        /// <returns></returns>
        public float? MinValue { get; set; }
        /// <summary>
        /// 是否进行阈值报警
        /// </summary>
        /// <returns></returns>
        public int? IsThresholdAlarm { get; set; }
        /// <summary>
        /// 0否 1是
        /// </summary>
        /// <returns></returns>
        public int? IsActived { get; set; }
        /// <summary>
        /// 报警间隔
        /// </summary>
        /// <returns></returns>
        public int? Interval { get; set; }
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
        /// 传感器ID
        /// </summary>
        public string SensorCategoryId { get; set; }

        /// <summary>
        /// 截面X坐标
        /// </summary>
        /// <returns></returns>
        public string PercentX { get; set; }
        /// <summary>
        /// 截面Y坐标
        /// </summary>
        /// <returns></returns>
        public string PercentY { get; set; }

        /// <summary>
        /// 监测类型
        /// </summary>
        /// <returns></returns>
        public int MonitorProjectListId { get; set; }
        /// <summary>
        /// 传感器设备
        /// </summary>
        /// <returns></returns>
        public string SensorId { get; set; }
        /// <summary>
        /// 初始值
        /// </summary>
        /// <returns></returns>
        public float NormalValue { get; set; }
        /// <summary>
        /// 修正值
        /// </summary>
        /// <returns></returns>
        public float AmendValue { get; set; }
        /// <summary>
        /// 一级阈值下限
        /// </summary>
        /// <returns></returns>
        public float? Range1Min { get; set; }
        /// <summary>
        /// 一级阈值上限
        /// </summary>
        /// <returns></returns>
        public float? Range1Max { get; set; }
        /// <summary>
        /// 二级阈值下限
        /// </summary>
        /// <returns></returns>
        public float? Range2Min { get; set; }
        /// <summary>
        /// 二级阈值上限
        /// </summary>
        /// <returns></returns>
        public float? Range2Max { get; set; }
    }
    public class BsriMonitorPointView : BsriMonitorPoint
    {
        /// <summary>
        /// 桥梁名称
        /// </summary>
        /// <returns></returns>
        public string StructureName { get; set; }

        /// <summary>
        /// 采集/网关名称
        /// </summary>
        /// <returns></returns>
        public string StationName { get; set; }
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
        /// CategoryCode
        /// </summary>
        /// <returns></returns>
        public string CategoryCode { get; set; }
    }
}

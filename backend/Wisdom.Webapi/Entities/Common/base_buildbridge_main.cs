using SqlSugar;
using System;

namespace Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class base_buildbridge_main
    {
        /// <summary>
        /// 
        /// </summary>
        public base_buildbridge_main()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int KeyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? BsriStructureId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? good { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ManageUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ManageUnitId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SelfManage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CityAreaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CityArea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeDes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? BridgeTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MaterialTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? SpanSum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PierSum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeDirection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Single? Xlongitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Single? YLatitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GPSDes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Dwg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FrontalViewPic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SideViewPic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MapPic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CADFrontalViewPic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CADSideViewPic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FireControlPic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SmallMileage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BigMileage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExpansionJointNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MaintainTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MaintainLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CheckUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ManageUnit1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CheckFrequency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MaintainUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ManageDept { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Transfer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TakeOver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SuperviseAttribute { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RemoveTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsConstruct { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StandardType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsMonitor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsLight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LightPic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LightLink { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DbServer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DbUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DbPw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? BhmCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CreatedUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsOne { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BuildUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BuildDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TakeOverDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HandOverDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DesignLoad { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Single? BridgeDeckArea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Single? AssetsValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HealthyState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TakeMeasures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? BuildSizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? BuildKindId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BCI { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? StructTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FSInstallationInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ResponsibilityCard { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LoadLimitCard { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LimitViaduct { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WidthLimitPier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeLight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeFire { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgePeople { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BridgeWidth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BCIData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IsPipei { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DiagBridge { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsDismantle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Single? InspectionRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PanoramaUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LiveActionUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BIMUrl { get; set; }
    }
}

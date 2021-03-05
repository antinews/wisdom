using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class base_buildbridge
    {
        /// <summary>
        /// 
        /// </summary>
        public base_buildbridge()
        {
        }

        private System.Int32 _KeyId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 KeyId { get { return this._KeyId; } set { this._KeyId = value; } }

        private System.Int32? _MainBridgeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? MainBridgeId { get { return this._MainBridgeId; } set { this._MainBridgeId = value; } }

        private System.Int32? _BsriStructureId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? BsriStructureId { get { return this._BsriStructureId; } set { this._BsriStructureId = value; } }

        private System.String _BridgeCode;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeCode { get { return this._BridgeCode; } set { this._BridgeCode = value; } }

        private System.String _BridgeName;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeName { get { return this._BridgeName; } set { this._BridgeName = value; } }

        private System.Int32? _IsDelete;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? IsDelete { get { return this._IsDelete; } set { this._IsDelete = value; } }

        private System.String _BridgeDes;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeDes { get { return this._BridgeDes; } set { this._BridgeDes = value; } }

        private System.String _Order;
        /// <summary>
        /// 
        /// </summary>
        public System.String Order { get { return this._Order; } set { this._Order = value; } }

        private System.String _BridgeType;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeType { get { return this._BridgeType; } set { this._BridgeType = value; } }

        private System.Int32? _BridgeTypeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? BridgeTypeId { get { return this._BridgeTypeId; } set { this._BridgeTypeId = value; } }

        private System.Int32? _MaterialTypeID;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? MaterialTypeID { get { return this._MaterialTypeID; } set { this._MaterialTypeID = value; } }

        private System.Int32? _SpanSum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? SpanSum { get { return this._SpanSum; } set { this._SpanSum = value; } }

        private System.Int32? _PierSum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PierSum { get { return this._PierSum; } set { this._PierSum = value; } }

        private System.Int32? _CityAreaId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? CityAreaId { get { return this._CityAreaId; } set { this._CityAreaId = value; } }

        private System.String _CityArea;
        /// <summary>
        /// 
        /// </summary>
        public System.String CityArea { get { return this._CityArea; } set { this._CityArea = value; } }

        private System.String _BridgeDirection;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeDirection { get { return this._BridgeDirection; } set { this._BridgeDirection = value; } }

        private System.String _InspectionPeople;
        /// <summary>
        /// 
        /// </summary>
        public System.String InspectionPeople { get { return this._InspectionPeople; } set { this._InspectionPeople = value; } }

        private System.String _BranchLeader;
        /// <summary>
        /// 
        /// </summary>
        public System.String BranchLeader { get { return this._BranchLeader; } set { this._BranchLeader = value; } }

        private System.String _Xlongitude;
        /// <summary>
        /// 
        /// </summary>
        public System.String Xlongitude { get { return this._Xlongitude; } set { this._Xlongitude = value; } }

        private System.String _YLatitude;
        /// <summary>
        /// 
        /// </summary>
        public System.String YLatitude { get { return this._YLatitude; } set { this._YLatitude = value; } }

        private System.String _GPSDes;
        /// <summary>
        /// 
        /// </summary>
        public System.String GPSDes { get { return this._GPSDes; } set { this._GPSDes = value; } }

        private System.String _Remark;
        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get { return this._Remark; } set { this._Remark = value; } }

        private System.String _Dwg;
        /// <summary>
        /// 
        /// </summary>
        public System.String Dwg { get { return this._Dwg; } set { this._Dwg = value; } }

        private System.String _FrontalViewPic;
        /// <summary>
        /// 
        /// </summary>
        public System.String FrontalViewPic { get { return this._FrontalViewPic; } set { this._FrontalViewPic = value; } }

        private System.String _SideViewPic;
        /// <summary>
        /// 
        /// </summary>
        public System.String SideViewPic { get { return this._SideViewPic; } set { this._SideViewPic = value; } }

        private System.String _FireControlPic;
        /// <summary>
        /// 
        /// </summary>
        public System.String FireControlPic { get { return this._FireControlPic; } set { this._FireControlPic = value; } }

        private System.String _SmallMileage;
        /// <summary>
        /// 
        /// </summary>
        public System.String SmallMileage { get { return this._SmallMileage; } set { this._SmallMileage = value; } }

        private System.String _BigMileage;
        /// <summary>
        /// 
        /// </summary>
        public System.String BigMileage { get { return this._BigMileage; } set { this._BigMileage = value; } }

        private System.String _ExpansionJointNum;
        /// <summary>
        /// 
        /// </summary>
        public System.String ExpansionJointNum { get { return this._ExpansionJointNum; } set { this._ExpansionJointNum = value; } }

        private System.String _BridgeLength;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeLength { get { return this._BridgeLength; } set { this._BridgeLength = value; } }

        private System.String _MaintainTypeName;
        /// <summary>
        /// 
        /// </summary>
        public System.String MaintainTypeName { get { return this._MaintainTypeName; } set { this._MaintainTypeName = value; } }

        private System.String _MaintainLevel;
        /// <summary>
        /// 
        /// </summary>
        public System.String MaintainLevel { get { return this._MaintainLevel; } set { this._MaintainLevel = value; } }

        private System.String _CheckUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.String CheckUnit { get { return this._CheckUnit; } set { this._CheckUnit = value; } }

        private System.String _ManageUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.String ManageUnit { get { return this._ManageUnit; } set { this._ManageUnit = value; } }

        private System.String _BridgeKind;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeKind { get { return this._BridgeKind; } set { this._BridgeKind = value; } }

        private System.Int32? _CheckFrequency;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? CheckFrequency { get { return this._CheckFrequency; } set { this._CheckFrequency = value; } }

        private System.Int32? _MaintainUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? MaintainUnit { get { return this._MaintainUnit; } set { this._MaintainUnit = value; } }

        private System.Int32? _ManageDept;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? ManageDept { get { return this._ManageDept; } set { this._ManageDept = value; } }

        private System.Int32? _Transfer;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? Transfer { get { return this._Transfer; } set { this._Transfer = value; } }

        private System.Int32? _TakeOver;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? TakeOver { get { return this._TakeOver; } set { this._TakeOver = value; } }

        private System.Int32? _SelfManage;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? SelfManage { get { return this._SelfManage; } set { this._SelfManage = value; } }

        private System.String _SuperviseAttribute;
        /// <summary>
        /// 
        /// </summary>
        public System.String SuperviseAttribute { get { return this._SuperviseAttribute; } set { this._SuperviseAttribute = value; } }

        private System.String _BridgeState;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeState { get { return this._BridgeState; } set { this._BridgeState = value; } }

        private System.String _RemoveTime;
        /// <summary>
        /// 
        /// </summary>
        public System.String RemoveTime { get { return this._RemoveTime; } set { this._RemoveTime = value; } }

        private System.Int32? _IsConstruct;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? IsConstruct { get { return this._IsConstruct; } set { this._IsConstruct = value; } }

        private System.String _StandardType;
        /// <summary>
        /// 
        /// </summary>
        public System.String StandardType { get { return this._StandardType; } set { this._StandardType = value; } }

        private System.Int32? _IsMonitor;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? IsMonitor { get { return this._IsMonitor; } set { this._IsMonitor = value; } }

        private System.Int32? _IsLight;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? IsLight { get { return this._IsLight; } set { this._IsLight = value; } }

        private System.String _LightPic;
        /// <summary>
        /// 
        /// </summary>
        public System.String LightPic { get { return this._LightPic; } set { this._LightPic = value; } }

        private System.String _LightLink;
        /// <summary>
        /// 
        /// </summary>
        public System.String LightLink { get { return this._LightLink; } set { this._LightLink = value; } }

        private System.String _DbServer;
        /// <summary>
        /// 
        /// </summary>
        public System.String DbServer { get { return this._DbServer; } set { this._DbServer = value; } }

        private System.String _DbName;
        /// <summary>
        /// 
        /// </summary>
        public System.String DbName { get { return this._DbName; } set { this._DbName = value; } }

        private System.String _DbUser;
        /// <summary>
        /// 
        /// </summary>
        public System.String DbUser { get { return this._DbUser; } set { this._DbUser = value; } }

        private System.String _DbPw;
        /// <summary>
        /// 
        /// </summary>
        public System.String DbPw { get { return this._DbPw; } set { this._DbPw = value; } }

        private System.Int32? _BhmCount;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? BhmCount { get { return this._BhmCount; } set { this._BhmCount = value; } }

        private System.Int32? _CreatedUserId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? CreatedUserId { get { return this._CreatedUserId; } set { this._CreatedUserId = value; } }

        private System.DateTime? _CreatedTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreatedTime { get { return this._CreatedTime; } set { this._CreatedTime = value; } }

        private System.Int32? _IsOne;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? IsOne { get { return this._IsOne; } set { this._IsOne = value; } }

        private System.Int32? _TunneLiningNum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? TunneLiningNum { get { return this._TunneLiningNum; } set { this._TunneLiningNum = value; } }

        private System.Int32? _TunnelRetainingWallNum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? TunnelRetainingWallNum { get { return this._TunnelRetainingWallNum; } set { this._TunnelRetainingWallNum = value; } }

        private System.Int32? _TunnelDeformationJointNum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? TunnelDeformationJointNum { get { return this._TunnelDeformationJointNum; } set { this._TunnelDeformationJointNum = value; } }

        private System.Int32? _TunnelEntranceNum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? TunnelEntranceNum { get { return this._TunnelEntranceNum; } set { this._TunnelEntranceNum = value; } }

        private System.Int32? _TunnelPavementNum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? TunnelPavementNum { get { return this._TunnelPavementNum; } set { this._TunnelPavementNum = value; } }

        private System.DateTime? _Generationtime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? Generationtime { get { return this._Generationtime; } set { this._Generationtime = value; } }

        private System.String _GenerationPeople;
        /// <summary>
        /// 
        /// </summary>
        public System.String GenerationPeople { get { return this._GenerationPeople; } set { this._GenerationPeople = value; } }
    }
}
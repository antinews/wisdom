using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class base_bridgecardinfo
    {
        /// <summary>
        /// 
        /// </summary>
        public base_bridgecardinfo()
        {
        }

        private System.Int32? _KeyId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? KeyId { get { return this._KeyId; } set { this._KeyId = value; } }

        private System.Int32? _BridgeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? BridgeId { get { return this._BridgeId; } set { this._BridgeId = value; } }

        private System.String _BridgeLocation;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeLocation { get { return this._BridgeLocation; } set { this._BridgeLocation = value; } }

        private System.String _BridgeCrossWay;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeCrossWay { get { return this._BridgeCrossWay; } set { this._BridgeCrossWay = value; } }

        private System.String _AreaId;
        /// <summary>
        /// 
        /// </summary>
        public System.String AreaId { get { return this._AreaId; } set { this._AreaId = value; } }

        private System.String _CardNumber;
        /// <summary>
        /// 
        /// </summary>
        public System.String CardNumber { get { return this._CardNumber; } set { this._CardNumber = value; } }

        private System.String _AuditedPersonId;
        /// <summary>
        /// 
        /// </summary>
        public System.String AuditedPersonId { get { return this._AuditedPersonId; } set { this._AuditedPersonId = value; } }

        private System.String _CheckerId;
        /// <summary>
        /// 
        /// </summary>
        public System.String CheckerId { get { return this._CheckerId; } set { this._CheckerId = value; } }

        private System.String _ListerId;
        /// <summary>
        /// 
        /// </summary>
        public System.String ListerId { get { return this._ListerId; } set { this._ListerId = value; } }

        private System.String _BuildCardDate;
        /// <summary>
        /// 
        /// </summary>
        public System.String BuildCardDate { get { return this._BuildCardDate; } set { this._BuildCardDate = value; } }

        private System.String _MaintainUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.String MaintainUnit { get { return this._MaintainUnit; } set { this._MaintainUnit = value; } }

        private System.String _BuildUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.String BuildUnit { get { return this._BuildUnit; } set { this._BuildUnit = value; } }

        private System.String _DesignUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.String DesignUnit { get { return this._DesignUnit; } set { this._DesignUnit = value; } }

        private System.String _SupervisingUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.String SupervisingUnit { get { return this._SupervisingUnit; } set { this._SupervisingUnit = value; } }

        private System.String _ConstructionUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.String ConstructionUnit { get { return this._ConstructionUnit; } set { this._ConstructionUnit = value; } }

        private System.String _BuildDate;
        /// <summary>
        /// 
        /// </summary>
        public System.String BuildDate { get { return this._BuildDate; } set { this._BuildDate = value; } }

        private System.String _StructType;
        /// <summary>
        /// 
        /// </summary>
        public System.String StructType { get { return this._StructType; } set { this._StructType = value; } }

        private System.String _DesignLoad;
        /// <summary>
        /// 
        /// </summary>
        public System.String DesignLoad { get { return this._DesignLoad; } set { this._DesignLoad = value; } }

        private System.String _SeismicFortificationIntensity;
        /// <summary>
        /// 
        /// </summary>
        public System.String SeismicFortificationIntensity { get { return this._SeismicFortificationIntensity; } set { this._SeismicFortificationIntensity = value; } }

        private System.String _Angle;
        /// <summary>
        /// 
        /// </summary>
        public System.String Angle { get { return this._Angle; } set { this._Angle = value; } }

        private System.Single? _BridgeSpanNumber;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? BridgeSpanNumber { get { return this._BridgeSpanNumber; } set { this._BridgeSpanNumber = value; } }

        private System.String _SpanGroup;
        /// <summary>
        /// 
        /// </summary>
        public System.String SpanGroup { get { return this._SpanGroup; } set { this._SpanGroup = value; } }

        private System.String _SpanMaxLength;
        /// <summary>
        /// 
        /// </summary>
        public System.String SpanMaxLength { get { return this._SpanMaxLength; } set { this._SpanMaxLength = value; } }

        private System.Single? _BridgeDeckArea;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? BridgeDeckArea { get { return this._BridgeDeckArea; } set { this._BridgeDeckArea = value; } }

        private System.Single? _BridgeLength;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? BridgeLength { get { return this._BridgeLength; } set { this._BridgeLength = value; } }

        private System.Single? _BridgeWidth;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? BridgeWidth { get { return this._BridgeWidth; } set { this._BridgeWidth = value; } }

        private System.Single? _RoadwayClearWidth;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? RoadwayClearWidth { get { return this._RoadwayClearWidth; } set { this._RoadwayClearWidth = value; } }

        private System.Single? _SidewayClearWidth;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? SidewayClearWidth { get { return this._SidewayClearWidth; } set { this._SidewayClearWidth = value; } }

        private System.String _RoadLevel;
        /// <summary>
        /// 
        /// </summary>
        public System.String RoadLevel { get { return this._RoadLevel; } set { this._RoadLevel = value; } }

        private System.Single? _DesignBedLevel;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? DesignBedLevel { get { return this._DesignBedLevel; } set { this._DesignBedLevel = value; } }

        private System.String _MaxWaterLevel;
        /// <summary>
        /// 
        /// </summary>
        public System.String MaxWaterLevel { get { return this._MaxWaterLevel; } set { this._MaxWaterLevel = value; } }

        private System.String _CityGirderForm;
        /// <summary>
        /// 
        /// </summary>
        public System.String CityGirderForm { get { return this._CityGirderForm; } set { this._CityGirderForm = value; } }

        private System.String _MainBeamForm;
        /// <summary>
        /// 
        /// </summary>
        public System.String MainBeamForm { get { return this._MainBeamForm; } set { this._MainBeamForm = value; } }

        private System.Single? _MainBeamLength;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? MainBeamLength { get { return this._MainBeamLength; } set { this._MainBeamLength = value; } }

        private System.Single? _MainBeamWidth;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? MainBeamWidth { get { return this._MainBeamWidth; } set { this._MainBeamWidth = value; } }

        private System.Single? _MainBeamHeight;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? MainBeamHeight { get { return this._MainBeamHeight; } set { this._MainBeamHeight = value; } }

        private System.Single? _MainBeamNumber;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? MainBeamNumber { get { return this._MainBeamNumber; } set { this._MainBeamNumber = value; } }

        private System.String _CrossBeamForm;
        /// <summary>
        /// 
        /// </summary>
        public System.String CrossBeamForm { get { return this._CrossBeamForm; } set { this._CrossBeamForm = value; } }

        private System.String _SupportForm;
        /// <summary>
        /// 
        /// </summary>
        public System.String SupportForm { get { return this._SupportForm; } set { this._SupportForm = value; } }

        private System.Single? _SupportNumber;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? SupportNumber { get { return this._SupportNumber; } set { this._SupportNumber = value; } }

        private System.String _SuperDeckStruct;
        /// <summary>
        /// 
        /// </summary>
        public System.String SuperDeckStruct { get { return this._SuperDeckStruct; } set { this._SuperDeckStruct = value; } }

        private System.String _ExpansionJointForm;
        /// <summary>
        /// 
        /// </summary>
        public System.String ExpansionJointForm { get { return this._ExpansionJointForm; } set { this._ExpansionJointForm = value; } }

        private System.Single? _ExpansionJointNumber;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? ExpansionJointNumber { get { return this._ExpansionJointNumber; } set { this._ExpansionJointNumber = value; } }

        private System.Single? _DeckElevation;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? DeckElevation { get { return this._DeckElevation; } set { this._DeckElevation = value; } }

        private System.Single? _BeamBottomElevation;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? BeamBottomElevation { get { return this._BeamBottomElevation; } set { this._BeamBottomElevation = value; } }

        private System.Single? _MainBridgeLongitudinalSlope;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? MainBridgeLongitudinalSlope { get { return this._MainBridgeLongitudinalSlope; } set { this._MainBridgeLongitudinalSlope = value; } }

        private System.Single? _MainBridgeCrossSlope;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? MainBridgeCrossSlope { get { return this._MainBridgeCrossSlope; } set { this._MainBridgeCrossSlope = value; } }

        private System.Single? _ApproachBridgeLongitudinalSlope;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? ApproachBridgeLongitudinalSlope { get { return this._ApproachBridgeLongitudinalSlope; } set { this._ApproachBridgeLongitudinalSlope = value; } }

        private System.String _ArchBridgeRiseSpanRatio;
        /// <summary>
        /// 
        /// </summary>
        public System.String ArchBridgeRiseSpanRatio { get { return this._ArchBridgeRiseSpanRatio; } set { this._ArchBridgeRiseSpanRatio = value; } }

        private System.Single? _TotalCost;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? TotalCost { get { return this._TotalCost; } set { this._TotalCost = value; } }

        private System.String _RailLength;
        /// <summary>
        /// 
        /// </summary>
        public System.String RailLength { get { return this._RailLength; } set { this._RailLength = value; } }

        private System.String _RailStruct;
        /// <summary>
        /// 
        /// </summary>
        public System.String RailStruct { get { return this._RailStruct; } set { this._RailStruct = value; } }

        private System.String _EndPostSize;
        /// <summary>
        /// 
        /// </summary>
        public System.String EndPostSize { get { return this._EndPostSize; } set { this._EndPostSize = value; } }

        private System.String _RevetmentType;
        /// <summary>
        /// 
        /// </summary>
        public System.String RevetmentType { get { return this._RevetmentType; } set { this._RevetmentType = value; } }

        private System.String _SlopeWallType;
        /// <summary>
        /// 
        /// </summary>
        public System.String SlopeWallType { get { return this._SlopeWallType; } set { this._SlopeWallType = value; } }

        private System.String _PierForm;
        /// <summary>
        /// 
        /// </summary>
        public System.String PierForm { get { return this._PierForm; } set { this._PierForm = value; } }

        private System.String _PierElevation;
        /// <summary>
        /// 
        /// </summary>
        public System.String PierElevation { get { return this._PierElevation; } set { this._PierElevation = value; } }

        private System.String _PierCapSize;
        /// <summary>
        /// 
        /// </summary>
        public System.String PierCapSize { get { return this._PierCapSize; } set { this._PierCapSize = value; } }

        private System.String _PierBaseElevation;
        /// <summary>
        /// 
        /// </summary>
        public System.String PierBaseElevation { get { return this._PierBaseElevation; } set { this._PierBaseElevation = value; } }

        private System.String _PierFloorSize;
        /// <summary>
        /// 
        /// </summary>
        public System.String PierFloorSize { get { return this._PierFloorSize; } set { this._PierFloorSize = value; } }

        private System.String _PierPileSize;
        /// <summary>
        /// 
        /// </summary>
        public System.String PierPileSize { get { return this._PierPileSize; } set { this._PierPileSize = value; } }

        private System.String _AbutmentForm;
        /// <summary>
        /// 
        /// </summary>
        public System.String AbutmentForm { get { return this._AbutmentForm; } set { this._AbutmentForm = value; } }

        private System.String _AbutmentElevation;
        /// <summary>
        /// 
        /// </summary>
        public System.String AbutmentElevation { get { return this._AbutmentElevation; } set { this._AbutmentElevation = value; } }

        private System.String _AbutmentBaseElevation;
        /// <summary>
        /// 
        /// </summary>
        public System.String AbutmentBaseElevation { get { return this._AbutmentBaseElevation; } set { this._AbutmentBaseElevation = value; } }

        private System.String _AbutmentCapSize;
        /// <summary>
        /// 
        /// </summary>
        public System.String AbutmentCapSize { get { return this._AbutmentCapSize; } set { this._AbutmentCapSize = value; } }

        private System.String _AbutmentFloorSize;
        /// <summary>
        /// 
        /// </summary>
        public System.String AbutmentFloorSize { get { return this._AbutmentFloorSize; } set { this._AbutmentFloorSize = value; } }

        private System.String _AbutmentPileSize;
        /// <summary>
        /// 
        /// </summary>
        public System.String AbutmentPileSize { get { return this._AbutmentPileSize; } set { this._AbutmentPileSize = value; } }

        private System.String _AbutmentRetainingPlankThickness;
        /// <summary>
        /// 
        /// </summary>
        public System.String AbutmentRetainingPlankThickness { get { return this._AbutmentRetainingPlankThickness; } set { this._AbutmentRetainingPlankThickness = value; } }

        private System.String _AbutmentWallForm;
        /// <summary>
        /// 
        /// </summary>
        public System.String AbutmentWallForm { get { return this._AbutmentWallForm; } set { this._AbutmentWallForm = value; } }

        private System.String _AbutmentWallLength;
        /// <summary>
        /// 
        /// </summary>
        public System.String AbutmentWallLength { get { return this._AbutmentWallLength; } set { this._AbutmentWallLength = value; } }

        private System.String _WaterPipe;
        /// <summary>
        /// 
        /// </summary>
        public System.String WaterPipe { get { return this._WaterPipe; } set { this._WaterPipe = value; } }

        private System.String _GasPipe;
        /// <summary>
        /// 
        /// </summary>
        public System.String GasPipe { get { return this._GasPipe; } set { this._GasPipe = value; } }

        private System.String _PowerCable;
        /// <summary>
        /// 
        /// </summary>
        public System.String PowerCable { get { return this._PowerCable; } set { this._PowerCable = value; } }

        private System.String _CommunicationCable;
        /// <summary>
        /// 
        /// </summary>
        public System.String CommunicationCable { get { return this._CommunicationCable; } set { this._CommunicationCable = value; } }

        private System.String _LayoutChart;
        /// <summary>
        /// 
        /// </summary>
        public System.String LayoutChart { get { return this._LayoutChart; } set { this._LayoutChart = value; } }

        private System.String _MainChannelArea;
        /// <summary>
        /// 
        /// </summary>
        public System.String MainChannelArea { get { return this._MainChannelArea; } set { this._MainChannelArea = value; } }

        private System.String _MainChannelLength;
        /// <summary>
        /// 
        /// </summary>
        public System.String MainChannelLength { get { return this._MainChannelLength; } set { this._MainChannelLength = value; } }

        private System.String _MainChannelWidth;
        /// <summary>
        /// 
        /// </summary>
        public System.String MainChannelWidth { get { return this._MainChannelWidth; } set { this._MainChannelWidth = value; } }

        private System.String _StairwayLength;
        /// <summary>
        /// 
        /// </summary>
        public System.String StairwayLength { get { return this._StairwayLength; } set { this._StairwayLength = value; } }

        private System.String _StairwayWidth;
        /// <summary>
        /// 
        /// </summary>
        public System.String StairwayWidth { get { return this._StairwayWidth; } set { this._StairwayWidth = value; } }

        private System.String _SlopeLength;
        /// <summary>
        /// 
        /// </summary>
        public System.String SlopeLength { get { return this._SlopeLength; } set { this._SlopeLength = value; } }

        private System.String _SlopeWidth;
        /// <summary>
        /// 
        /// </summary>
        public System.String SlopeWidth { get { return this._SlopeWidth; } set { this._SlopeWidth = value; } }

        private System.String _HeadRoomHeight;
        /// <summary>
        /// 
        /// </summary>
        public System.String HeadRoomHeight { get { return this._HeadRoomHeight; } set { this._HeadRoomHeight = value; } }

        private System.String _LiftLength;
        /// <summary>
        /// 
        /// </summary>
        public System.String LiftLength { get { return this._LiftLength; } set { this._LiftLength = value; } }

        private System.String _RoadType;
        /// <summary>
        /// 
        /// </summary>
        public System.String RoadType { get { return this._RoadType; } set { this._RoadType = value; } }

        private System.String _CeilingMaterial;
        /// <summary>
        /// 
        /// </summary>
        public System.String CeilingMaterial { get { return this._CeilingMaterial; } set { this._CeilingMaterial = value; } }

        private System.String _InsideMaterial;
        /// <summary>
        /// 
        /// </summary>
        public System.String InsideMaterial { get { return this._InsideMaterial; } set { this._InsideMaterial = value; } }

        private System.String _ImportExportNum;
        /// <summary>
        /// 
        /// </summary>
        public System.String ImportExportNum { get { return this._ImportExportNum; } set { this._ImportExportNum = value; } }

        private System.String _ManageUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.String ManageUnit { get { return this._ManageUnit; } set { this._ManageUnit = value; } }

        private System.String _SurveyUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.String SurveyUnit { get { return this._SurveyUnit; } set { this._SurveyUnit = value; } }

        private System.String _BridgeMaintainType;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeMaintainType { get { return this._BridgeMaintainType; } set { this._BridgeMaintainType = value; } }

        private System.Single? _CarSurfaceNumber;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? CarSurfaceNumber { get { return this._CarSurfaceNumber; } set { this._CarSurfaceNumber = value; } }

        private System.Single? _CarwayDeckArea;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? CarwayDeckArea { get { return this._CarwayDeckArea; } set { this._CarwayDeckArea = value; } }

        private System.String _HistoryDetectionInfo;
        /// <summary>
        /// 
        /// </summary>
        public System.String HistoryDetectionInfo { get { return this._HistoryDetectionInfo; } set { this._HistoryDetectionInfo = value; } }

        private System.String _HistoryMaintainInfo;
        /// <summary>
        /// 
        /// </summary>
        public System.String HistoryMaintainInfo { get { return this._HistoryMaintainInfo; } set { this._HistoryMaintainInfo = value; } }

        private System.String _FSInstallationInfo;
        /// <summary>
        /// 
        /// </summary>
        public System.String FSInstallationInfo { get { return this._FSInstallationInfo; } set { this._FSInstallationInfo = value; } }

        private System.String _DesignSpeed;
        /// <summary>
        /// 
        /// </summary>
        public System.String DesignSpeed { get { return this._DesignSpeed; } set { this._DesignSpeed = value; } }

        private System.Single? _SideWayDeckArea;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? SideWayDeckArea { get { return this._SideWayDeckArea; } set { this._SideWayDeckArea = value; } }

        private System.String _NavigationLevel;
        /// <summary>
        /// 
        /// </summary>
        public System.String NavigationLevel { get { return this._NavigationLevel; } set { this._NavigationLevel = value; } }

        private System.String _ElevationPhoto;
        /// <summary>
        /// 
        /// </summary>
        public System.String ElevationPhoto { get { return this._ElevationPhoto; } set { this._ElevationPhoto = value; } }

        private System.String _PositivePhoto;
        /// <summary>
        /// 
        /// </summary>
        public System.String PositivePhoto { get { return this._PositivePhoto; } set { this._PositivePhoto = value; } }

        private System.String _BCIData;
        /// <summary>
        /// 
        /// </summary>
        public System.String BCIData { get { return this._BCIData; } set { this._BCIData = value; } }

        private System.String _BCI;
        /// <summary>
        /// 
        /// </summary>
        public System.String BCI { get { return this._BCI; } set { this._BCI = value; } }

        private System.Single? _ResponsibilityCard;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? ResponsibilityCard { get { return this._ResponsibilityCard; } set { this._ResponsibilityCard = value; } }

        private System.String _LoadLimitCard;
        /// <summary>
        /// 
        /// </summary>
        public System.String LoadLimitCard { get { return this._LoadLimitCard; } set { this._LoadLimitCard = value; } }

        private System.String _LimitViaduct;
        /// <summary>
        /// 
        /// </summary>
        public System.String LimitViaduct { get { return this._LimitViaduct; } set { this._LimitViaduct = value; } }

        private System.String _WidthLimitPier;
        /// <summary>
        /// 
        /// </summary>
        public System.String WidthLimitPier { get { return this._WidthLimitPier; } set { this._WidthLimitPier = value; } }

        private System.Single? _BridgeLight;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? BridgeLight { get { return this._BridgeLight; } set { this._BridgeLight = value; } }

        private System.String _BridgeFire;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeFire { get { return this._BridgeFire; } set { this._BridgeFire = value; } }

        private System.String _BridgePeople;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgePeople { get { return this._BridgePeople; } set { this._BridgePeople = value; } }

        private System.Single? _BuildKindId;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? BuildKindId { get { return this._BuildKindId; } set { this._BuildKindId = value; } }

        private System.Single? _ManageUnitId;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? ManageUnitId { get { return this._ManageUnitId; } set { this._ManageUnitId = value; } }

        private System.Single? _StructTypeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? StructTypeId { get { return this._StructTypeId; } set { this._StructTypeId = value; } }

        private System.Single? _BuildSizeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? BuildSizeId { get { return this._BuildSizeId; } set { this._BuildSizeId = value; } }

        private System.String _TakeOverDate;
        /// <summary>
        /// 
        /// </summary>
        public System.String TakeOverDate { get { return this._TakeOverDate; } set { this._TakeOverDate = value; } }

        private System.String _HandOverDate;
        /// <summary>
        /// 
        /// </summary>
        public System.String HandOverDate { get { return this._HandOverDate; } set { this._HandOverDate = value; } }

        private System.Single? _AssetsValue;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? AssetsValue { get { return this._AssetsValue; } set { this._AssetsValue = value; } }

        private System.String _TakeMeasures;
        /// <summary>
        /// 
        /// </summary>
        public System.String TakeMeasures { get { return this._TakeMeasures; } set { this._TakeMeasures = value; } }

        private System.String _IsPipei;
        /// <summary>
        /// 
        /// </summary>
        public System.String IsPipei { get { return this._IsPipei; } set { this._IsPipei = value; } }

        private System.String _MaintainTypeName;
        /// <summary>
        /// 
        /// </summary>
        public System.String MaintainTypeName { get { return this._MaintainTypeName; } set { this._MaintainTypeName = value; } }

        private System.Int32? _TypeOK;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? TypeOK { get { return this._TypeOK; } set { this._TypeOK = value; } }

        private System.String _SteelArea;
        /// <summary>
        /// 
        /// </summary>
        public System.String SteelArea { get { return this._SteelArea; } set { this._SteelArea = value; } }

        private System.String _ConcreteArea;
        /// <summary>
        /// 
        /// </summary>
        public System.String ConcreteArea { get { return this._ConcreteArea; } set { this._ConcreteArea = value; } }

        private System.String _PierArea;
        /// <summary>
        /// 
        /// </summary>
        public System.String PierArea { get { return this._PierArea; } set { this._PierArea = value; } }

        private System.String _RailingArea;
        /// <summary>
        /// 
        /// </summary>
        public System.String RailingArea { get { return this._RailingArea; } set { this._RailingArea = value; } }

        private System.String _DeckArea;
        /// <summary>
        /// 
        /// </summary>
        public System.String DeckArea { get { return this._DeckArea; } set { this._DeckArea = value; } }

        private System.Int32? _IsDismantle;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? IsDismantle { get { return this._IsDismantle; } set { this._IsDismantle = value; } }
    }
}
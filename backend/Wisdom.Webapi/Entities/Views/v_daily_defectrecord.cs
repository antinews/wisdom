using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_daily_defectrecord
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_daily_defectrecord()
        {
        }

        private System.Int32 _KeyId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 KeyId { get { return this._KeyId; } set { this._KeyId = value; } }

        private System.String _BridgeName;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeName { get { return this._BridgeName; } set { this._BridgeName = value; } }

        private System.String _TrueName;
        /// <summary>
        /// 真名
        /// </summary>
        public System.String TrueName { get { return this._TrueName; } set { this._TrueName = value; } }

        private System.DateTime? _RecordTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? RecordTime { get { return this._RecordTime; } set { this._RecordTime = value; } }

        private System.String _ItemDescription;
        /// <summary>
        /// 
        /// </summary>
        public System.String ItemDescription { get { return this._ItemDescription; } set { this._ItemDescription = value; } }

        private System.String _RecordDescription;
        /// <summary>
        /// 
        /// </summary>
        public System.String RecordDescription { get { return this._RecordDescription; } set { this._RecordDescription = value; } }

        private System.String _Sound;
        /// <summary>
        /// 
        /// </summary>
        public System.String Sound { get { return this._Sound; } set { this._Sound = value; } }

        private System.String _Weather;
        /// <summary>
        /// 
        /// </summary>
        public System.String Weather { get { return this._Weather; } set { this._Weather = value; } }

        private System.Int32? _IsMonitorAlarm;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? IsMonitorAlarm { get { return this._IsMonitorAlarm; } set { this._IsMonitorAlarm = value; } }

        private System.String _MissionLevel;
        /// <summary>
        /// 
        /// </summary>
        public System.String MissionLevel { get { return this._MissionLevel; } set { this._MissionLevel = value; } }

        private System.String _MissionType;
        /// <summary>
        /// 
        /// </summary>
        public System.String MissionType { get { return this._MissionType; } set { this._MissionType = value; } }

        private System.String _ItemName;
        /// <summary>
        /// 
        /// </summary>
        public System.String ItemName { get { return this._ItemName; } set { this._ItemName = value; } }

        private System.String _Status;
        /// <summary>
        /// 
        /// </summary>
        public System.String Status { get { return this._Status; } set { this._Status = value; } }

        private System.String _StatusHandle;
        /// <summary>
        /// 
        /// </summary>
        public System.String StatusHandle { get { return this._StatusHandle; } set { this._StatusHandle = value; } }

        private System.Int32? _StatusId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? StatusId { get { return this._StatusId; } set { this._StatusId = value; } }

        private System.String _AreaName;
        /// <summary>
        /// 
        /// </summary>
        public System.String AreaName { get { return this._AreaName; } set { this._AreaName = value; } }

        private System.Int32? _CityAreaId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? CityAreaId { get { return this._CityAreaId; } set { this._CityAreaId = value; } }

        private System.Int32? _BridgeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? BridgeId { get { return this._BridgeId; } set { this._BridgeId = value; } }

        private System.Single? _XLongitude;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? XLongitude { get { return this._XLongitude; } set { this._XLongitude = value; } }

        private System.Single? _YLatitude;
        /// <summary>
        /// 
        /// </summary>
        public System.Single? YLatitude { get { return this._YLatitude; } set { this._YLatitude = value; } }

        private System.String _Pic;
        /// <summary>
        /// 
        /// </summary>
        public System.String Pic { get { return this._Pic; } set { this._Pic = value; } }

        private System.String _UserPositionDescription;
        /// <summary>
        /// 
        /// </summary>
        public System.String UserPositionDescription { get { return this._UserPositionDescription; } set { this._UserPositionDescription = value; } }

        private System.Int32? _UserId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? UserId { get { return this._UserId; } set { this._UserId = value; } }

        private System.String _Unit1;
        /// <summary>
        /// 
        /// </summary>
        public System.String Unit1 { get { return this._Unit1; } set { this._Unit1 = value; } }

        private System.Int32? _DepartmentId;
        /// <summary>
        /// 部门ID
        /// </summary>
        public System.Int32? DepartmentId { get { return this._DepartmentId; } set { this._DepartmentId = value; } }

        private System.String _DepartmentName;
        /// <summary>
        /// 部门名称
        /// </summary>
        public System.String DepartmentName { get { return this._DepartmentName; } set { this._DepartmentName = value; } }

        private System.DateTime? _CreatedTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreatedTime { get { return this._CreatedTime; } set { this._CreatedTime = value; } }

        private System.Int32? _OptionId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? OptionId { get { return this._OptionId; } set { this._OptionId = value; } }

        private System.Int32? _ItemId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? ItemId { get { return this._ItemId; } set { this._ItemId = value; } }

        private System.String _OptionName;
        /// <summary>
        /// 
        /// </summary>
        public System.String OptionName { get { return this._OptionName; } set { this._OptionName = value; } }

        private System.String _GUID;
        /// <summary>
        /// 
        /// </summary>
        public System.String GUID { get { return this._GUID; } set { this._GUID = value; } }

        private System.Int32 _bridge_id;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 bridge_id { get { return this._bridge_id; } set { this._bridge_id = value; } }

        private System.String _path;
        /// <summary>
        /// 
        /// </summary>
        public System.String path { get { return this._path; } set { this._path = value; } }

        private System.String _Unit2;
        /// <summary>
        /// 
        /// </summary>
        public System.String Unit2 { get { return this._Unit2; } set { this._Unit2 = value; } }

        private System.String _Unit3;
        /// <summary>
        /// 
        /// </summary>
        public System.String Unit3 { get { return this._Unit3; } set { this._Unit3 = value; } }

        private System.String _Unit;
        /// <summary>
        /// 
        /// </summary>
        public System.String Unit { get { return this._Unit; } set { this._Unit = value; } }

        private System.String _InspectionPeople;
        /// <summary>
        /// 
        /// </summary>
        public System.String InspectionPeople { get { return this._InspectionPeople; } set { this._InspectionPeople = value; } }

        private System.Int32? _HistoryDefectId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? HistoryDefectId { get { return this._HistoryDefectId; } set { this._HistoryDefectId = value; } }

        private System.String _ManageUnit;
        /// <summary>
        /// 
        /// </summary>
        public System.String ManageUnit { get { return this._ManageUnit; } set { this._ManageUnit = value; } }

        private System.String _IsCheck;
        /// <summary>
        /// 
        /// </summary>
        public System.String IsCheck { get { return this._IsCheck; } set { this._IsCheck = value; } }

        private System.String _CheckStatus;
        /// <summary>
        /// 
        /// </summary>
        public System.String CheckStatus { get { return this._CheckStatus; } set { this._CheckStatus = value; } }

        private System.DateTime? _TimeLimit;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? TimeLimit { get { return this._TimeLimit; } set { this._TimeLimit = value; } }

        private System.String _RepairRemark;
        /// <summary>
        /// 
        /// </summary>
        public System.String RepairRemark { get { return this._RepairRemark; } set { this._RepairRemark = value; } }

        private System.Int32? _BridgeTypeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? BridgeTypeId { get { return this._BridgeTypeId; } set { this._BridgeTypeId = value; } }

        private System.String _BridgeType;
        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeType { get { return this._BridgeType; } set { this._BridgeType = value; } }

        private System.String _OptionParentName;
        /// <summary>
        /// 
        /// </summary>
        public System.String OptionParentName { get { return this._OptionParentName; } set { this._OptionParentName = value; } }

        private System.Int32? _level;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? level { get { return this._level; } set { this._level = value; } }

        private System.Int32? _OptionParentId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? OptionParentId { get { return this._OptionParentId; } set { this._OptionParentId = value; } }

        private System.Int32? _MainBridgeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? MainBridgeId { get { return this._MainBridgeId; } set { this._MainBridgeId = value; } }

        private System.String _X;
        /// <summary>
        /// 
        /// </summary>
        public System.String X { get { return this._X; } set { this._X = value; } }

        private System.String _Y;
        /// <summary>
        /// 
        /// </summary>
        public System.String Y { get { return this._Y; } set { this._Y = value; } }

        private System.String _Z;
        /// <summary>
        /// 
        /// </summary>
        public System.String Z { get { return this._Z; } set { this._Z = value; } }
    }
}
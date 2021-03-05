using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class base_daily_inspectionoption
    {
        /// <summary>
        /// 
        /// </summary>
        public base_daily_inspectionoption()
        {
        }

        private System.Int32? _KeyId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? KeyId { get { return this._KeyId; } set { this._KeyId = value; } }

        private System.String _OptionName;
        /// <summary>
        /// 
        /// </summary>
        public System.String OptionName { get { return this._OptionName; } set { this._OptionName = value; } }

        private System.Int32? _OptionTypeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? OptionTypeId { get { return this._OptionTypeId; } set { this._OptionTypeId = value; } }

        private System.Int32? _ParentId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? ParentId { get { return this._ParentId; } set { this._ParentId = value; } }

        private System.Int32? _level;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? level { get { return this._level; } set { this._level = value; } }

        private System.String _ParentName;
        /// <summary>
        /// 
        /// </summary>
        public System.String ParentName { get { return this._ParentName; } set { this._ParentName = value; } }

        private System.Int32? _SnOrder;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? SnOrder { get { return this._SnOrder; } set { this._SnOrder = value; } }

        private System.Int32? _BridgeTypeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? BridgeTypeId { get { return this._BridgeTypeId; } set { this._BridgeTypeId = value; } }

        private System.Int32? _BuildTypeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? BuildTypeId { get { return this._BuildTypeId; } set { this._BuildTypeId = value; } }

        private System.String _GUID;
        /// <summary>
        /// 
        /// </summary>
        public System.String GUID { get { return this._GUID; } set { this._GUID = value; } }

        private System.DateTime? _CreatedTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreatedTime { get { return this._CreatedTime; } set { this._CreatedTime = value; } }
    }
}
using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class base_daily_inspectionitem
    {
        /// <summary>
        /// 
        /// </summary>
        public base_daily_inspectionitem()
        {
        }

        private System.Int32? _KeyId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? KeyId { get { return this._KeyId; } set { this._KeyId = value; } }

        private System.String _ItemName;
        /// <summary>
        /// 
        /// </summary>
        public System.String ItemName { get { return this._ItemName; } set { this._ItemName = value; } }

        private System.Int32? _OptionTypeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? OptionTypeId { get { return this._OptionTypeId; } set { this._OptionTypeId = value; } }

        private System.String _Unit;
        /// <summary>
        /// 
        /// </summary>
        public System.String Unit { get { return this._Unit; } set { this._Unit = value; } }

        private System.String _OptionName;
        /// <summary>
        /// 
        /// </summary>
        public System.String OptionName { get { return this._OptionName; } set { this._OptionName = value; } }

        private System.String _ParentName;
        /// <summary>
        /// 
        /// </summary>
        public System.String ParentName { get { return this._ParentName; } set { this._ParentName = value; } }

        private System.DateTime? _CreatedTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreatedTime { get { return this._CreatedTime; } set { this._CreatedTime = value; } }
    }
}
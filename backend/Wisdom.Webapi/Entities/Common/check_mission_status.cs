using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class check_mission_status
    {
        /// <summary>
        /// 
        /// </summary>
        public check_mission_status()
        {
        }

        private System.Int32 _KeyId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 KeyId { get { return this._KeyId; } set { this._KeyId = value; } }

        private System.String _Status;
        /// <summary>
        /// 
        /// </summary>
        public System.String Status { get { return this._Status; } set { this._Status = value; } }

        private System.String _Remark;
        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get { return this._Remark; } set { this._Remark = value; } }

        private System.DateTime? _CreatedTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreatedTime { get { return this._CreatedTime; } set { this._CreatedTime = value; } }

        private System.Int32? _orderNum;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? orderNum { get { return this._orderNum; } set { this._orderNum = value; } }

        private System.String _StatusMain;
        /// <summary>
        /// 
        /// </summary>
        public System.String StatusMain { get { return this._StatusMain; } set { this._StatusMain = value; } }

        private System.String _StatusHandle;
        /// <summary>
        /// 
        /// </summary>
        public System.String StatusHandle { get { return this._StatusHandle; } set { this._StatusHandle = value; } }

        private System.String _StatusProcess;
        /// <summary>
        /// 
        /// </summary>
        public System.String StatusProcess { get { return this._StatusProcess; } set { this._StatusProcess = value; } }
    }
}
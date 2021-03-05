using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class check_mission_defect_main
    {
        /// <summary>
        /// 
        /// </summary>
        public check_mission_defect_main()
        {
        }

        private System.Int32 _KeyId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 KeyId { get { return this._KeyId; } set { this._KeyId = value; } }

        private System.Int32? _DefectId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? DefectId { get { return this._DefectId; } set { this._DefectId = value; } }

        private System.Int32? _DefectIdType;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? DefectIdType { get { return this._DefectIdType; } set { this._DefectIdType = value; } }

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
    }
}
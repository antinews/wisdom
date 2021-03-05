using SqlSugar;

namespace Entitys
{
    /// <summary>
    /// VIEW
    /// </summary>
    public class v_base_bridgefilepath
    {
        /// <summary>
        /// VIEW
        /// </summary>
        public v_base_bridgefilepath()
        {
        }

        private System.String _path;
        /// <summary>
        /// 
        /// </summary>
        public System.String path { get { return this._path; } set { this._path = value; } }

        private System.Int32? _bridgeId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? bridgeId { get { return this._bridgeId; } set { this._bridgeId = value; } }

        private System.Int32 _MainId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 MainId { get { return this._MainId; } set { this._MainId = value; } }
    }
}
using SqlSugar;
using System;

namespace Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class base_ass_bridgetype
    {
        /// <summary>
        /// 
        /// </summary>
        public base_ass_bridgetype()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 KeyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String BridgeTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String BuildType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String StandardType { get; set; }
    }
}

using SqlSugar;
using System;

namespace Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public class base_cityarea
    {
        /// <summary>
        /// 
        /// </summary>
        public base_cityarea()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 KeyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String AreaName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String GUID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ShortName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? OrderNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? Code { get; set; }
    }
}


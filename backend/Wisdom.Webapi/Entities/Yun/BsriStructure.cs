using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Yun
{
    /// <summary>
    /// 云平台结构物
    /// </summary>
    public class BsriStructure
    {
        /// <summary>
        /// 标识
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int No { get; set; }
        /// <summary>
        /// 桥梁名称
        /// </summary>
        /// <returns></returns>
        public string StructureName { get; set; }
        /// <summary>
        /// 桥梁简称
        /// </summary>
        /// <returns></returns>
        public string ShortName { get; set; }
        /// <summary>
        /// 桥梁类型
        /// </summary>
        /// <returns></returns>
        public string Btype { get; set; }
       
        /// <summary>
        /// 所在省级
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// 所在地市级
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// 所在区县级
        /// </summary>
        /// <returns></returns>
        public string County { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        //[DisplayName("地址")]
        public string Place { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        /// <returns></returns>
        public Single? Xcool { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        /// <returns></returns>
        public Single? Ycool { get; set; }
    }
}

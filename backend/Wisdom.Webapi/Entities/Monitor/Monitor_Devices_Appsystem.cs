using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    public class Monitor_Devices_Appsystem
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        /// <summary>
        /// 边缘端使用的数据库类型 Mysql Taosdata Sqlite
        /// </summary>
        public string EdgeDatabaseType { get; set; } = "";

        /// <summary>
        /// 使用REST连接器时，数据库地址
        /// </summary>
        public string RestDataSource { get; set; } = "";

        /// <summary>
        /// 数据库链接
        /// </summary>
        public string DataSource { get; set; } = "";

        /// <summary>
        ///数据库
        /// </summary>
        public string Database { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; } = "";
    }
}

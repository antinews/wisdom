using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    /// <summary>
    /// 模块指令类
    /// </summary>
    public class Monitor_Command
    {
        /// <summary>
        /// 指令主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 所属协议
        /// </summary>
        public int CodecsId { get; set; }
        /// <summary>
        /// 模块号，请统一使用十六进制Hex
        /// </summary>
        public string ModuleId { get; set; } = "";
        /// <summary>
        /// 模块对应指令
        /// </summary>
        public string Command { get; set; } = "";
    }
}

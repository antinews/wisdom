using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    /// <summary>
    /// 协议类
    /// </summary>
    public class Monitor_Codecs
    {
        /// <summary>
        /// 协议解析ID，非自增，手动填写证整型
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int CodecsId { get; set; }

        /// <summary>
        /// 协议解析名称
        /// </summary>
        public string CodecsName { get; set; } = "";

        /// <summary>
        /// 原生代码解析时，使用的类名 CommandFangKongZhenXian
        /// </summary>
        public string ClassName { get; set; } = "";

        /// <summary>
        /// 协议说明
        /// </summary>
        public string CodescDesc { get; set; } = "";

        /// <summary>
        /// 协议解析方式：Native 内置插件解析 Hybird 混合脚本解析
        /// </summary>
        public string CodecsMethod { get; set; } = "";

        /// <summary>
        /// 解析插件所在路径
        /// </summary>
        public string CodecsPluginPath { get; set; } = "";

        /// <summary>
        /// 动态解析选择的语言 javascript / python
        /// </summary>
        public string CodecsLanguage { get; set; } = "";

        public string CodecsScriptPath { get; set; } = "";
        /// <summary>
        /// 默认测点个数
        /// </summary>
        public int DefaultNumber { get; set; }

    }
}

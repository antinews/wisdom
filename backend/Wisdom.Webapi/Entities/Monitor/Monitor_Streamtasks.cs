using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    public class Monitor_Streamtasks
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public int DeviceId { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string TaskName { get; set; } = "";

        /// <summary>
        /// 规则说明
        /// </summary>
        public string TaskDesc { get; set; } = "";

        /// <summary>
        /// 执行方法  Native 原始代码  Hybird 混合代码执行
        /// </summary>
        public string ExecuteMethod { get; set; } = "";

        /// <summary>
        /// asp.net javscript python
        /// </summary>
        public string ExecuteLanguage { get; set; } = "";

        /// <summary>
        /// 执行文件路径
        /// </summary>
        public string ExecuteFilePath { get; set; } = "";
        /// <summary>
        /// 执行参数；以KEY=VALUE的形式表示
        /// </summary>
        public string ExecuteParams { get; set; } = "";
        /// <summary>
        /// 是否启用 1 启用 0不启用
        /// </summary>
        public int IsActived { get; set; }
    }
}

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edge.WebApi.Entity.Monitor
{
    public class Monitor_Computeformulainfo
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 公式名称
        /// </summary>
        public string FormulaName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string FormulaDesc { get; set; } = "";
        public string FormulaKey
        {
            get;
            set;
        } = "";

        private List<string> formulakeys;
        /// <summary>
        /// 参数
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<string> FormulaKeys
        {
            get
            {
                if (this.formulakeys == null || this.formulakeys.Count == 0)
                {
                    return string.IsNullOrEmpty(FormulaKey) ? null :FormulaKey.Split(",").ToList();
                }
                return this.formulakeys;
            }
            set
            {
                formulakeys = value;
            }
        }

        public string FormulaValue
        {
            get;
            set;
        } = "";
        private List<double> formulavalues;
        /// <summary>
        /// 参数值
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<double> FormulaValues
        {
            get
            {
                if (this.formulavalues == null || this.formulavalues.Count == 0)
                {
                    return string.IsNullOrEmpty(FormulaValue)?null: FormulaValue.Split(",").Select(s => Convert.ToDouble(s)).ToList();
                }
                return this.formulavalues;
            }
            set
            {
                formulavalues = value;
            }
        }

    }
}

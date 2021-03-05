using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.Entities.Monitor
{
    public class monitor_area : ModelContext
    {

        [SugarColumn(IsPrimaryKey = true)]
        public int id { get; set; }

        public string name { get; set; }

        public int parentID { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<monitor_area> childnode
        {
            get
            {
                return base.CreateMapping<monitor_area>().Where(it => it.parentID == this.id).ToList();
            }
        }
    }
}


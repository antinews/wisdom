using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wisdom.Webapi.ViewModels.Systems.Mneu
{
    public class MenuItem
    {
        //public int Id { get; set; }
        //public int ParentId { get; set; }
        public string Path { get; set; }
        public string Component { get; set; }
        public string Redirect { get; set; }
        public string Name { get; set; }
        public MenuMeta Meta { get; set; }
        public bool Hidden { get; set; }
        public List<MenuItem> Children { get; set; }
    }

    public class MenuMeta
    {
        public bool NoKeepAlive { get; set; }
        public bool Affix { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Badge { get; set; }
    }
}

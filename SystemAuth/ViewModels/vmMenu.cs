using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemAuth.ViewModels
{
    public class VmMenu
    {
        public string Path { get; set; }
        public string MenuText { get; set; }        
        public string Component { get; set; }
        public string Selector { get; set; }
        public List<VmMenu> Children { get; set; }
    }
}

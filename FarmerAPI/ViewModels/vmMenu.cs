using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmerAPI.ViewModels
{
    public class vmMenu
    {
        public string Path { get; set; }
        public string MenuText { get; set; }        
        public string Component { get; set; }
        public List<vmMenu> Children { get; set; }
    }
}

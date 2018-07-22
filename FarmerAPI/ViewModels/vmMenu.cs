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
        public string Selector { get; set; }
        public List<vmMenu> Children { get; set; }
    }

    public class Test
    {
        public string Path { get; set; }
        public string MenuText { get; set; }
        public string Component { get; set; }
        public int? RoleId { get; set; }
    }

    public class Test2DB
    {
        public int? TestPk { get; set; }
        public string Test1 { get; set; }
        public string Test2 { get; set; }
        public string ActionName { get; set; }
    }
}

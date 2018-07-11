using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmerAPI.ViewModels
{
    public class vmImenuRole
    {
    }

    public class MenuNode
    {
        public string MenuText { get; set; }
        public int MenuId { get; set; }
        public List<MenuNode> Children { get; set; }
    }
}

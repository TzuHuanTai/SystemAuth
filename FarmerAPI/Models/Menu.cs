using System;
using System.Collections.Generic;

namespace FarmerAPI.Models
{
    public partial class Menu
    {
        public int MenuId { get; set; }
        public string MenuText { get; set; }
        public int SortNo { get; set; }
        public string Component { get; set; }
        public int? RootMenuId { get; set; }
    }
}

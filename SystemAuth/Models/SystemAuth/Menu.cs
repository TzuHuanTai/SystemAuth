﻿using System;
using System.Collections.Generic;

namespace SystemAuth.Models
{
    public partial class Menu
    {
        public Menu()
        {
            ImenuRole = new HashSet<ImenuRole>();
        }

        public int MenuId { get; set; }
        public string Path { get; set; }
        public string MenuText { get; set; }
        public int SortNo { get; set; }
        public string Selector { get; set; }
        public string Component { get; set; }
        public int? RootMenuId { get; set; }
        public int AppId { get; set; }

        public App App { get; set; }
        public ICollection<ImenuRole> ImenuRole { get; set; }
    }
}

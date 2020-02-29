using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class Menu
    {
        public Menu()
        {
            IMenuRole = new HashSet<IMenuRole>();
        }

        public int MenuId { get; set; }
        public string Path { get; set; }
        public string MenuText { get; set; }
        public int? SortNo { get; set; }
        public string Selector { get; set; }
        public string Component { get; set; }
        public int? RootMenuId { get; set; }
        public int? AppId { get; set; }

        public virtual App App { get; set; }
        public virtual ICollection<IMenuRole> IMenuRole { get; set; }
    }
}

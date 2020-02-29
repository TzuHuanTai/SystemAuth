using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class App
    {
        public App()
        {
            Ctrl = new HashSet<Ctrl>();
            Menu = new HashSet<Menu>();
        }

        public int AppId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Ctrl> Ctrl { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
    }
}

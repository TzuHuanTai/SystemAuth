using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SqlServer
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

        public ICollection<Ctrl> Ctrl { get; set; }
        public ICollection<Menu> Menu { get; set; }
    }
}

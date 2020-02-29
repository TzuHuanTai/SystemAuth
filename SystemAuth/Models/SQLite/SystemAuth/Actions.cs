using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class Actions
    {
        public Actions()
        {
            IActionRole = new HashSet<IActionRole>();
        }

        public int ActionId { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }
        public int ControllerId { get; set; }
        public string Description { get; set; }

        public virtual Ctrl Controller { get; set; }
        public virtual ICollection<IActionRole> IActionRole { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class Ctrl
    {
        public Ctrl()
        {
            Actions = new HashSet<Actions>();
            ICtrlRole = new HashSet<ICtrlRole>();
        }

        public int CtrlId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AppId { get; set; }

        public virtual App App { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }
        public virtual ICollection<ICtrlRole> ICtrlRole { get; set; }
    }
}

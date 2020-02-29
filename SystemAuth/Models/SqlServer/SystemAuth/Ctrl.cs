using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SqlServer
{
    public partial class Ctrl
    {
        public Ctrl()
        {
            Actions = new HashSet<Actions>();
            IctrlRole = new HashSet<IctrlRole>();
        }

        public int CtrlId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AppId { get; set; }

        public App App { get; set; }
        public ICollection<Actions> Actions { get; set; }
        public ICollection<IctrlRole> IctrlRole { get; set; }
    }
}

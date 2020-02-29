using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class ICtrlRole
    {
        public int ControllerId { get; set; }
        public int RoleId { get; set; }

        public virtual Ctrl Controller { get; set; }
        public virtual RoleGroup Role { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SqlServer
{
    public partial class IctrlRole
    {
        public int ControllerId { get; set; }
        public int RoleId { get; set; }

        public Ctrl Controller { get; set; }
        public RoleGroup Role { get; set; }
    }
}

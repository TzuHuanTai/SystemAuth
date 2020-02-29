using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SqlServer
{
    public partial class IactionRole
    {
        public int ActionId { get; set; }
        public int RoleId { get; set; }

        public Actions Action { get; set; }
        public RoleGroup Role { get; set; }
    }
}

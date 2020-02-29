using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class IActionRole
    {
        public int ActionId { get; set; }
        public int RoleId { get; set; }

        public virtual Actions Action { get; set; }
        public virtual RoleGroup Role { get; set; }
    }
}

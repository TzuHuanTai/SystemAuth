using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class IMenuRole
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual RoleGroup Role { get; set; }
    }
}

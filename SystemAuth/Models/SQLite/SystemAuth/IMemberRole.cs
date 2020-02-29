using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class IMemberRole
    {
        public string Account { get; set; }
        public int RoleId { get; set; }

        public virtual Member AccountNavigation { get; set; }
        public virtual RoleGroup Role { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SqlServer
{
    public partial class ImenuRole
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }

        public Menu Menu { get; set; }
        public RoleGroup Role { get; set; }
    }
}

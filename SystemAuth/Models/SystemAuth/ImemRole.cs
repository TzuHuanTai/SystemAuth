﻿using System;
using System.Collections.Generic;

namespace SystemAuth.Models
{
    public partial class ImemRole
    {
        public string Account { get; set; }
        public int RoleId { get; set; }

        public Member AccountNavigation { get; set; }
        public RoleGroup Role { get; set; }
    }
}

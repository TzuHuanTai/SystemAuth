using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SqlServer
{
    public partial class SystemLog
    {
        public long Num { get; set; }
        public string Account { get; set; }
        public string Route { get; set; }
        public string Method { get; set; }
        public string Action { get; set; }
        public string Detail { get; set; }
        public string Ip { get; set; }
        public DateTime LogTime { get; set; }

        public Member AccountNavigation { get; set; }
    }
}

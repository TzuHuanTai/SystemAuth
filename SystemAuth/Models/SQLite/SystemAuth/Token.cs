using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class Token
    {
        public string Account { get; set; }
        public DateTime AuthTime { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string TokenCode { get; set; }
        public string Ip { get; set; }

        public virtual Member AccountNavigation { get; set; }
    }
}

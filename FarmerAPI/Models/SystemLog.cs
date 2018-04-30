using System;
using System.Collections.Generic;

namespace FarmerAPI.Models
{
    public partial class SystemLog
    {
        public long Num { get; set; }
        public DateTime LogTime { get; set; }
        public string UserId { get; set; }
        public string Domain { get; set; }
        public string Action { get; set; }
        public string Detail { get; set; }
    }
}

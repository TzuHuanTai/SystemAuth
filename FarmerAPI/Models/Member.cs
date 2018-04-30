using System;
using System.Collections.Generic;

namespace FarmerAPI.Models
{
    public partial class Member
    {
        public string Domain { get; set; }
        public string Name { get; set; }
        public string DeptId { get; set; }
        public string MemId { get; set; }
        public string MemPw { get; set; }
        public bool IsActive { get; set; }
    }
}

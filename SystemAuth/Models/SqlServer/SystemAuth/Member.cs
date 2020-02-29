using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SqlServer
{
    public partial class Member
    {
        public Member()
        {
            ImemRole = new HashSet<ImemRole>();
            SystemLog = new HashSet<SystemLog>();
        }

        public string Account { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Domain { get; set; }
        public string DeptId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public Token Token { get; set; }
        public ICollection<ImemRole> ImemRole { get; set; }
        public ICollection<SystemLog> SystemLog { get; set; }
    }
}

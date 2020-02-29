using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class Member
    {
        public Member()
        {
            IMemberRole = new HashSet<IMemberRole>();
            SystemLog = new HashSet<SystemLog>();
        }

        public string Account { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Domain { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public virtual Token Token { get; set; }
        public virtual ICollection<IMemberRole> IMemberRole { get; set; }
        public virtual ICollection<SystemLog> SystemLog { get; set; }
    }
}

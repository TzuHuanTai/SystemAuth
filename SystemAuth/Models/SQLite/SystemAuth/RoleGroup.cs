using System;
using System.Collections.Generic;

namespace SystemAuth.Models.SQLite
{
    public partial class RoleGroup
    {
        public RoleGroup()
        {
            IActionRole = new HashSet<IActionRole>();
            ICtrlRole = new HashSet<ICtrlRole>();
            IMemberRole = new HashSet<IMemberRole>();
            IMenuRole = new HashSet<IMenuRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int? SortNo { get; set; }
        public int? AccessScope { get; set; }
        public string Description { get; set; }
        public int? ParentRoleId { get; set; }
        public bool? RejectScope { get; set; }
        public bool? ApproveScope { get; set; }
        public bool? SubmitScope { get; set; }
        public bool? PassScope { get; set; }
        public bool? PrintScope { get; set; }

        public virtual ICollection<IActionRole> IActionRole { get; set; }
        public virtual ICollection<ICtrlRole> ICtrlRole { get; set; }
        public virtual ICollection<IMemberRole> IMemberRole { get; set; }
        public virtual ICollection<IMenuRole> IMenuRole { get; set; }
    }
}

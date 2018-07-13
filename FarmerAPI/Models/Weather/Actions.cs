using System;
using System.Collections.Generic;

namespace FarmerAPI.Models
{
    public partial class Actions
    {
        public Actions()
        {
            IactionRole = new HashSet<IactionRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }
        public int? ControllerId { get; set; }
        public string Description { get; set; }

        public Controllers Controller { get; set; }
        public ICollection<IactionRole> IactionRole { get; set; }
    }
}

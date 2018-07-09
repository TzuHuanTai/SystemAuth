using System;
using System.Collections.Generic;

namespace FarmerAPI.Models
{
    public partial class Actions
    {
        public Actions()
        {
            IactionAllowed = new HashSet<IactionAllowed>();
        }

        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public string Method { get; set; }
        public int? ControllerId { get; set; }
        public string Description { get; set; }

        public Controllers Controller { get; set; }
        public ICollection<IactionAllowed> IactionAllowed { get; set; }
    }
}

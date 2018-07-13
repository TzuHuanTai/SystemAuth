using System;
using System.Collections.Generic;

namespace FarmerAPI.Models
{
    public partial class Controllers
    {
        public Controllers()
        {
            Actions = new HashSet<Actions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Actions> Actions { get; set; }
    }
}

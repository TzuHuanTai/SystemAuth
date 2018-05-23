using System;
using System.Collections.Generic;

namespace FarmerAPI.Models
{
    public partial class City
    {
        public City()
        {
            StationInfo = new HashSet<StationInfo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<StationInfo> StationInfo { get; set; }
    }
}

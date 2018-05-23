using System;
using System.Collections.Generic;

namespace FarmerAPI.Models
{
    public partial class StationInfo
    {
        public StationInfo()
        {
            WeatherData = new HashSet<WeatherData>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CityId { get; set; }
        public string Address { get; set; }

        public City City { get; set; }
        public ICollection<WeatherData> WeatherData { get; set; }
    }
}

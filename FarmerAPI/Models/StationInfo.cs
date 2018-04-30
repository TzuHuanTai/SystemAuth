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

        public int Num { get; set; }
        public string Name { get; set; }
        public int? CityId { get; set; }

        public StationInfo NumNavigation { get; set; }
        public StationInfo InverseNumNavigation { get; set; }
        public ICollection<WeatherData> WeatherData { get; set; }
    }
}

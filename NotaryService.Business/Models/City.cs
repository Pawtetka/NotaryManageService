using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class City
    {
        public City()
        {
            Locations = new HashSet<Location>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityType { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class Location
    {
        public Location()
        {
            Offices = new HashSet<Office>();
        }

        public int LocationId { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Office> Offices { get; set; }
    }
}

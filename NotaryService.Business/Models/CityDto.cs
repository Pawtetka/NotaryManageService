using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class CityDto
    {
        public CityDto()
        {
            Locations = new HashSet<LocationDto>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityType { get; set; }

        public virtual ICollection<LocationDto> Locations { get; set; }
    }
}

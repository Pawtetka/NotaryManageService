using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class LocationDto
    {
        public LocationDto()
        {
            Offices = new HashSet<OfficeDto>();
        }

        public int LocationId { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }

        public virtual CityDto City { get; set; }
        public virtual ICollection<OfficeDto> Offices { get; set; }
    }
}

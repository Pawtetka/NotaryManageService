using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class OfficeDto
    {
        public OfficeDto()
        {
            Workers = new HashSet<WorkerDto>();
        }

        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeStatus { get; set; }
        public string OfficeSize { get; set; }
        public int LocationId { get; set; }

        public virtual LocationDto Location { get; set; }
        public virtual ICollection<WorkerDto> Workers { get; set; }
    }
}

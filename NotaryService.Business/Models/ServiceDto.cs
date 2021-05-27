using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class ServiceDto
    {
        public ServiceDto()
        {
            WorkerServices = new HashSet<WorkerServiceDto>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Complexity { get; set; }
        public string Importance { get; set; }

        public virtual ICollection<WorkerServiceDto> WorkerServices { get; set; }
    }
}

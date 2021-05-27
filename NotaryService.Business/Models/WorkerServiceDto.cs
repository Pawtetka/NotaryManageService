using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class WorkerServiceDto
    {
        public int WorkerServiceId { get; set; }
        public int WorkerId { get; set; }
        public int ServiceId { get; set; }

        public virtual ServiceDto Service { get; set; }
        public virtual WorkerDto Worker { get; set; }
    }
}

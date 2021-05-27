using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class Service
    {
        public Service()
        {
            WorkerServices = new HashSet<WorkerService>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Complexity { get; set; }
        public string Importance { get; set; }

        public virtual ICollection<WorkerService> WorkerServices { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryDatabaseDLL.Models
{
    public partial class WorkerService
    {
        public int WorkerServiceId { get; set; }
        public int WorkerId { get; set; }
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
        public virtual Worker Worker { get; set; }
    }
}

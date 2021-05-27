using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class AssistantDto
    {
        public int AssistantId { get; set; }
        public string Specialization { get; set; }
        public int WorkerId { get; set; }
        public int? NotaryId { get; set; }

        public virtual NotaryDto Notary { get; set; }
        public virtual WorkerDto Worker { get; set; }
    }
}

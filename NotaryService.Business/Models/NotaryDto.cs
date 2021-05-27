using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class NotaryDto
    {
        public NotaryDto()
        {
            Assistants = new HashSet<AssistantDto>();
            Receptions = new HashSet<ReceptionDto>();
        }

        public int NotaryId { get; set; }
        public string CertificateNumber { get; set; }
        public int WorkerId { get; set; }

        public virtual WorkerDto Worker { get; set; }
        public virtual ICollection<AssistantDto> Assistants { get; set; }
        public virtual ICollection<ReceptionDto> Receptions { get; set; }
    }
}

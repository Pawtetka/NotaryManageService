using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class DocumentDto
    {
        public DocumentDto()
        {
            Receptions = new HashSet<ReceptionDto>();
        }

        public int DocumentId { get; set; }
        public string DocumentName { get; set; }

        public virtual ICollection<ReceptionDto> Receptions { get; set; }
    }
}

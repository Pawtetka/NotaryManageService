using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class ReceptionDto
    {
        public int ReceptionId { get; set; }
        public DateTime ReceptionDate { get; set; }
        public int Price { get; set; }
        public int NotaryId { get; set; }
        public int ClientId { get; set; }
        public int? DocumentId { get; set; }

        public virtual ClientDto Client { get; set; }
        public virtual DocumentDto Document { get; set; }
        public virtual NotaryDto Notary { get; set; }
    }
}

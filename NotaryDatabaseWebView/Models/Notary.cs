using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryDatabaseWebView.Models
{
    public partial class Notary
    {
        public Notary()
        {
            Assistants = new HashSet<Assistant>();
            Receptions = new HashSet<Reception>();
        }

        public int NotaryId { get; set; }
        public string CertificateNumber { get; set; }
        public int WorkerId { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual ICollection<Assistant> Assistants { get; set; }
        public virtual ICollection<Reception> Receptions { get; set; }
    }
}

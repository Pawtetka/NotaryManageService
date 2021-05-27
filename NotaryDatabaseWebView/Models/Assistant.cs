using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryDatabaseWebView.Models
{
    public partial class Assistant
    {
        public int AssistantId { get; set; }
        public string Specialization { get; set; }
        public int WorkerId { get; set; }
        public int? NotaryId { get; set; }

        public virtual Notary Notary { get; set; }
        public virtual Worker Worker { get; set; }
    }
}

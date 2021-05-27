using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryDatabaseWebView.Models
{
    public partial class Document
    {
        public Document()
        {
            Receptions = new HashSet<Reception>();
        }

        public int DocumentId { get; set; }
        public string DocumentName { get; set; }

        public virtual ICollection<Reception> Receptions { get; set; }
    }
}

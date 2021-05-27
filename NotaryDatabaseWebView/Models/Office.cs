using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryDatabaseWebView.Models
{
    public partial class Office
    {
        public Office()
        {
            Workers = new HashSet<Worker>();
        }

        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeStatus { get; set; }
        public string OfficeSize { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}

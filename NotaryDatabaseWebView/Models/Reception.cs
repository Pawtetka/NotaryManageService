using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryDatabaseWebView.Models
{
    public partial class Reception
    {
        public int ReceptionId { get; set; }
        public DateTime ReceptionDate { get; set; }
        public int Price { get; set; }
        public int NotaryId { get; set; }
        public int ClientId { get; set; }
        public int? DocumentId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Document Document { get; set; }
        public virtual Notary Notary { get; set; }
    }
}

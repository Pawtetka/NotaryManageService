using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryDatabaseDLL.Models
{
    public partial class Client
    {
        public Client()
        {
            Receptions = new HashSet<Reception>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PassportNumber { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Reception> Receptions { get; set; }
    }
}

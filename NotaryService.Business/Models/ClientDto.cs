using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class ClientDto
    {
        public ClientDto()
        {
            Receptions = new HashSet<ReceptionDto>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PassportNumber { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<ReceptionDto> Receptions { get; set; }
    }
}

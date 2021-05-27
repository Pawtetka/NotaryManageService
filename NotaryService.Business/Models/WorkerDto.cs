using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryService.Models
{
    public partial class WorkerDto
    {
        public WorkerDto()
        {
            Assistants = new HashSet<AssistantDto>();
            Notaries = new HashSet<NotaryDto>();
            WorkerServices = new HashSet<WorkerServiceDto>();
        }

        public int WorkerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PassportNumber { get; set; }
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public int OfficeId { get; set; }

        public virtual OfficeDto Office { get; set; }
        public virtual ICollection<AssistantDto> Assistants { get; set; }
        public virtual ICollection<NotaryDto> Notaries { get; set; }
        public virtual ICollection<WorkerServiceDto> WorkerServices { get; set; }
    }
}

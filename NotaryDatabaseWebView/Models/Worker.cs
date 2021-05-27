using System;
using System.Collections.Generic;

#nullable disable

namespace NotaryDatabaseWebView.Models
{
    public partial class Worker
    {
        public Worker()
        {
            Assistants = new HashSet<Assistant>();
            Notaries = new HashSet<Notary>();
            WorkerServices = new HashSet<WorkerService>();
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

        public virtual Office Office { get; set; }
        public virtual ICollection<Assistant> Assistants { get; set; }
        public virtual ICollection<Notary> Notaries { get; set; }
        public virtual ICollection<WorkerService> WorkerServices { get; set; }
    }
}

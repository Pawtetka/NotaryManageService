using FluentValidation;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Validation
{
    public class WorkerValidator : AbstractValidator<Worker>
    {
        public WorkerValidator()
        {
            RuleFor(a => a.WorkerId).NotNull();
            RuleFor(a => a.Age).NotEmpty();
            RuleFor(a => a.FirstName).NotEmpty();
            RuleFor(a => a.LastName).NotEmpty();
            RuleFor(a => a.HireDate).NotEmpty();
            RuleFor(a => a.OfficeId).NotNull();
            RuleFor(a => a.PassportNumber).NotEmpty();
            RuleFor(a => a.PhoneNumber).NotEmpty();
        }
    }
}

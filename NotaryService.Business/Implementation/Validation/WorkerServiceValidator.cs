using FluentValidation;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Validation
{
    public class WorkerServiceValidator : AbstractValidator<WorkerService>
    {
        public WorkerServiceValidator()
        {
            RuleFor(a => a.WorkerServiceId).NotNull();
            RuleFor(a => a.WorkerId).NotEmpty();
            RuleFor(a => a.ServiceId).NotEmpty();
        }
    }
}

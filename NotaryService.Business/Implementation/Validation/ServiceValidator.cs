using FluentValidation;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Validation
{
    public class ServiceValidator : AbstractValidator<Service>
    {
        public ServiceValidator()
        {
            RuleFor(a => a.ServiceId).NotNull();
            RuleFor(a => a.ServiceName).NotEmpty();
            RuleFor(a => a.Importance).NotEmpty();
            RuleFor(a => a.Complexity).NotEmpty();
        }
    }
}

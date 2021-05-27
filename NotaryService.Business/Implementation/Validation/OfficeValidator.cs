using FluentValidation;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Validation
{
    public class OfficeValidator : AbstractValidator<Office>
    {
        public OfficeValidator()
        {
            RuleFor(a => a.OfficeId).NotNull();
            RuleFor(a => a.OfficeName).NotEmpty();
            RuleFor(a => a.OfficeSize).NotEmpty();
            RuleFor(a => a.OfficeStatus).NotEmpty();
        }
    }
}

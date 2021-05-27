using FluentValidation;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Validation
{
    public class ReceptionValidator : AbstractValidator<Reception>
    {
        public ReceptionValidator()
        {
            RuleFor(a => a.ReceptionId).NotNull();
            RuleFor(a => a.Price).NotEmpty();
            RuleFor(a => a.ReceptionDate).NotEmpty();
            RuleFor(a => a.ClientId).NotEmpty();
            RuleFor(a => a.NotaryId).NotEmpty();
        }
    }
}

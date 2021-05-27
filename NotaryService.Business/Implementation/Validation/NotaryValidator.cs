using FluentValidation;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Validation
{
    public class NotaryValidator : AbstractValidator<Notary>
    {
        public NotaryValidator()
        {
            RuleFor(a => a.NotaryId).NotNull();
            RuleFor(a => a.CertificateNumber).NotEmpty();
            RuleFor(a => a.Assistants).NotEmpty();
            RuleFor(a => a.Receptions).NotEmpty();
            RuleFor(a => a.WorkerId).NotEmpty();
        }
    }
}

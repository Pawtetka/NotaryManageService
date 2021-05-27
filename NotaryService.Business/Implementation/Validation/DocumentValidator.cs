using FluentValidation;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Validation
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            RuleFor(a => a.DocumentId).NotNull();
            RuleFor(a => a.DocumentName).NotEmpty();
            RuleFor(a => a.Receptions).NotEmpty();
        }
    }
}

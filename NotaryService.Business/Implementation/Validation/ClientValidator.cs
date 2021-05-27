using FluentValidation;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Validation
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(a => a.ClientId).NotNull();
            RuleFor(a => a.FirstName).NotEmpty();
            RuleFor(a => a.LastName).NotEmpty();
            RuleFor(a => a.PassportNumber).NotEmpty();
            RuleFor(a => a.PhoneNumber).NotEmpty();
            RuleFor(a => a.Age).NotEmpty();
            RuleFor(a => a.Receptions).NotEmpty();
        }
    }
}

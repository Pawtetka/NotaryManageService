using FluentValidation;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Validation
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(a => a.CityId).NotNull();
            RuleFor(a => a.CityName).NotEmpty();
            RuleFor(a => a.CityType).NotEmpty();
            RuleFor(a => a.Locations).NotEmpty();
        }
    }
}

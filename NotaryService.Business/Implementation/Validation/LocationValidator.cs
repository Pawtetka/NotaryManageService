using FluentValidation;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Validation
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(a => a.LocationId).NotNull();
            RuleFor(a => a.Address).NotEmpty();
            RuleFor(a => a.CityId).NotEmpty();
            RuleFor(a => a.Offices).NotEmpty();
        }
    }
}

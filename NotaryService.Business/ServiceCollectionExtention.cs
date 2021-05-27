using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NotaryDatabaseDLL.Models;
using NotaryService.Business.Abstraction;
using NotaryService.Business.Implementation.Services;
using NotaryService.Business.Implementation.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBllServices(this IServiceCollection services)
        {

            services.AddTransient<ICrudInterface<City>, CitiesService>();


            services.AddTransient<AbstractValidator<City>, CityValidator>();

            return services;
        }
    }
}

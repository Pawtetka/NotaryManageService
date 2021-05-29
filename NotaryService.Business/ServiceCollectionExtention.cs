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
            services.AddTransient<ICrudInterface<Client>, ClientsService>();
            services.AddTransient<ICrudInterface<Document>, DocumentsService>();
            services.AddTransient<ICrudInterface<Location>, LocationsService>();
            services.AddTransient<ICrudInterface<Notary>, NotariesService>();
            services.AddTransient<ICrudInterface<Office>, OfficesService>();
            services.AddTransient<ICrudInterface<Reception>, ReceptionsService>();
            services.AddTransient<ICrudInterface<Service>, ServicesService>();
            services.AddTransient<ICrudInterface<WorkerService>, WorkerServicesService>();
            services.AddTransient<ICrudInterface<Worker>, WorkersService>();


            services.AddTransient<AbstractValidator<City>, CityValidator>();
            services.AddTransient<AbstractValidator<Client>, ClientValidator>();
            services.AddTransient<AbstractValidator<Document>, DocumentValidator>();
            services.AddTransient<AbstractValidator<Location>, LocationValidator>();
            services.AddTransient<AbstractValidator<Notary>, NotaryValidator>();
            services.AddTransient<AbstractValidator<Office>, OfficeValidator>();
            services.AddTransient<AbstractValidator<Reception>, ReceptionValidator>();
            services.AddTransient<AbstractValidator<Service>, ServiceValidator>();
            services.AddTransient<AbstractValidator<Worker>, WorkerValidator>();
            services.AddTransient<AbstractValidator<WorkerService>, WorkerServiceValidator>();

            return services;
        }
    }
}

using Application.Features.Customers.Commands.Rules;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ServiceRegistration 
    {
        public static void AddApplicationServices(this IServiceCollection service) 
        {
            service.AddFluentValidationAutoValidation();
            service.AddFluentValidationClientsideAdapters();
            service.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
        }

    }
}

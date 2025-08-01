﻿using bms.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;

namespace bms.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // reg all services in the project dynamically
            var assembly = typeof(DependencyInjection).Assembly;

            // register mediator
            services.AddMediatR(configuration => {
                configuration.RegisterServicesFromAssembly(assembly);
            });

            // register validators using FluentValidation
            services.AddValidatorsFromAssembly(assembly);

            //register auto mapper
            //services.AddAutoMapper(assembly);

            // register pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

            return services;
        }
    }
}

namespace Application.Services.Extensions
{
    using Application.Services.Interfaces;
    using Application.Services.Requests;
    using Application.Services.Validators;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICalculatorFactory, CalculatorFactory>();
            services.AddScoped<IVATCalculator, NetCalculator>();
            services.AddScoped<IVATCalculator, GrossCalculator>();
            services.AddScoped<IVATCalculator, VATCalculator>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<VATRequest>, VATRequestValidator>();

            return services;
        }
    }
}

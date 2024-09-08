namespace Presentation.API.Extensions
{
    using Microsoft.OpenApi.Models;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Austrian VAT Calculator API",
                    Description = "An API to calculate VAT for Austrian purchases"
                });
            });

            return services;
        }
    }
}

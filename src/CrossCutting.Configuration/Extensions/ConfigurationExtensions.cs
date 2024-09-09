namespace CrossCutting.Configuration.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class ConfigurationExtensions
    {
        private const string VATSection = "VAT";

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<VATConfiguration>(configuration.GetSection(VATSection));

            return services;
        }
    }
}

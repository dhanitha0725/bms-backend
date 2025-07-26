using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bms.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

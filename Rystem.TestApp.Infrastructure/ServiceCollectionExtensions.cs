using Microsoft.Extensions.DependencyInjection;
using Rystem.TestApp.Domain;

namespace Rystem.TestApp.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStorageForApp(this IServiceCollection services)
        {
            services.AddRepository<SimpleModel, string>(settings =>
            {
                settings
                    .SetStorage<SimpleModelRepository>();
            });
            return services;
        }
    }
}
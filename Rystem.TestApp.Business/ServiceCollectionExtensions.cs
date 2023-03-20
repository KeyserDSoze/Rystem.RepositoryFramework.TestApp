using Microsoft.Extensions.DependencyInjection;
using Rystem.TestApp.Domain;

namespace Rystem.TestApp.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessForApp(this IServiceCollection services)
        {
            services
                .AddBusinessForRepository<SimpleModel, string>()
                .AddBusinessBeforeGet<SimpleModelBeforeGet>();
            return services;
        }
        public static IServiceCollection AddCacheForApp(this IServiceCollection services)
        {
            services
                .AddMemoryCache();
            services
                .AddCacheForRepository<SimpleModel, string>()
                .WithInMemoryCache();
            return services;
        }
    }
}
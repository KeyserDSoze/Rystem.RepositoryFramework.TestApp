using Microsoft.Extensions.DependencyInjection;
using Rystem.TestApp.Domain;

namespace Rystem.TestApp.Infrastructure.BlobStorageIntegration
{
    public static class BlobStorageServiceCollectionExtensions
    {
        public static IServiceCollection AddStorageWithBlobStorageForApp(this IServiceCollection services)
        {
            services.AddRepository<SimpleModel, string>(settings =>
            {
                settings
                    .WithBlobStorage(configuration =>
                    {
                        configuration.ConnectionString = "connectionstring";
                        configuration.ContainerName = "container name, the name of the class is the default value";
                        configuration.Prefix = "VirtualPath/InnerVirtualPath/AnotherVirtualPath/";
                    });
            });
            return services;
        }
    }
}
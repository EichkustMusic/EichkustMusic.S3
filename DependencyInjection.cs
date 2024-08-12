using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EichkustMusic.S3
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddS3(
            this IServiceCollection services, IConfigurationManager configuration)
        {
            return services.AddSingleton<IS3Storage>(serviceProvider =>
                new S3Storage(configuration));
        }
    }
}

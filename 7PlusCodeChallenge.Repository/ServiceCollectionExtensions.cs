using _7PlusCodeChallenge.Contracts.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _7PlusCodeChallenge.Repository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddRepositoryConfiguration(configuration)
                .AddScoped<IUserRepository, ApiUserRepository>()
                .AddScoped<IParser, JsonParser>()
                .AddSingleton<ICacheAdapter, MemoryCacheAdapter>();
        }

        private static IServiceCollection AddRepositoryConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped(x => configuration.GetSection("ApiRepositoryOptions").Get<ApiUserRepositoryOptions>());
        }
    }
}

using _7PlusCodeChallenge.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace _7PlusCodeChallenge.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserService, UserService>();
        }
    }
}

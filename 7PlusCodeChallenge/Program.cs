using System;
using System.Threading.Tasks;
using _7PlusCodeChallenge.Contracts.Services;
using _7PlusCodeChallenge.Repository;
using _7PlusCodeChallenge.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _7PlusCodeChallenge
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddRepositories(configuration)
                .AddServices()
                .BuildServiceProvider();

            var userService = serviceProvider.GetRequiredService<IUserService>();

            var user = await userService.GetUserAsync(42);

            if (user != null)
            {
                Console.WriteLine($"{user.LastName}, {user.FirstName}");
                Console.WriteLine();
            }

            var firstNamesOf23YearOlds = await userService.GetFirstNamesForAgeAsync(23);

            if (firstNamesOf23YearOlds?.Names != null)
            {
                Console.WriteLine(string.Join(',', firstNamesOf23YearOlds.Names));
                Console.WriteLine();
            }

            var genders = await userService.GetGendersByAgeAsync();

            if (genders?.Models != null)
            {
                foreach (var model in genders.Models)
                {
                    Console.WriteLine($"Age: {model.Age} Female: {model.Female} Male: {model.Male}");
                }
            }
        }
    }
}


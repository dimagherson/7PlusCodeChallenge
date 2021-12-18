using System.Threading;
using System.Threading.Tasks;
using _7PlusCodeChallenge.Contracts.Models;

namespace _7PlusCodeChallenge.Contracts.Services
{
    public interface IUserService
    {
        public Task<UserModel> GetUserAsync(int userId, CancellationToken cancellationToken = default);
        public Task<NameCollectionModel> GetFirstNamesForAgeAsync(int age, CancellationToken cancellationToken = default);
        public Task<GenderByAgeCollectionModel> GetGendersByAgeAsync(CancellationToken cancellationToken = default);
    }
}

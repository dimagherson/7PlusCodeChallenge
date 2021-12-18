using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace _7PlusCodeChallenge.Contracts.Repository
{
    public interface IUserRepository
    {
        public Task<IList<User>> GetAsync(CancellationToken cancellationToken);
        public Task<IList<User>> GetByAgeAsync(int age, CancellationToken cancellationToken);
        public Task<User> GetAsync(int userId, CancellationToken cancellationToken);
    }
}

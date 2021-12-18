using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using _7PlusCodeChallenge.Contracts.Models;
using _7PlusCodeChallenge.Contracts.Repository;
using _7PlusCodeChallenge.Contracts.Services;

namespace _7PlusCodeChallenge.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserModel> GetUserAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(userId, cancellationToken);

            if (user == null)
            {
                return null;
            }

            var model = new UserModel
            {
                FirstName = user.First
            };

            return model;
        }

        public async Task<NameCollectionModel> GetFirstNamesForAgeAsync(int age, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetByAgeAsync(age, cancellationToken);

            if (users == null)
            {
                return null;
            }

            var model = new NameCollectionModel
            {
                Names = users.Select(u => u.First).ToList()
            };

            return model;
        }

        public async Task<GenderByAgeCollectionModel> GetGendersByAgeAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAsync(cancellationToken);

            if (users == null)
            {
                return null;
            }

            var model = new GenderByAgeCollectionModel
            {
                Models = users.GroupBy(u => u.Age).OrderBy(g => g.Key).Select(g => new GenderByAgeModel
                {
                    Age = g.Key,
                    Female = g.Count(u => u.Gender == Gender.Female),
                    Male = g.Count(u => u.Gender == Gender.Male)
                }).ToList()
            };

            return model;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using _7PlusCodeChallenge.Contracts.Repository;

namespace _7PlusCodeChallenge.Repository
{
    public class ApiUserRepository : IUserRepository
    {
        private static readonly string UsersCacheKey = "__UsersCacheKey";

        private readonly ApiUserRepositoryOptions _options;
        private readonly IParser _parser;
        private readonly ICacheAdapter _cacheAdapter;

        public ApiUserRepository(ApiUserRepositoryOptions options, IParser parser, ICacheAdapter cacheAdapter)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _cacheAdapter = cacheAdapter ?? throw new ArgumentNullException(nameof(cacheAdapter));
        }

        public async Task<IList<User>> GetAsync(CancellationToken cancellationToken)
        {
            var users = _cacheAdapter.Get<IList<User>>(UsersCacheKey);

            if (users == null)
            {
                users = await FetchAsync(cancellationToken);
                _cacheAdapter.Set(UsersCacheKey, users);
            }

            return users;
        }

        private async Task<IList<User>> FetchAsync(CancellationToken cancellationToken)
        {
            try
            {
                var client = new HttpClient();
                client.Timeout = _options.Timeout;
                var response = await client.GetAsync(_options.Url, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return null; // Soft failure. Alternatively can throw exception
                }

                var result = await response.Content.ReadAsStreamAsync();

                var users = _parser.Parse<IList<User>>(result);

                return users;
            }
            catch (Exception ex)
            {
                // logging...
                return null;
            }
        }

        public async Task<IList<User>> GetByAgeAsync(int age, CancellationToken cancellationToken)
        {
            var users = await GetAsync(cancellationToken);

            return users?.Where(u => u.Age == age).ToList();
        }

        public async Task<User> GetAsync(int userId, CancellationToken cancellationToken)
        {
            var users = await GetAsync(cancellationToken);

            return users?.FirstOrDefault(u => u.Id == userId);
        }
    }
}

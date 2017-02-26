using System;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TravelpayoutsAPI.Library;

namespace AviaTicketsWpfApplication.Fundamentals
{
    public sealed class CacheService : ICacheService
	{
        private readonly IApiFactory _apiFactory;
		private readonly IRepository<CacheItem> _repositoryCache;

		public CacheService(
            IApiFactory apiFactory,
			IRepository<CacheItem> repositoryCache)
		{
            _apiFactory = apiFactory;
			_repositoryCache = repositoryCache;
        }

        public async Task<T> GetAsync<T>(string tag) where T : class
        {
            var item = await _repositoryCache.GetByTagAsync(tag);

            if (item != null)
            {
                return JsonConvert.DeserializeObject<T>(item.Info);
            }

            return default(T);
        }

		public async Task CreateTableAsync()
		{
			if (!await _repositoryCache.Connection.ExistsAsync<CacheItem>())
			{
				await _repositoryCache.Connection.CreateTableAsync<CacheItem>();
			}
		}

		public async Task InsertUserLocationAsync()
		{
            var item = await _repositoryCache.GetByTagAsync(CacheTags.USERINFO);

			if (item == null)
			{
                string ip = await _apiFactory.UserInfo.UserIP;
                var userLocationInfo = await _apiFactory.UserInfo.GetUserLocationAsync(ip);

				await _repositoryCache.InsertAsync(new CacheItem
				{
                    Tag = CacheTags.USERINFO,
					Info = JsonConvert.SerializeObject(userLocationInfo),
					CreateAt = DateTime.Now,
					UpdateAt = DateTime.Now,
                    IsTemporary = false
				});
			}
		}

		public async Task UpdateOrInsertDataAsync(string dataName)
		{
			var today = DateTime.Today;
			var item = await _repositoryCache.GetByTagAsync(dataName);

			if (item != null)
			{
				if (Math.Abs(today.Day - item.UpdateAt.Day) > 10)
				{
                    item.Info = await _apiFactory.DataInfo.GetJsonAsync(dataName);
					item.UpdateAt = DateTime.Now;

					await _repositoryCache.UpdateAsync(item);
				}
			}
			else
			{
				await _repositoryCache.InsertAsync(new CacheItem
				{
					Tag = dataName,
                    Info = await _apiFactory.DataInfo.GetJsonAsync(dataName),
					CreateAt = DateTime.Now,
					UpdateAt = DateTime.Now,
                    IsTemporary = false
				});
			}
		}

		public async Task DeleteDataAsync(string dataName)
		{
			var item = await _repositoryCache.GetByTagAsync(dataName);

			if (item != null)
			{
				await _repositoryCache.DeleteAsync(item);
			}
		}

        public async Task ClearFromTemporaryAsync()
        {
            var items = await _repositoryCache
                .GetTable()
                .Where(t => t.IsTemporary)
                .ToListAsync();

            foreach (var temp in items)
            {
                await Task.Yield();
                await _repositoryCache.DeleteAsync(temp);
            }
        }

        public async Task<Tuple<string, string>> GetApiInfoAsync()
        {
            var item = await _repositoryCache.GetByTagAsync(CacheTags.APIINFO);
            if (item != null)
            {
                var info = JObject.Parse(item.Info);

                return new Tuple<string, string>((string)info["token"], (string)info["marker"]);
            }

            return null;
        }
    }
}

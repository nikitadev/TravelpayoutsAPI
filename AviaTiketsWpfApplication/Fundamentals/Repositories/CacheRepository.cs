using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTicketsWpfApplication.Fundamentals
{
	public class CacheRepository : IRepository<CacheItem>
	{
		private readonly IDbConnection _dbConnection;

		public SQLiteAsyncConnection Connection
		{  
			get
			{
				return _dbConnection.SQLiteConnection;
            }
		}

		public CacheRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
        }

		public async Task InsertAsync(CacheItem obj)
		{
			await Connection.InsertAsync(obj);
		}

		public async Task UpdateAsync(CacheItem obj)
		{
			await Connection.UpdateAsync(obj);
		}

		public async Task DeleteAsync(CacheItem obj)
		{
			await Connection.DeleteAsync(obj);
		}

		public async Task<CacheItem> GetByIdAsync(int id)
		{
			var query = Connection.Table<CacheItem>().Where(x => x.Id == id);
			var result = await query.ToListAsync();

			return result.FirstOrDefault();
		}

		public async Task<CacheItem> GetByTagAsync(string tag)
		{
			var query = Connection.Table<CacheItem>().Where(x => x.Tag.ToLower() == tag.ToLower());
			var result = await query.ToListAsync();

			return result.FirstOrDefault();
		}

        public AsyncTableQuery<CacheItem> GetTable()
        {
            return Connection.Table<CacheItem>();
        }
	}
}

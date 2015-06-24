using SQLite;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Models;

namespace AviaTicketsWpfApplication.Fundamentals.Interfaces
{
	public interface IRepository<T> where T : IDbEntity, new()
	{
		SQLiteAsyncConnection Connection { get; }

		Task InsertAsync(T obj);

		Task UpdateAsync(T obj);

		Task DeleteAsync(T obj);

        AsyncTableQuery<T> GetTable();

		Task<T> GetByIdAsync(int id);

		Task<T> GetByTagAsync(string tag);
	}
}

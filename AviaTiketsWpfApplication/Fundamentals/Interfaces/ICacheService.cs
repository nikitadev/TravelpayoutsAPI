using System.Threading.Tasks;

namespace AviaTicketsWpfApplication.Fundamentals.Interfaces
{
	public interface ICacheService
	{
		Task CreateTableAsync();
        Task UpdateOrInsertDataAsync(string dataName);
		Task DeleteDataAsync(string dataName);
        Task ClearFromTemporaryAsync();
		Task InsertUserLocationAsync();
        Task<T> GetAsync<T>(string tag) where T : class;
        Task<string> GetTokenAsync();
    }
}
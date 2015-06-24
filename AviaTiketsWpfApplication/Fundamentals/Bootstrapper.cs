using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using System;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.Fundamentals
{
	public class Bootstrapper
	{
		private readonly ICacheService _cacheService;

		public Bootstrapper(ICacheService cacheService)
		{
			_cacheService = cacheService;
        }

		public async Task InitializeAsync(IProgress<Tuple<int, string>> progress)
		{
			progress.Report(Tuple.Create(5, "tables"));
			await _cacheService.CreateTableAsync();

			progress.Report(Tuple.Create(10, "get user location"));
			await _cacheService.InsertUserLocationAsync();

			progress.Report(Tuple.Create(20, DataNames.Planes));
			await _cacheService.UpdateOrInsertDataAsync(DataNames.Planes);
			progress.Report(Tuple.Create(30, DataNames.Cities));
			await _cacheService.UpdateOrInsertDataAsync(DataNames.Cities);
			progress.Report(Tuple.Create(40, DataNames.Airports));
			await _cacheService.UpdateOrInsertDataAsync(DataNames.Airports);
			progress.Report(Tuple.Create(60, DataNames.Airlines));
			await _cacheService.UpdateOrInsertDataAsync(DataNames.Airlines);
			progress.Report(Tuple.Create(70, DataNames.Countries));
			await _cacheService.UpdateOrInsertDataAsync(DataNames.Countries);
			progress.Report(Tuple.Create(80, DataNames.AirlinesAlliances));
			await _cacheService.UpdateOrInsertDataAsync(DataNames.AirlinesAlliances);
			
			//await _cacheService.UpdateOrInsertDataAsync(DataNames.Routes);

			progress.Report(Tuple.Create(100, "done"));
		}
	}
}

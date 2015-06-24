using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Models.Data;

namespace TravelpayoutsAPI.Library.Infostructures.Interfaces
{
	[ContractClass(typeof(DataInfoProviderContract))]
    /// <summary>
    /// Данные о странах, городах, аэропортах, авиакомпаниях, самолетах
    /// </summary>
    public interface IDataInfoProvider
    {
		/// <summary>
		/// Возвращает данные в первичном виде
		/// </summary>
		/// <param name="dataName"><see cref="DataNames"/></param>
		/// <returns></returns>
		Task<string> GetJsonAsync(string dataName);

		/// <summary>
		/// Возвращает данные
		/// </summary>
		/// <typeparam name="T">тип данных</typeparam>
		/// <param name="dataName"><see cref="DataNames"/></param>
		/// <returns>Список данных</returns>
		Task<IEnumerable<T>> GetAsync<T>(string dataName) where T : class, INameDataInfo;
    }

    [ContractClassFor(typeof(IDataInfoProvider))]
    internal abstract class DataInfoProviderContract : IDataInfoProvider
    {
		public Task<string> GetJsonAsync(string dataName)
		{
			Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(dataName), "dataName can not be null or empty.");

			Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

			return Task.FromResult(default(string));
		}

		public Task<IEnumerable<T>> GetAsync<T>(string dataName) where T: class, INameDataInfo
		{
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(dataName), "dataName can not be null or empty.");

            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
            Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), item => item != null));

            return Task.FromResult(default(IEnumerable<T>));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
	}
}
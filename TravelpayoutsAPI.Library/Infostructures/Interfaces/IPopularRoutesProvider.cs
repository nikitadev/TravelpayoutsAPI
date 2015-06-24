using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Models;

namespace TravelpayoutsAPI.Library.Infostructures.Interfaces
{
    [ContractClass(typeof(PopularRoutesProviderContract))]
    /// <summary>
    /// Популярные маршруты авиалиний или из города
    /// </summary>
    public interface IPopularRoutesProvider
    {
        /// <summary>
        /// Популярные направления авиакомпании
        /// </summary>
        /// <param name="token">Индивидуальный токен доступа.</param>
        /// <param name="airlineCode">IATA код авиакомпании. IATA код указывается буквами верхнего регистра</param>
        /// <param name="limit">Количество записей на странице. Значение по умолчанию - 30. Не более 1000.</param>
        /// <returns></returns>
        Task<IEnumerable<Tuple<string, int>>> GetPopularAirlineRoutes(string token, string airlineCode, int limit);

        /// <summary>
        /// Популярные направления из города
        /// </summary>
        /// <param name="token">Индивидуальный токен доступа.</param>
        /// <param name="origin">Пункт отправления. IATA код города.</param>
        /// <returns></returns>
        Task<IEnumerable<Ticket>> GetPopularRoutesFromCity(string token, string origin);
    }

    [ContractClassFor(typeof(IPopularRoutesProvider))]
    internal abstract class PopularRoutesProviderContract : IPopularRoutesProvider
    {
        public Task<IEnumerable<Tuple<string, int>>> GetPopularAirlineRoutes(string token, string airlineCode, int limit)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(token), "Token can not be null or empty.");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(airlineCode), "Airline code can not be null or empty.");

            Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Tuple<string, int>>>(), item => item != null));

            return Task.FromResult(default(IEnumerable<Tuple<string, int>>));
        }

        public Task<IEnumerable<Ticket>> GetPopularRoutesFromCity(string token, string origin)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(token), "Token can not be null or empty.");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(origin), "Origin can not be null or empty.");

            Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Ticket>>(), item => item != null));

            return Task.FromResult(default(IEnumerable<Ticket>));
        }
    }
}

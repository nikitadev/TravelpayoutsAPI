using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Implements;
using TravelpayoutsAPI.Library.Models.Monitor;

namespace TravelpayoutsAPI.Library.Infostructures.Interfaces
{
    [ContractClass(typeof(SimpleSearchTicketsProviderContract))]
	public interface ISimpleSearchTicketsProvider
	{
        /// <summary>
        /// Самые дешевые авиабилеты
        /// </summary>
        /// <param name="token">Индивидуальный токен доступа.</param>
        /// <param name="origin">IATA код города вылета.<seealso cref="GetCities"/></param>
        /// <param name="destination">IATA код города назначения (укажите "-" для любых направлений).<seealso cref="GetCities"/></param>
        /// <param name="departDate">День или месяц вылета.</param>
        /// <param name="returnDate">День или месяц возвращения.</param>
        /// <returns><see cref="Result<Ticket>"/></returns>
        /// <example>
        /// var result = await GetCheapestTickets("MOW", "HKT", new DateTime(2015, 6, 1), new DateTime(2015, 6, 30))
        /// </example>
        Task<IEnumerable<Ticket>> GetCheapestTickets(string token, string origin, string destination = "-", DateTime? departDate = null, DateTime? returnDate = null);

        /// <summary>
        /// Билеты без пересадок
        /// </summary>
        /// <param name="token">Индивидуальный токен доступа.</param>
        /// <param name="origin">IATA код города вылета.</param>
        /// <param name="destination">IATA код города назначения (укажите "-" для любых направлений).</param>
        /// <param name="departDate">День или месяц вылета.</param>
        /// <param name="returnDate">День или месяц возвращения.</param>
        /// <returns></returns>
        /// <example>
        /// var result = await GetDirectFlights("MOW", "LED", new DateTime(2015, 6, 1), new DateTime(2015, 7, 31))
        /// </example>
        Task<IEnumerable<Ticket>> GetDirectFlights(string token, string origin, string destination = "-", DateTime? departDate = null, DateTime? returnDate = null);

        /// <summary>
        /// Билеты из города на любое число месяца
        /// </summary>
        /// <param name="token">Индивидуальный токен доступа.</param>
        /// <param name="origin">IATA код города вылета.</param>
        /// <param name="destination">IATA код города назначения.</param>
        /// <param name="departDate">Месяц вылета (в формате yyyy-mm).</param>
        /// <param name="tripDuration">Длительность пребывания в городе назначения.</param>
        /// <param name="calendarType">Поле, по которому будет строиться календарь. Одно из двух значений: departure_date или return_date.</param>
        /// <param name="returnDate">Месяц возвращения (в формате yyyy-mm). Если дата возвращения не указана, отобразятся рейсы в одну сторону.</param>
        /// <returns></returns>
        Task<Dictionary<DateTime, Ticket>> GetTicketsFromCityForAnyday(string token, string origin, string destination = null, DateTime? departDate = null, int tripDuration = 0, CalendarType calendarType = CalendarType.departure_date, DateTime? returnDate = null);

        /// <summary>
        /// Билеты из города на любое число месяца
        /// </summary>
        /// <param name="token">Индивидуальный токен доступа.</param>
        /// <param name="origin">IATA код города вылета.</param>
        /// <param name="months">Месяцы вылета (в формате yyyy-mm).</param>
        /// <param name="tripDurations">Длительность пребывания в городе назначения (от 1 до 30 дней)</param>
        /// <param name="destination">IATA код города назначения.</param>
        /// <returns></returns>
        Task<IEnumerable<Ticket>> GetTicketsFromCityForAnyday(string token, string origin, string[] months, int[] tripDurations, string destination = null);

        /// <summary>
        /// Цены на билеты по месяцам
        /// </summary>
        /// <param name="token"></param>
        /// <param name="origin">Пункт отправления. IATA код города. Длина не более 3. Длина не менее 3.</param>
        /// <param name="destination">Пункт назначения. IATA код города или код страны. Длина не более 3. Длина не менее 1.</param>
        /// <returns></returns>
        Task<Dictionary<DateTime, Ticket>> GetTicketsByMontly(string token, string origin, string destination = null);
    }

    [ContractClassFor(typeof(ISimpleSearchTicketsProvider))]
    internal abstract class SimpleSearchTicketsProviderContract : ISimpleSearchTicketsProvider
    {
        public Task<IEnumerable<Ticket>> GetCheapestTickets(string token, string origin, string destination = "-", DateTime? departDate = null, DateTime? returnDate = null)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(token), "Token can not be null or empty.");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(origin), "Origin can not be null or empty.");

            Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Ticket>>(), item => item != null));

            return Task.FromResult(default(IEnumerable<Ticket>));
        }

        public Task<IEnumerable<Ticket>> GetDirectFlights(string token, string origin, string destination = "-", DateTime? departDate = null, DateTime? returnDate = null)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(token), "Token can not be null or empty.");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(origin), "Origin can not be null or empty.");

            Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Ticket>>(), item => item != null));

            return Task.FromResult(default(IEnumerable<Ticket>));
        }

        public Task<Dictionary<DateTime, Ticket>> GetTicketsFromCityForAnyday(string token, string origin, string destination = null, DateTime? departDate = null, int tripDuration = 0, CalendarType calendarType = CalendarType.departure_date, DateTime? returnDate = null)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(token), "Token can not be null or empty.");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(origin), "Origin can not be null or empty.");

            Contract.Ensures(Contract.ForAll(Contract.Result<Dictionary<DateTime, Ticket>>(), item => item.Value != null));

            return Task.FromResult(default(Dictionary<DateTime, Ticket>));
        }

        public Task<IEnumerable<Ticket>> GetTicketsFromCityForAnyday(string token, string origin, string[] months, int[] tripDurations, string destination = null)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(token), "Token can not be null or empty.");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(origin), "Origin can not be null or empty.");

            Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<Ticket>>(), item => item != null));

            return Task.FromResult(default(IEnumerable<Ticket>));
        }

        public Task<Dictionary<DateTime, Ticket>> GetTicketsByMontly(string token, string origin, string destination = null)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(token), "Token can not be null or empty.");
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(origin), "Origin can not be null or empty.");

            Contract.Ensures(Contract.ForAll(Contract.Result<Dictionary<DateTime, Ticket>>(), item => item.Value != null));

            return Task.FromResult(default(Dictionary<DateTime, Ticket>));
        }
    }
}

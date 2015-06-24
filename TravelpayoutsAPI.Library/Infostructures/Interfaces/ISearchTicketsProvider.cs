using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Models;

namespace TravelpayoutsAPI.Library.Infostructures.Interfaces
{
    [ContractClass(typeof(SearchTicketsProviderContract))]
	public interface ISearchTicketsProvider
	{
		/// <summary>
		/// Цены на авиабилеты
		/// </summary>
		/// <param name="token"></param>
		/// <param name="origin">Пункт отправления. IATA код города или код страны. Длина не менее 2. Длина не более 3. false.</param>
		/// <param name="destination">Пункт назначения. IATA код города или код страны. Длина не менее 2. Длина не более 3. false.</param>
		/// <param name="isbeginningOfPeriod">Начало периода, в который попадают даты отправления. false.</param>
		/// <param name="period">Значение по умолчанию - <see cref="PeriodType.Day"/>. Содержит только одно из следующих значений: <see cref="PeriodType"/></param>
		/// <param name="isOneWay">true - в одну сторону, false - туда и обратно. Значение по умолчанию - false</param>
		/// <param name="page">Номер страницы. Значение по умолчанию - 1.</param>
		/// <param name="limit">Количество записей на странице. Значение по умолчанию - 30. Не более 1000.</param>
		/// <param name="isShowToAffiliates">false - все цены, true - только цены, найденные с партнёрским маркером (рекомендовано). Значение по умолчанию - true.</param>
		/// <param name="sorting">Сортировка цен. Для направлений город - город возможна сортировка только по цене. Значение по умолчанию - <see cref="SortingMode.Price"/>. Содержит только одно из следующих значений: <see cref="SortingMode"/></param>
		/// <param name="tripClass">Значение по умолчанию - <see cref="TripClassMode.Econom"/>. Содержит только одно из следующих значений: <see cref="TripClassMode"/></param>
		/// <param name="isTripDurationByDay">Длительность пребывания в неделях или днях (для period = <see cref="PeriodType.Day"/>)</param>
		/// <returns></returns>
		Task<List<Ticket>> GetPrice(
			string token, 
			string origin, 
			string destination,
            DateTime departDate,
            DateTime returnDate,
			PeriodType period = PeriodType.Year,
			bool isOneWay = false,
			int page = 1,
			int limit = 30,
			bool isShowToAffiliates = true,
			SortingMode sorting = SortingMode.Price,
			TripClassMode tripClass = TripClassMode.Econom);

        /// <summary>
        /// Специальные предложения
        /// </summary>
        /// <returns></returns>
        Task<List<Offer>> GetSpecialOffers();
    }

    [ContractClassFor(typeof(ISearchTicketsProvider))]
    internal abstract class SearchTicketsProviderContract : ISearchTicketsProvider
    {
        public Task<List<Ticket>> GetPrice(string token, string origin, string destination, DateTime departDate, DateTime returnDate, PeriodType period = PeriodType.Year, bool isOneWay = false, int page = 1, int limit = 30, bool isShowToAffiliates = true, SortingMode sorting = SortingMode.Price, TripClassMode tripClass = TripClassMode.Econom)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(token), "Token can not be null or empty.");

            return Task.FromResult(default(List<Ticket>));
        }

        public Task<List<Offer>> GetSpecialOffers()
        {
            Contract.Ensures(Contract.ForAll(Contract.Result<List<Offer>>(), item => item != null));

            return Task.FromResult(default(List<Offer>));
        }
    }
}

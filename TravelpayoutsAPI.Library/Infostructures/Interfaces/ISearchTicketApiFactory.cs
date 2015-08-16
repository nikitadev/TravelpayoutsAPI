using System;
using System.Diagnostics.Contracts;

namespace TravelpayoutsAPI.Library.Infostructures.Interfaces
{
    [ContractClass(typeof(SearchTicketApiFactoryContract))]
    public interface ISearchTicketApiFactory
    {
        /// <summary>
        /// Предоставляет доступ к данным о пользователе
        /// </summary>
        IUserInfoProvider UserInfo { get; }

        /// <summary>
        /// Предоставляет данные
        /// </summary>
        IDataInfoProvider DataInfo { get; }

        /// <summary>
        /// Предоставляет доступ к популярным маршрутам
        /// </summary>
        IPopularRoutesProvider PopularRoutes { get; }

        /// <summary>
        /// Поиск билетов из кэша
        /// </summary>
        ISearchTicketsProvider MainSearch { get; }

        /// <summary>
        /// Предоставляет упращёный поиск билетов из кэша
        /// </summary>
        ISimpleSearchTicketsProvider SimpleSearch { get; }

        /// <summary>
        /// Предоставляет доступ к стандартному поиску
        /// </summary>
        IFlightSearchProvider FlightSearch { get; }
    }

    [ContractClassFor(typeof(ISearchTicketApiFactory))]
    internal abstract class SearchTicketApiFactoryContract : ISearchTicketApiFactory
    {
        public IUserInfoProvider UserInfo
        {
            get
            {
                Contract.Ensures(Contract.Result<IUserInfoProvider>() != null);

                return default(IUserInfoProvider);
            }
        }

        public IDataInfoProvider DataInfo
        {
            get 
            { 
                Contract.Ensures(Contract.Result<IDataInfoProvider>() != null);

                return default(IDataInfoProvider);
            }
        }

        public IPopularRoutesProvider PopularRoutes
        {
            get
            {
                Contract.Ensures(Contract.Result<IPopularRoutesProvider>() != null);

                return default(IPopularRoutesProvider);
            }
        }

        public ISearchTicketsProvider MainSearch
        {
            get
            {
                Contract.Ensures(Contract.Result<ISearchTicketsProvider>() != null);

                return default(ISearchTicketsProvider);
            }
        }

        public ISimpleSearchTicketsProvider SimpleSearch
        {
            get
            {
                Contract.Ensures(Contract.Result<ISimpleSearchTicketsProvider>() != null);

                return default(ISimpleSearchTicketsProvider);
            }
        }

        public IFlightSearchProvider FlightSearch
        {
            get
            {
                Contract.Ensures(Contract.Result<IFlightSearchProvider>() != null);

                return default(IFlightSearchProvider);
            }
        }
    }
}

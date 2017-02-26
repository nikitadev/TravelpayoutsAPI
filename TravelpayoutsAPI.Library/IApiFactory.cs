using System.Diagnostics.Contracts;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;

namespace TravelpayoutsAPI.Library
{
    [ContractClass(typeof(ApiFactoryContract))]
    public interface IApiFactory
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
        IRealtimeSearchProvider RealtimeSearch { get; }
    }

    [ContractClassFor(typeof(IApiFactory))]
    internal abstract class ApiFactoryContract : IApiFactory
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

        public IRealtimeSearchProvider RealtimeSearch
        {
            get
            {
                Contract.Ensures(Contract.Result<IRealtimeSearchProvider>() != null);

                return default(IRealtimeSearchProvider);
            }
        }
    }
}

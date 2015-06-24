using System.Diagnostics.Contracts;

namespace TravelpayoutsAPI.Library.Infostructures.Interfaces
{
    [ContractClass(typeof(SearchTicketApiFactoryContract))]
    public interface ISearchTicketApiFactory
    {
        IUserInfoProvider UserInfo { get; }

        IDataInfoProvider DataInfo { get; }

        IPopularRoutesProvider PopularRoutes { get; }

        ISearchTicketsProvider MainSearch { get; }

        ISimpleSearchTicketsProvider SimpleSearch { get; }
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
    }
}

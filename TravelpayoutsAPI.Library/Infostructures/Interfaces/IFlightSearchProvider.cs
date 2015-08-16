using System.Collections.Generic;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Models;
using TravelpayoutsAPI.Library.Models.Search;

namespace TravelpayoutsAPI.Library.Infostructures.Interfaces
{
    /// <summary>
    /// Поиск в реальном времени
    /// </summary>
    public interface IFlightSearchProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="marker"></param>
        /// <param name="host"></param>
        /// <param name="adults"></param>
        /// <param name="children"></param>
        /// <param name="infants"></param>
        /// <param name="tripClass"></param>
        /// <param name="userIP"></param>
        /// <param name="segments"></param>
        /// <returns></returns>
        Task<List<SearchResult>> GetTickets(string token, string marker, string host, int adults, int children, int infants, string tripClass, string userIP, params Segment[] segments);
    }
}

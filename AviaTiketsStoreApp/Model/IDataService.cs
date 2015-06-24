using System.Threading.Tasks;

namespace AviaTicketsStoreApp.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}
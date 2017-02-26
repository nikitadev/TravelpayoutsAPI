using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library;

namespace AviaTicketsWpfApplication.Fundamentals.Abstracts
{
    public abstract class BasePageChartViewModel<T> : BasePageSearchResultViewModel<T> where T : class
    {
        private string _titleChart;
        public string TitleChart
        {
            get { return _titleChart; }
            set { Set(ref _titleChart, value); }
        }

        public BasePageChartViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            MessengerInstance.Register<DetailsPageMessage>(this, async (m) => await DetailsPageMessageHandler(m));
        }

        protected override async Task<IEnumerable<T>> UpdateCollection(ISearchQuery searchQuery)
        {
            await Task.Yield();

            return Enumerable.Empty<T>();
        }

        protected async Task DetailsPageMessageHandler(DetailsPageMessage m)
        {
            string code;
            if (m.Parametrs.TryGetValue("code", out code))
            {
                await SearchQueryHandler(new TextSearchQuery { Text = code });
            }
        }
    }
}

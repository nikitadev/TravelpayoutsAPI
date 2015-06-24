
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using System.Collections.Generic;

namespace AviaTicketsWpfApplication.Models
{
    public class SearchResultMessage
    {
        public IEnumerable<IHyperlinkViewModel> ListResult { get; set; }

        public string Message { get; set; }

        public bool IsFinished { get; set; }
    }
}
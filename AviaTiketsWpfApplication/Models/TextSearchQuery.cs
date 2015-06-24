using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using System;

namespace AviaTicketsWpfApplication.Models
{
    public class TextSearchQuery : ISearchQuery
    {
        public string Text;

        public bool IsValidate
        {
            get { return !String.IsNullOrEmpty(Text); }
        }
    }
}

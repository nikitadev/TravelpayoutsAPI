using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTicketsWpfApplication.Fundamentals.Interfaces
{
    public interface ICollectionViewModel<T>
    {
        ObservableCollection<T> Collection { get; }
    }
}

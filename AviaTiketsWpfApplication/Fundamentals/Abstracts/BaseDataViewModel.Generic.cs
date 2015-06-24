using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.Fundamentals.Abstracts
{
    public abstract class BaseDataViewModel<T> : ViewModelBase, IHyperlinkViewModel where T : IDataInfo
    {
        protected readonly T _current;

        protected virtual Type TypeDetailedViewModel { get { return null; } }

        public RelayCommand ClickElementCommand { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public BaseDataViewModel(T data)
        {
            _current = data;

            Code = data.Code;
            Title = data.Name;

            ClickElementCommand = new RelayCommand(ClickElementCommandHandler, () => !String.IsNullOrEmpty(_current.Code));
        }

        protected void ClickElementCommandHandler()
        {
            if (TypeDetailedViewModel != null)
            {
                string @params = String.Format("code={0}", _current.Code);
                MessengerInstance.Send(new PageMessage(TypeDetailedViewModel, @params));
            }
        }
    }
}

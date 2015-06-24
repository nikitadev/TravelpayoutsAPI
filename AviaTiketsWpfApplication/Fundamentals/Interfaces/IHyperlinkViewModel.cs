using GalaSoft.MvvmLight.Command;

namespace AviaTicketsWpfApplication.Fundamentals.Interfaces
{
    public interface IHyperlinkViewModel
    {
        RelayCommand ClickElementCommand { get; set; }

        string Title { get; set; }
    }
}

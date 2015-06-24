using AviaTicketsWpfApplication.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Media;

namespace AviaTicketsWpfApplication.ViewModels
{
    public enum TileSizeMode { Large, Medium, Small }
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public sealed class TileViewModel : ViewModelBase
    {
        const string LargeStyleName = "LargeTileStyleKey";
        const string MediumStyleName = "MediumTileStyleKey";
        const string SmallStyleName = "SmallTileStyleKey";
        public RelayCommand TappedTileCommand { get; set; }
        
        public Type TypePage { get; set; }

        private string _title;
        public string Title 
        {
            get { return _title;  }
            set { Set(ref _title, value); }
        }

        private TileSizeMode _sizeMode;
        public TileSizeMode SizeMode 
        {
            get { return _sizeMode; }
            set 
            {
                _sizeMode = value;

                StyleKeyName = _sizeMode == TileSizeMode.Large ? LargeStyleName 
                    : _sizeMode == TileSizeMode.Medium ? MediumStyleName 
                    : SmallStyleName;

                RaisePropertyChanged(() => StyleKeyName);
            }
        }

        public string StyleKeyName { get; private set; }

        private string _iconName;
        public string IconName
        { 
            get { return _iconName;  }
            set { Set(ref _iconName, value); }
        }

        private SolidColorBrush _background;
        public SolidColorBrush Background 
        {
            get { return _background;  }
            set { Set(ref _background, value); }
        }

        /// <summary>
        /// Initializes a new instance of the TileViewModel class.
        /// </summary>
        public TileViewModel()
        {
            TappedTileCommand = new RelayCommand(TappedTileCommandHandle, () => TypePage != null);
        }

        private void TappedTileCommandHandle()
        {
            MessengerInstance.Send(new PageMessage(TypePage, String.Concat("title=", Title)));
        }
    }
}
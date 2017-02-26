using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using AviaTicketsWpfApplication.Properties;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using AviaTicketsWpfApplication.Fundamentals;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TokenDialogViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly IRepository<CacheItem> _repositoryCashe;

        private readonly HashSet<string> _columnClearValidations;

        public RelayCommand AcceptCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private bool _isSaveTokenChecked;
        public bool IsSaveTokenChecked 
        {
            get { return _isSaveTokenChecked; }
            set { Set(ref _isSaveTokenChecked, value); }
        }

        private bool _isUseTestTokenChecked;
        public bool IsUseTestTokenChecked 
        {
            get { return _isUseTestTokenChecked; }
            set 
            {
                Token = value ? Settings.Default.TestToken : String.Empty;
                Marker = value ? Settings.Default.TestMarker : String.Empty;

                Set(ref _isUseTestTokenChecked, value); 
            }
        }        

        private string _token;
        public string Token 
        {
            get { return _token; }
            set { Set(ref _token, value); }
        }

        private string _marker;
        public string Marker
        {
            get { return _marker; }
            set { Set(ref _marker, value); }
        }

        private Uri _link;
        public Uri Link
        {
            get { return _link; }
            set { Set(ref _link, value); }
        }

        public string Error
        {
            get
            {
                return String.Join(", ", _columnClearValidations.ToArray());
            }
        }

        /// <summary>
        /// Initializes a new instance of the TokenDialogViewModel class.
        /// </summary>
        public TokenDialogViewModel(IRepository<CacheItem> repositoryCashe)
        {
            _repositoryCashe = repositoryCashe;

            _columnClearValidations = new HashSet<string>();

            IsUseTestTokenChecked = false;

            Link = new Uri(Settings.Default.MainUri);

            CancelCommand = new RelayCommand(CancelHandler);
            AcceptCommand = new RelayCommand(async () => await AcceptCommandHandlerAsync(), () => String.IsNullOrEmpty(Error));
        }

        private void CancelHandler()
        {
            MessengerInstance.Send(new DialogMessage { ActType = ActionType.Close });
        }

        private async Task AcceptCommandHandlerAsync()
        {
            var item = new CacheItem
            {
                Tag = CacheTags.APIINFO,
                Info = JsonConvert.SerializeObject(new { Token, Marker }),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                IsTemporary = !IsSaveTokenChecked
            };

            await _repositoryCashe.InsertAsync(item);

            MessengerInstance.Send(new DialogMessage { DlgType = DialogType.Login, ActType = ActionType.Hide });
        }

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;
                if (columnName == nameof(Token))
                {
                    if (String.IsNullOrWhiteSpace(Token))
                    {
                        result = Resources.ValidateMsgToken;
                    }
                }
                if (columnName == nameof(Marker))
                {
                    if (String.IsNullOrWhiteSpace(Marker))
                    {
                        result = Resources.ValidateMsgMarker;
                    }
                }

                if (!String.IsNullOrEmpty(result))
                {
                    _columnClearValidations.Add(columnName);
                }
                else if (_columnClearValidations.Contains(columnName))
                {
                    _columnClearValidations.Remove(columnName);
                }

                AcceptCommand.RaiseCanExecuteChanged();

                return result;
            }
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models.Data;

namespace TravelpayoutsAPI.Library.Infostructures.Implements
{
    public sealed class UserInfoProvider : BaseApiProvider, IUserInfoProvider
    {
        private UriBuilder _uriBuilder;

        public Task<string> UserIP
        {
            get 
            {
                _uriBuilder.Host = GeneralSettings.IP_URI;

                var uri = CreateUri();

                return _requestManager.Get(uri);
            }
        }

        public UserInfoProvider(IRequestManager requestManager)
            : base(requestManager)
        {
            _uriBuilder = new UriBuilder { Scheme = GeneralSettings.ShemaName, Port = -1 };
        }

        protected override UriBuilder GetBaseUri()
        {
            return _uriBuilder;
        }

        public async Task<string> GetIPAsync()
        {
            _uriBuilder.Host = GeneralSettings.IP_URI;

            var uri = CreateUri();

            return await _requestManager.Get(uri).ConfigureAwait(false);
        }

        public async Task<UserLocationInfo> GetUserLocationAsync(string ip)
        {
            var userInfo = new UserLocationInfo { IP = ip };

            string callbackName = "useriata";
            string locale = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            string query = String.Format("locale={0}&callback={1}&ip={2}", locale, callbackName, ip);

            // http://www.travelpayouts.com/whereami?locale=ru&callback=useriata&ip=40.0.0.1
            _uriBuilder.Host = GeneralSettings.MAIN_URI;
            //var uriBuilder = new UriBuilder(GeneralSettings.ShemaName, GeneralSettings.MAIN_URI, -1, GeneralSettings.WAI);

            var uri = CreateUri(GeneralSettings.WAI, query);

            var result = await _requestManager.Get(uri).ConfigureAwait(false);

            string json = result.Replace(callbackName, String.Empty).Replace("(", String.Empty).Replace(")", String.Empty);

            JsonConvert.PopulateObject(json, userInfo);

            return userInfo;
        }
    }
}

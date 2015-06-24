using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;

namespace TravelpayoutsAPI.Library.Models.Data
{
    public abstract class BaseCultureDataInfo : IDataInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string CultureName
        {
            get
            {
                string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

                string cultureName;
                if (NameTranslations.TryGetValue(culture, out cultureName))
                {
                    return cultureName;
                }

                return Name;
            }
        }

        [JsonProperty("name_translations")]
        public IDictionary<string, string> NameTranslations { get; set; }
    }
}

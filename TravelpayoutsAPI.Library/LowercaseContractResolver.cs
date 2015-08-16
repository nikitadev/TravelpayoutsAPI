using Newtonsoft.Json.Serialization;

namespace TravelpayoutsAPI.Library
{
    internal class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}

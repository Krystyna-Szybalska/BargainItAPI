using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BargainIt.Api.Configuration.JsonSerilizer; 

public static class JsonSerializerSettingsExtensions {
	public static void AddJsonSettings(this JsonSerializerSettings jsonSerializerSettings)
	{
		jsonSerializerSettings.Converters.Add(new StringEnumConverter());
		jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
	}
}
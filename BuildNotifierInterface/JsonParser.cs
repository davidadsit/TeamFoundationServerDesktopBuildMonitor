using System.Web.Script.Serialization;

namespace BuildMonitor.Plugin
{
	public static class JsonParser
	{
		public static T Parse<T>(string json)
		{
			JavaScriptSerializer jSerialize = new JavaScriptSerializer();
			return jSerialize.Deserialize<T>(json);
		}

		public static string Serialize<T>(T o)
		{
			JavaScriptSerializer jSerialize = new JavaScriptSerializer();
			return jSerialize.Serialize(o);
		}
	}
}
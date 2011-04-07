using System.Diagnostics.Contracts;
using System.IO;
using System.Web.Script.Serialization;

namespace BuildMonitor.Plugin
{
	public class JsonConfigurationManager<T> : IConfigurationManager<T>
	{
		private readonly FileInfo _configurationFile;
		private readonly JavaScriptSerializer _javaScriptSerializer;

		public JsonConfigurationManager(FileInfo configurationFile)
		{
			_javaScriptSerializer = new JavaScriptSerializer();
			_configurationFile = configurationFile;
		}

		public T Load()
		{
			Contract.Requires(_configurationFile != null);
			if (!_configurationFile.Exists)
			{
				return default(T);
			}
			using (StreamReader streamReader = _configurationFile.OpenText())
			{
				string configuration = streamReader.ReadToEnd();
				return Parse(configuration);
			}
		}

		public void Save(T configuration)
		{
			string serializedConfiguration = Serialize(configuration);
			using (StreamWriter streamWriter = _configurationFile.CreateText())
			{
				streamWriter.Write(serializedConfiguration);
			}
		}

		private T Parse(string json)
		{
			return _javaScriptSerializer.Deserialize<T>(json);
		}

		private string Serialize(T o)
		{
			return _javaScriptSerializer.Serialize(o);
		}
	}
}
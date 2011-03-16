using BuildMonitor.Plugin;
using NUnit.Framework;

namespace PluginTests
{
	[TestFixture]
	public class JsonParserTests
	{
		[Test]
		public void DeserializeHydratesTheObjectCorrection()
		{
			const string json = @"{""Property1"":""123"",""Property2"":456}";
			Configuration configuration = JsonParser.Parse<Configuration>(json);
			Assert.AreEqual("123", configuration.Property1);
			Assert.AreEqual(456, configuration.Property2);
		}

		[Test]
		public void DeserializeSerializedObject()
		{
			Configuration configuration = new Configuration {Property1 = "123", Property2 = 456};
			Configuration rehydratedConfiguration = JsonParser.Parse<Configuration>(JsonParser.Serialize(configuration));
			Assert.AreEqual(configuration.Property1, rehydratedConfiguration.Property1);
			Assert.AreEqual(configuration.Property2, rehydratedConfiguration.Property2);
		}

		[Test]
		public void SerializeGeneratesTheCorrectString()
		{
			const string json = @"{""Property1"":""123"",""Property2"":456}";
			Configuration configuration = new Configuration {Property1 = "123", Property2 = 456};
			Assert.AreEqual(json, JsonParser.Serialize(configuration));
		}
	}
}
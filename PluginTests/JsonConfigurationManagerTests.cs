using System.IO;
using BuildMonitor.Plugin;
using NUnit.Framework;

namespace PluginTests
{
	[TestFixture]
	public class JsonConfigurationManagerTests
	{
		[Test]
		public void ConfigurationCanBePersistedAndRestored()
		{
			Configuration configuration = new Configuration {Property1 = "123", Property2 = 456};
			FileInfo configurationFile = new FileInfo("TestFile.config");
			configurationFile.Delete();
			JsonConfigurationManager<Configuration> jsonConfigurationManager =
				new JsonConfigurationManager<Configuration>(configurationFile);
			jsonConfigurationManager.Save(configuration);
			Configuration rehydratedConfiguration = jsonConfigurationManager.Load();
			Assert.AreEqual(configuration.Property1, rehydratedConfiguration.Property1);
			Assert.AreEqual(configuration.Property2, rehydratedConfiguration.Property2);
		}

		[Test]
		public void ImplmentsIConfigurationManager()
		{
			FileInfo configurationFile = new FileInfo("test.config");
			JsonConfigurationManager<object> jsonConfigurationManager = new JsonConfigurationManager<object>(configurationFile);
			Assert.IsInstanceOf(typeof (IConfigurationManager<object>), jsonConfigurationManager);
		}
	}
}
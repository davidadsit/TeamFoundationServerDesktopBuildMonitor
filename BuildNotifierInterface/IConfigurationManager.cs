using System.IO;

namespace BuildMonitor.Plugin
{
	public interface IConfigurationManager<T>
	{
		T Load();
		void Save(T configuration);
	}
}
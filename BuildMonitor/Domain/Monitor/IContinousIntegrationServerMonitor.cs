using System;

namespace BuildMonitor.Domain.Monitor
{
	public interface IContinousIntegrationServerMonitor
	{
		BuildStatus GetBuildStatus();
	}

	internal class TFSMonitor : IContinousIntegrationServerMonitor
	{
		public BuildStatus GetBuildStatus()
		{
			throw new NotImplementedException();
		}
	}
}
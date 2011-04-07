using System;

namespace BuildMonitor.Domain.Monitor
{
	internal class TFSMonitor : IContinousIntegrationServerMonitor
	{
		public BuildStatus GetBuildStatus()
		{
			throw new NotImplementedException();
		}
	}
}
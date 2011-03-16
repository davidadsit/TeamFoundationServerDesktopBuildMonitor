using System;
using System.ComponentModel.Composition;

namespace BuildMonitor.Plugin
{
	[Export(typeof (AbstractNotifier))]
	public abstract class AbstractNotifier
	{
		public abstract void AnnounceBrokenBuild(string buildName, string requestorNetworkId, DateTime requestTime);

		public abstract void AnnounceResolvedBuild(string buildName, string requestorNetworkId, DateTime requestTime);
	}
}
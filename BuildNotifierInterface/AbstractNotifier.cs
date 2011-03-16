using System.ComponentModel.Composition;

namespace BuildMonitor.Plugin
{
	[Export]
	public abstract class AbstractNotifier
	{
		public abstract void AnnounceBrokenBuild(string buildName, string requestorNetworkId);

		public abstract void AnnounceResolvedBuild(string buildName, string requestorNetworkId);
	}
}
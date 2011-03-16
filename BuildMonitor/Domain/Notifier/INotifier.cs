namespace BuildMonitor.Domain.Notifier
{
	public interface INotifier
	{
		void SetBuildQuality(BuildStatus currentBuildStatus, BuildStatus previousBuildStatus, string requestorName);
	}
}
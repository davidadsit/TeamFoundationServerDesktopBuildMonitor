namespace BuildMonitor.Domain.Monitor
{
	public interface IContinousIntegrationServerMonitor
	{
		BuildStatus GetBuildStatus();
	}
}
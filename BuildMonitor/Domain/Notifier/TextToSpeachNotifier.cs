using SpeechLib;

namespace BuildMonitor.Domain.Notifier
{
	public class TextToSpeachNotifier : INotifier
	{
		private readonly string _announcement;

		public TextToSpeachNotifier(string announcement)
		{
			_announcement = announcement;
		}

		public void SetBuildQuality(BuildStatus currentBuildStatus, BuildStatus previousBuildStatus, string requestorName)
		{
			SpVoice voiceClass = new SpVoice();
			voiceClass.Speak(string.Format(_announcement, requestorName));
		}
	}
}
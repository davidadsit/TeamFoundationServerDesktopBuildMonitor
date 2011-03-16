using System;
using BuildMonitor.Plugin;

namespace TextToSpeechPlugin
{
	public class TextToSpeechNotifier : AbstractNotifier
	{
		public const string BuildCorrected = "Congratulations! {0} fixed the {1} build.";
		public const string BuildFailed = "It is unfortunate, but {0} broke the {1} build.";
		private readonly ISpeechLibrary _speechLibrary;

		public TextToSpeechNotifier(ISpeechLibrary speechLibrary)
		{
			_speechLibrary = speechLibrary;
		}

		public override void AnnounceBrokenBuild(string buildName, string requestorNetworkId, DateTime requestTime)
		{
			string announcement = string.Format(BuildFailed, requestorNetworkId, buildName);
			_speechLibrary.Speak(announcement);
		}

		public override void AnnounceResolvedBuild(string buildName, string requestorNetworkId, DateTime requestTime)
		{
			string announcement = string.Format(BuildCorrected, requestorNetworkId, buildName);
			_speechLibrary.Speak(announcement);
		}
	}
}
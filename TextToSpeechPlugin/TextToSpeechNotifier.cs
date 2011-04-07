using System;
using System.IO;
using BuildMonitor.Plugin;

namespace TextToSpeechPlugin
{
	public class TextToSpeechNotifier : AbstractNotifier
	{
		public const string BuildCorrected = "Congratulations! {0} fixed the {1} build.";
		public const string BuildFailed = "It is unfortunate, but {0} broke the {1} build.";
		private const string _configFileName = "TextToSpeech.config";
		private readonly IConfigurationManager<TextToSpeechConfiguration> _configurationManager;
		private readonly ISpeechLibrary _speechLibrary;
		private FileInfo _configFileInfo;

		public TextToSpeechNotifier(ISpeechLibrary speechLibrary,
		                            IConfigurationManager<TextToSpeechConfiguration> configurationManager)
		{
			_speechLibrary = speechLibrary;
			_configurationManager = configurationManager;
			_configFileInfo = new FileInfo(_configFileName);
		}

		public override void AnnounceBrokenBuild(string buildName, string requestorNetworkId, DateTime requestTime)
		{
			TextToSpeechConfiguration textToSpeechConfiguration = _configurationManager.Load();
			string name = textToSpeechConfiguration.GetNameFromNetworkId(requestorNetworkId);
			string announcement = string.Format(BuildFailed, name, buildName);
			_speechLibrary.Speak(announcement);
		}

		public override void AnnounceResolvedBuild(string buildName, string requestorNetworkId, DateTime requestTime)
		{
			TextToSpeechConfiguration textToSpeechConfiguration = _configurationManager.Load();
			string name = textToSpeechConfiguration.GetNameFromNetworkId(requestorNetworkId);
			string announcement = string.Format(BuildCorrected, name, buildName);
			_speechLibrary.Speak(announcement);
		}
	}
}
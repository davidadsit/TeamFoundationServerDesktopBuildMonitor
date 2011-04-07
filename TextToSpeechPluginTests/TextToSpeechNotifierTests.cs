using System;
using System.IO;
using BuildMonitor.Plugin;
using Moq;
using NUnit.Framework;
using TextToSpeechPlugin;

namespace TextToSpeechPluginTests
{
	[TestFixture]
	public class TextToSpeechNotifierTests
	{
		private const string buildName = "Global";
		private const string requestorId = "Requestor ID";
		private const string requestorName = "Requestor Name";
		private Mock<IConfigurationManager<TextToSpeechConfiguration>> _configurationManagerMock;
		private Mock<ISpeechLibrary> _speechLibraryMock;
		private TextToSpeechNotifier _textToSpeechNotifier;

		private TextToSpeechConfiguration textToSpeechConfiguration;

		[SetUp]
		public void SetUp()
		{
			_speechLibraryMock = new Mock<ISpeechLibrary>();
			_configurationManagerMock = new Mock<IConfigurationManager<TextToSpeechConfiguration>>();
			_textToSpeechNotifier = new TextToSpeechNotifier(_speechLibraryMock.Object, _configurationManagerMock.Object);
			textToSpeechConfiguration = new TextToSpeechConfiguration();
			textToSpeechConfiguration.AddNetworkIdToNameMap(requestorId, requestorName);
		}

		[TearDown]
		public void TearDown()
		{
			_speechLibraryMock.Verify();
			_configurationManagerMock.Verify();
		}

		[Test]
		public void AnnounceBrokenBuild_SendsFormattedStringToISpeechLibrary()
		{
			string buildFailed = string.Format(TextToSpeechNotifier.BuildFailed, requestorName, buildName);
			_configurationManagerMock.Setup(x => x.Load()).Returns(textToSpeechConfiguration);
			_speechLibraryMock.Setup(x => x.Speak(buildFailed)).Verifiable();

			_textToSpeechNotifier.AnnounceBrokenBuild(buildName, requestorId, DateTime.Now);
		}

		[Test]
		public void AnnounceResolvedBuild_SendsFormattedStringToISpeechLibrary()
		{
			string buildFailed = string.Format(TextToSpeechNotifier.BuildCorrected, requestorName, buildName);
			_configurationManagerMock.Setup(x => x.Load()).Returns(textToSpeechConfiguration);
			_speechLibraryMock.Setup(x => x.Speak(buildFailed)).Verifiable();

			_textToSpeechNotifier.AnnounceResolvedBuild(buildName, requestorId, DateTime.Now);
		}
	}
}
using System;
using Moq;
using NUnit.Framework;
using TextToSpeechPlugin;

namespace TextToSpeechPluginTests
{
	[TestFixture]
	public class TextToSpeechNotifierTests
	{
		private Mock<ISpeechLibrary> _speechLibraryMock;
		private TextToSpeechNotifier _textToSpeechNotifier;

		[SetUp]
		public void SetUp()
		{
			_speechLibraryMock = new Mock<ISpeechLibrary>();
			_textToSpeechNotifier = new TextToSpeechNotifier(_speechLibraryMock.Object);
		}

		[TearDown]
		public void TearDown()
		{
			_speechLibraryMock.Verify();
		}

		[Test]
		public void AnnounceBrokenBuild_SendsFormattedStringToISpeechLibrary()
		{
			const string buildName = "Global";
			const string requestorId = "Requestor ID";
			string buildFailed = string.Format(TextToSpeechNotifier.BuildFailed, requestorId, buildName);
			_speechLibraryMock.Setup(x => x.Speak(buildFailed)).Verifiable();

			_textToSpeechNotifier.AnnounceBrokenBuild(buildName, requestorId, DateTime.Now);
		}

		[Test]
		public void AnnounceResolvedBuild_SendsFormattedStringToISpeechLibrary()
		{
			const string buildName = "Global";
			const string requestorId = "Requestor ID";
			string buildCorrected = string.Format(TextToSpeechNotifier.BuildCorrected, requestorId, buildName);
			_speechLibraryMock.Setup(x => x.Speak(buildCorrected)).Verifiable();

			_textToSpeechNotifier.AnnounceResolvedBuild(buildName, requestorId, DateTime.Now);
		}
	}
}
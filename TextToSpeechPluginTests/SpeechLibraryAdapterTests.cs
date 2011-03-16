using System;
using NUnit.Framework;
using TextToSpeechPlugin;

namespace Tests
{
	[TestFixture]
	public class SpeechLibraryAdapterTests
	{
		private SpeechLibraryAdapter _speechLibraryAdapter;

		[SetUp]
		public void SetUp()
		{
			_speechLibraryAdapter = new SpeechLibraryAdapter();
		}

		[Test]
		public void ImplementsISpeechLibrary()
		{
			Assert.IsInstanceOf(typeof (ISpeechLibrary), _speechLibraryAdapter);
		}

		[Test]
		public void SpeaksHelloWorld()
		{
			_speechLibraryAdapter.Speak("Hello, World!");
		}

		[Test]
		public void GetVoices()
		{
			Console.Out.WriteLine("Found {0} voice(s).", _speechLibraryAdapter.VoiceCount);
		}
	}
}
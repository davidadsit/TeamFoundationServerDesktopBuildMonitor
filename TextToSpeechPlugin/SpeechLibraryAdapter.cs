using SpeechLib;

namespace TextToSpeechPlugin
{
	public class SpeechLibraryAdapter : ISpeechLibrary
	{
		private readonly SpVoice _voice;

		public SpeechLibraryAdapter()
		{
			_voice = new SpVoice();
		}

		public int VoiceCount
		{
			get
			{
				return _voice.GetVoices().Count;
			}
		}

		public void Speak(string text)
		{
			_voice.Speak(text);
		}
	}
}
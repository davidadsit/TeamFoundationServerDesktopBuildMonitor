using System.Collections.Generic;

namespace TextToSpeechPlugin
{
	public class TextToSpeechConfiguration
	{
		public TextToSpeechConfiguration()
		{
			NetworkIdToNameMap = new Dictionary<string, string>();
		}

		public Dictionary<string, string> NetworkIdToNameMap { get; set; }

		public void AddNetworkIdToNameMap(string id, string name)
		{
			NetworkIdToNameMap.Add(id, name);
		}

		public string GetNameFromNetworkId(string id)
		{
			return NetworkIdToNameMap[id];
		}
	}
}
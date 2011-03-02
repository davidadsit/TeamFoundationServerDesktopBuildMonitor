using System;

namespace Domain
{
	public class ServerBuild
	{
		public ServerBuild(Uri serverUri, Uri buildUri)
		{
			ServerUri = serverUri;
			BuildUri = buildUri;
		}

		public ServerBuild()
		{
		}

		public Uri ServerUri { get; set; }
		public Uri BuildUri { get; set; }
	}
}
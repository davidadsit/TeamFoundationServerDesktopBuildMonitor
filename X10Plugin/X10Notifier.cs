using System;
using BuildMonitor.Plugin;

namespace X10Plugin
{
	public class X10Notifier : AbstractNotifier
	{
		private readonly IX10Adapter _x10Adapter;

		public X10Notifier(IX10Adapter x10Adapter)
		{
			_x10Adapter = x10Adapter;
		}

		public override void AnnounceBrokenBuild(string buildName, string requestorNetworkId, DateTime requestTime)
		{
			if (ShouldBeOffRegarlessOfBuildStatus(requestTime))
			{
				_x10Adapter.AllLightsOff();
			}
			else
			{
				_x10Adapter.RedLightOn();
			}
		}

		public override void AnnounceResolvedBuild(string buildName, string requestorNetworkId, DateTime requestTime)
		{
			if (ShouldBeOffRegarlessOfBuildStatus(requestTime))
			{
				_x10Adapter.AllLightsOff();
			}
			else
			{
				_x10Adapter.GreenLightOn();
			}
		}

		private static bool ShouldBeOffRegarlessOfBuildStatus(DateTime requestTime)
		{
			bool afterWorkEnds = requestTime.Hour >= 17;
			bool beforeWorkStarts = requestTime.Hour < 8;
			bool officeIsClosed = requestTime.DayOfWeek == DayOfWeek.Saturday || requestTime.DayOfWeek == DayOfWeek.Sunday;
			return beforeWorkStarts || afterWorkEnds || officeIsClosed;
		}
	}
}
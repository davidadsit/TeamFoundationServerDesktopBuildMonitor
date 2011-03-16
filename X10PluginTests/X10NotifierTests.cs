using System;
using Moq;
using NUnit.Framework;
using X10Plugin;

namespace X10PluginTests
{
	[TestFixture]
	public class X10NotifierTests
	{
		private X10Notifier _textToSpeechNotifier;
		private Mock<IX10Adapter> _x10AdapterMock;

		[SetUp]
		public void SetUp()
		{
			_x10AdapterMock = new Mock<IX10Adapter>();
			_textToSpeechNotifier = new X10Notifier(_x10AdapterMock.Object);
		}

		[TearDown]
		public void TearDown()
		{
			_x10AdapterMock.Verify();
		}

		[Test]
		public void AnnounceBrokenBuild_SendsAllOffToIX10Adapter_WhenDateIsAfter5pm()
		{
			_x10AdapterMock.Setup(x => x.AllLightsOff()).Verifiable();
			_textToSpeechNotifier.AnnounceBrokenBuild(string.Empty, string.Empty, DateTime.Parse("03/04/2011 17:01"));
		}


		[Test]
		public void AnnounceBrokenBuild_SendsAllOffToIX10Adapter_WhenDateIsBefore8am()
		{
			_x10AdapterMock.Setup(x => x.AllLightsOff()).Verifiable();
			_textToSpeechNotifier.AnnounceBrokenBuild(string.Empty, string.Empty, DateTime.Parse("03/04/2011 07:59"));
		}

		[Test]
		public void AnnounceBrokenBuild_SendsAllOffToIX10Adapter_WhenDateIsSaturday()
		{
			_x10AdapterMock.Setup(x => x.AllLightsOff()).Verifiable();
			_textToSpeechNotifier.AnnounceBrokenBuild(string.Empty, string.Empty, DateTime.Parse("03/05/2011"));
		}

		[Test]
		public void AnnounceBrokenBuild_SendsAllOffToIX10Adapter_WhenDateIsSunday()
		{
			_x10AdapterMock.Setup(x => x.AllLightsOff()).Verifiable();
			_textToSpeechNotifier.AnnounceBrokenBuild(string.Empty, string.Empty, DateTime.Parse("03/06/2011"));
		}

		[Test]
		public void AnnounceBrokenBuild_SendsRedLightOnToIX10Adapter()
		{
			_x10AdapterMock.Setup(x => x.RedLightOn()).Verifiable();
			_textToSpeechNotifier.AnnounceBrokenBuild(string.Empty, string.Empty, DateTime.Parse("03/04/2011 13:00"));
		}

		[Test]
		public void AnnounceResolvedBuild_SendsAllOffToIX10Adapter_WhenDateIsAfter5pm()
		{
			_x10AdapterMock.Setup(x => x.AllLightsOff()).Verifiable();
			_textToSpeechNotifier.AnnounceResolvedBuild(string.Empty, string.Empty, DateTime.Parse("03/04/2011 17:01"));
		}

		[Test]
		public void AnnounceResolvedBuild_SendsAllOffToIX10Adapter_WhenDateIsBefore8am()
		{
			_x10AdapterMock.Setup(x => x.AllLightsOff()).Verifiable();
			_textToSpeechNotifier.AnnounceResolvedBuild(string.Empty, string.Empty, DateTime.Parse("03/04/2011 07:59"));
		}

		[Test]
		public void AnnounceResolvedBuild_SendsAllOffToIX10Adapter_WhenDateIsSaturday()
		{
			_x10AdapterMock.Setup(x => x.AllLightsOff()).Verifiable();
			_textToSpeechNotifier.AnnounceResolvedBuild(string.Empty, string.Empty, DateTime.Parse("03/05/2011"));
		}

		[Test]
		public void AnnounceResolvedBuild_SendsAllOffToIX10Adapter_WhenDateIsSunday()
		{
			_x10AdapterMock.Setup(x => x.AllLightsOff()).Verifiable();
			_textToSpeechNotifier.AnnounceResolvedBuild(string.Empty, string.Empty, DateTime.Parse("03/06/2011"));
		}

		[Test]
		public void AnnounceResolvedBuild_SendsGreenLightOnToIX10Adapter()
		{
			_x10AdapterMock.Setup(x => x.GreenLightOn()).Verifiable();
			_textToSpeechNotifier.AnnounceResolvedBuild(string.Empty, string.Empty, DateTime.Parse("03/04/2011 13:00"));
		}
	}
}
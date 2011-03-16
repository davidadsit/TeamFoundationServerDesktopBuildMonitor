namespace X10Plugin.LampStates
{
	internal class X10GreenLamp : X10LampState
	{
		protected override string RedLamp
		{
			get { return Off; }
		}

		protected override string GreenLamp
		{
			get { return On; }
		}
	}
}
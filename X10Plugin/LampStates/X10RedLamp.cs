namespace X10Plugin.LampStates
{
	internal class X10RedLamp : X10LampState
	{
		protected override string RedLamp
		{
			get { return On; }
		}

		protected override string GreenLamp
		{
			get { return Off; }
		}
	}
}
namespace X10Plugin.LampStates
{
	public class X10NoLamps : X10LampState
	{
		protected override string RedLamp
		{
			get { return Off; }
		}

		protected override string GreenLamp
		{
			get { return Off; }
		}
	}
}
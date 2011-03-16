namespace X10Plugin.LampStates
{
	public abstract class X10LampState
	{
		protected const string Off = "off";
		protected const string On = "on";
		private const string Instruction = "1 a1{0} a2{1}";

		public static X10LampState AllOff
		{
			get { return new X10NoLamps(); }
		}

		public static X10LampState Red
		{
			get { return new X10RedLamp(); }
		}

		public static X10LampState Green
		{
			get { return new X10GreenLamp(); }
		}

		protected abstract string RedLamp { get; }
		protected abstract string GreenLamp { get; }

		public string GetInstruction()
		{
			return string.Format(Instruction, GreenLamp, RedLamp);
		}
	}
}
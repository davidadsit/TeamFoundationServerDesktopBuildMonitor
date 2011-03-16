using System.Diagnostics;
using System.Reflection;
using X10Plugin.LampStates;

namespace X10Plugin
{
	public class X10Adapter : IX10Adapter
	{
		private readonly string _x10InteractionApplicationFilePath;

		public X10Adapter()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			_x10InteractionApplicationFilePath = string.Format(@"""{0}""",
			                                                   assembly.Location.Replace(assembly.FullName, "Plugins\\cm17a.exe"));
		}

		public void AllLightsOff()
		{
			SendCommandToX10Devices(X10LampState.AllOff.GetInstruction());
		}

		public void GreenLightOn()
		{
			SendCommandToX10Devices(X10LampState.Green.GetInstruction());
		}

		public void RedLightOn()
		{
			SendCommandToX10Devices(X10LampState.Red.GetInstruction());
		}

		private void SendCommandToX10Devices(string arguments)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			                             	{
			                             		UseShellExecute = false,
			                             		CreateNoWindow = true,
			                             		FileName = _x10InteractionApplicationFilePath
			                             	};
			Process process = new Process {StartInfo = startInfo};
			process.StartInfo.Arguments = arguments;
			process.Start();
		}
	}
}
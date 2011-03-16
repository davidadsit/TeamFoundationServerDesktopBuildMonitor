using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using BuildMonitor.Domain;
using BuildMonitor.Plugin;
using CCTfsWrapper;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using Tools;
using BuildStatus = Microsoft.TeamFoundation.Build.Client.BuildStatus;

namespace BuildMonitor
{
	public partial class BuildStatusForm : Form
	{
		private readonly List<ServerBuild> _serverBuilds = new List<ServerBuild>();
		private ImageIndex _lastBuildStatus;
		private Thread _monitorThread;
		private Dictionary<string, string> _userNames;

		public BuildStatusForm()
		{
			InitializeComponent();
			SetupUsers();
			Stream greenBallStream =
				Assembly.GetExecutingAssembly().GetManifestResourceStream("CCTfsWrapper.Images.GreenBall.gif");
			Stream redBallStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CCTfsWrapper.Images.RedBall.gif");
			Stream yellowBallStream =
				Assembly.GetExecutingAssembly().GetManifestResourceStream("CCTfsWrapper.Images.YellowBall.ico");
			ImageList imageList = new ImageList();
			imageList.Images.Add(Image.FromStream(greenBallStream));
			imageList.Images.Add(Image.FromStream(yellowBallStream));
			imageList.Images.Add(Image.FromStream(redBallStream));
			BuildListView.SmallImageList = imageList;

			GetBuildsFromRegistry();
			MonitorBuilds();
		}

		[Import]
		public IEnumerable<AbstractNotifier> Notifiers { get; set; }

		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType,
		                                    int dwLogonProvider, out SafeTokenHandle phToken);

		private delegate void AddItemCallback(ListViewItem listViewItem);

		private delegate void ClearItemsCallback();

		private static ImageIndex GetImageIndex(BuildStatus buildStatus)
		{
			switch (buildStatus)
			{
				case BuildStatus.Succeeded:
					return ImageIndex.Green;
				case BuildStatus.InProgress:
					return ImageIndex.Yellow;
				default:
					return ImageIndex.Red;
			}
		}

		private enum ImageIndex
		{
			Green = 0,
			Yellow = 1,
			Red = 2
		}

		private static void SetRadiatorBuildStatusColor(ImageIndex overallImageIndex)
		{
			try
			{
				string userName = ConfigurationManager.AppSettings["RadiatorUserName"];
				string domain = ConfigurationManager.AppSettings["RadiatorDomain"];
				string password = ConfigurationManager.AppSettings["RadiatorPassword"];
				using (new Impersonator(userName, domain, password))
				{
					string buildStatusColorCss = string.Format(".buildStatusColor {{ background-color: {0}; }}", overallImageIndex);

					string cssFilePath = ConfigurationManager.AppSettings["RadiatorCssFilePath"];
					string cssFileContents = File.ReadAllText(cssFilePath);

					cssFileContents = cssFileContents.Replace(".buildStatusColor { background-color: Red; }", buildStatusColorCss);
					cssFileContents = cssFileContents.Replace(".buildStatusColor { background-color: Yellow; }", buildStatusColorCss);
					cssFileContents = cssFileContents.Replace(".buildStatusColor { background-color: Green; }", buildStatusColorCss);

					File.WriteAllText(cssFilePath, cssFileContents);
				}
			}
			catch
			{
			}
		}

		private void AddBuilds(List<ServerBuild> serverBuilds)
		{
			foreach (ServerBuild serverBuild in serverBuilds)
			{
				int count =
					(from sb in _serverBuilds
					 where sb.ServerUri == serverBuild.ServerUri && sb.BuildUri == serverBuild.BuildUri
					 select sb).ToList().Count;
				if (count == 0)
				{
					_serverBuilds.Add(serverBuild);
				}
			}
		}

		private void AddItem(ListViewItem listViewItem)
		{
			if (BuildListView.InvokeRequired)
			{
				AddItemCallback d = AddItem;
				Invoke(d, new object[] {listViewItem});
			}
			else
			{
				BuildListView.Items.Add(listViewItem);
			}
		}

		private void AnnounceCulprit(string culprit)
		{
			foreach (AbstractNotifier notifier in Notifiers)
			{
				notifier.AnnounceBrokenBuild("Global", culprit, DateTime.Now);
			}
		}

		private void BuildListView_KeyDown(object sender, KeyEventArgs e)
		{
			_monitorThread.Abort();
			if (e.KeyCode == Keys.Delete)
			{
				foreach (ListViewItem selectedItem in BuildListView.SelectedItems)
				{
					ServerBuild selectedServerBuild = (ServerBuild) selectedItem.Tag;
					_serverBuilds.Remove(selectedServerBuild);
				}
			}
			SaveBuildList();
			MonitorBuilds();
		}

		private void ClearItems()
		{
			if (BuildListView.InvokeRequired)
			{
				ClearItemsCallback d = ClearItems;
				Invoke(d, new object[] {});
			}
			else
			{
				BuildListView.Items.Clear();
			}
		}

		private IBuildServer GetBuildServer(string serverUri)
		{
			TfsTeamProjectCollection teamProjectCollection =
				TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(serverUri));
			IBuildServer buildServer = teamProjectCollection.GetService<IBuildServer>();
			return buildServer;
		}

		private void GetBuildsFromRegistry()
		{
			string serverBuildsKeyName = string.Format("SOFTWARE\\{0}\\ServerBuilds", Application.ProductName);
			RegistryKey serverBuildsKey = Registry.LocalMachine.OpenSubKey(serverBuildsKeyName);
			if (serverBuildsKey != null)
			{
				foreach (string serverKeyName in serverBuildsKey.GetSubKeyNames())
				{
					ServerBuild serverBuild = new ServerBuild();
					RegistryKey serverBuildKey = serverBuildsKey.OpenSubKey(serverKeyName);
					serverBuild.ServerUri = new Uri(serverBuildKey.GetValue("ServerUri").ToString());
					serverBuild.BuildUri = new Uri(serverBuildKey.GetValue("BuildUri").ToString());
					_serverBuilds.Add(serverBuild);
				}
			}
		}

		private void ListBuildsForm_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				ShowInTaskbar = false;
			}
			else
			{
				ShowInTaskbar = true;
			}
		}

		private void MonitorBuilds()
		{
			_monitorThread = new Thread(ShowBuilds);
			_monitorThread.Start();
		}

		private void SaveBuildList()
		{
			string serverBuildsKey = "SOFTWARE\\" + Application.ProductName + "\\ServerBuilds";
			if (Registry.LocalMachine.OpenSubKey(serverBuildsKey) != null)
			{
				Registry.LocalMachine.DeleteSubKeyTree(serverBuildsKey);
			}
			Registry.LocalMachine.CreateSubKey(serverBuildsKey);

			int count = 0;
			foreach (ServerBuild serverBuild in _serverBuilds)
			{
				string serverBuildKeyName = string.Format("SOFTWARE\\{0}\\ServerBuilds\\ServerBuild{1}", Application.ProductName,
				                                          count);
				RegistryKey serverBuildKey = Registry.LocalMachine.CreateSubKey(serverBuildKeyName);
				serverBuildKey.SetValue("ServerUri", serverBuild.ServerUri.ToString());
				serverBuildKey.SetValue("BuildUri", serverBuild.BuildUri.ToString());
				count++;
			}
		}

		private void SetSystemTrayIcon(ImageIndex imageIndex)
		{
			string imageResourceString;
			switch (imageIndex)
			{
				case ImageIndex.Green:
					imageResourceString = "CCTfsWrapper.Images.GreenBall.ico";
					break;
				case ImageIndex.Yellow:
					imageResourceString = "CCTfsWrapper.Images.GreenBall.ico";
					break;
				default:
					imageResourceString = "CCTfsWrapper.Images.RedBall.ico";
					break;
			}
			Stream iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResourceString);

			systemTrayNotifyIcon.Icon = new Icon(iconStream);
		}

		private void SetX10IntegrationStatus(ImageIndex imageIndex)
		{
			const string GREEN_LAMP_CODE = "a1";
			const string RED_LAMP_CODE = "a2";
			Process proc = new Process();
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.FileName = string.Format(@"""{0}""",
			                                        Assembly.GetExecutingAssembly().Location.Replace("CCTfsWrapper.exe",
			                                                                                         "ExternalPrograms\\cm17a.exe"));
			string greenLampStatus;
			string redLampStatus;
			if (DateTime.Now.Hour >= 17 || DateTime.Today.DayOfWeek == DayOfWeek.Saturday ||
			    DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
			{
				greenLampStatus = "off";
				redLampStatus = "off";
			}
			else
			{
				switch (imageIndex)
				{
					case ImageIndex.Green:
						greenLampStatus = "on";
						redLampStatus = "off";
						break;
					case ImageIndex.Yellow:
						greenLampStatus = "on";
						redLampStatus = "on";
						break;
					default:
						greenLampStatus = "off";
						redLampStatus = "on";
						break;
				}
			}
			proc.StartInfo.Arguments = string.Format("1 {0}{1} {2}{3}", GREEN_LAMP_CODE, greenLampStatus, RED_LAMP_CODE,
			                                         redLampStatus);
			proc.Start();
		}

		private void SetupUsers()
		{
			_userNames = new Dictionary<string, string>();
			_userNames.Add("CTAC\\DLaxman", "Dave Laxman");
			_userNames.Add("CTAC\\JCooper", "It's not likely but it seems that Jim Cooper");
			_userNames.Add("CTAC\\RVidal", "Richard Vidal");
			_userNames.Add("CTAC\\MMeans", "Marc Means");
			_userNames.Add("CTAC\\RDunn", "Bob Dunn");
			_userNames.Add("CTAC\\MFisk", "Mike Fisk");
			_userNames.Add("CTAC\\AFraikin", "Antoine Fraikin");
			_userNames.Add("CTAC\\MReeve", "Matt Reeve");
			_userNames.Add("CTAC\\ASadler", "Aaron Sadler");
			_userNames.Add("CTAC\\TWilson", "Well, I guess it can happen...  Tyler Wilson");
		}

		private void ShowBuilds()
		{
			while (true)
			{
				try
				{
					ClearItems();
					ImageIndex overallImageIndex = ImageIndex.Green;
					string culprit = "";
					foreach (ServerBuild serverBuild in _serverBuilds)
					{
						IBuildServer buildServer = GetBuildServer(serverBuild.ServerUri.ToString());
						IBuildDefinition buildDefinition = buildServer.QueryBuildDefinitionsByUri(new[] {serverBuild.BuildUri})[0];
						string[] buildInformation = new string[2];
						buildInformation[0] = buildDefinition.Name;

						IBuildDetail lastBuildDetails =
							buildServer.QueryBuildsByUri(new[] {buildDefinition.LastBuildUri}, null, QueryOptions.None)[0];
						if (lastBuildDetails != null)
						{
							buildInformation[1] = lastBuildDetails.Status.ToString();
						}
						ImageIndex imageIndex = lastBuildDetails == null ? ImageIndex.Red : GetImageIndex(lastBuildDetails.Status);
						ListViewItem listViewItem = new ListViewItem(buildInformation, (int) imageIndex);
						listViewItem.Tag = serverBuild;
						AddItem(listViewItem);
						if (imageIndex == ImageIndex.Red)
						{
							overallImageIndex = ImageIndex.Red;
							culprit = lastBuildDetails == null ? "Unable to find build" : lastBuildDetails.RequestedFor;
						}
						else if (imageIndex == ImageIndex.Yellow && overallImageIndex == ImageIndex.Green)
						{
							overallImageIndex = ImageIndex.Yellow;
						}
					}
					SetSystemTrayIcon(overallImageIndex);
					SetX10IntegrationStatus(overallImageIndex);
					if (overallImageIndex != _lastBuildStatus)
					{
						SetRadiatorBuildStatusColor(overallImageIndex);
						AnnounceCulprit(culprit);
						_lastBuildStatus = overallImageIndex;
					}
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
				}
				Thread.Sleep(5000);
			}
		}

		private void btnAddBuilds_Click(object sender, EventArgs e)
		{
			_monitorThread.Abort();
			SelectBuildsForm selectBuildsForm = new SelectBuildsForm();
			selectBuildsForm.ShowDialog();
			AddBuilds(selectBuildsForm.SelectedServerBuilds);
			SaveBuildList();
			MonitorBuilds();
		}

		private void systemTrayNotifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			WindowState = FormWindowState.Normal;
		}
	}

	public sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		private SafeTokenHandle() : base(true)
		{
		}

		protected override bool ReleaseHandle()
		{
			return CloseHandle(handle);
		}

		[DllImport("kernel32.dll")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr handle);
	}
}
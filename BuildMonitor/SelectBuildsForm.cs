using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BuildMonitor.Domain;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Server;
using Microsoft.Win32;

namespace CCTfsWrapper
{
	public partial class SelectBuildsForm : Form
	{
		private const string DEFAULT_PROJECT_NAME_KEY = "DefaultProject";
		private const string DEFAULT_SERVER_URI_KEY = "DefaultServerUri";
		private readonly string _baseRegistryKey = "SOFTWARE\\" + Application.ProductName;
		private Dictionary<string, Uri> _buildDictionary;

		public SelectBuildsForm()
		{
			InitializeComponent();
		}

		public List<ServerBuild> SelectedServerBuilds { get; set; }

		protected override void OnLoad(EventArgs e)
		{
			RegistryKey serverUriKey = Registry.LocalMachine.OpenSubKey(_baseRegistryKey);
			lblServerUri.Text = serverUriKey == null ? "" : serverUriKey.GetValue(DEFAULT_SERVER_URI_KEY).ToString();
			if (lblServerUri.Text != string.Empty)
			{
				PopulateProjects();
				RegistryKey projectUriKey = Registry.LocalMachine.OpenSubKey(_baseRegistryKey);
				string defaultProject = projectUriKey == null ? "" : projectUriKey.GetValue(DEFAULT_PROJECT_NAME_KEY).ToString();
				TeamProjectDropDown.SelectedText = defaultProject;
				ShowBuilds(defaultProject);
			}
			base.OnLoad(e);
		}

		private void AddBuildsButton_Click(object sender, EventArgs e)
		{
			SelectedServerBuilds = new List<ServerBuild>();
			foreach (ListViewItem selectedItem in BuildsListView.SelectedItems)
			{
				SelectedServerBuilds.Add(new ServerBuild(new Uri(lblServerUri.Text), _buildDictionary[selectedItem.Text]));
			}
			Close();
		}

		private void ChangeServerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string serverUri = InputBox.Show("Server Uri:", "Enter Server Uri");
			if (!string.IsNullOrEmpty(lblServerUri.Text))
			{
				Registry.LocalMachine.CreateSubKey(_baseRegistryKey).SetValue(DEFAULT_SERVER_URI_KEY, serverUri);

				lblServerUri.Text = serverUri;
				BuildsListView.Clear();
				PopulateProjects();
			}
		}

		private IBuildServer GetBuildServer()
		{
			IBuildServer buildServer;
			TfsTeamProjectCollection teamProjectCollection =
				TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(lblServerUri.Text));
			buildServer = teamProjectCollection.GetService<IBuildServer>();
			return buildServer;
		}

		private void PopulateProjects()
		{
			TeamProjectDropDown.Items.Clear();
			TeamFoundationServer tfs = TeamFoundationServerFactory.GetServer(lblServerUri.Text);
			ICommonStructureService projectCollection =
				(ICommonStructureService) tfs.GetService(typeof (ICommonStructureService));
			foreach (ProjectInfo projectInfo in projectCollection.ListProjects())
			{
				TeamProjectDropDown.Items.Add(projectInfo.Name);
			}
			TeamProjectDropDown.Enabled = true;
		}

		private void ShowBuilds(string projectName)
		{
			try
			{
				BuildsListView.Items.Clear();
				IBuildServer buildServer = GetBuildServer();
				IBuildDefinition[] buildDefinitions = buildServer.QueryBuildDefinitions(projectName);
				_buildDictionary = new Dictionary<string, Uri>();
				foreach (IBuildDefinition buildDetail in buildDefinitions)
				{
					_buildDictionary.Add(buildDetail.Name, buildDetail.Uri);
					string[] buildInformation = new string[2];
					buildInformation[0] = buildDetail.Name;
					buildInformation[1] = buildDetail.Uri.ToString();
					BuildsListView.Items.Add(buildDetail.Name);
				}
			}
			catch (Exception ex)
			{
			}
		}

		private void ShowBuilds()
		{
			ShowBuilds(TeamProjectDropDown.Text);
		}

		private void TeamProjectDropDown_SelectedIndexChanged(object sender, EventArgs e)
		{
			Registry.LocalMachine.CreateSubKey("SOFTWARE\\" + Application.ProductName).SetValue(DEFAULT_PROJECT_NAME_KEY,
			                                                                                    TeamProjectDropDown.SelectedItem.
			                                                                                    	ToString());
			ShowBuilds();
		}

		private void lblServerUri_Resize(object sender, EventArgs e)
		{
			ChangeServerLink.Left = lblServerUri.Left + lblServerUri.Width + 5;
		}
	}
}
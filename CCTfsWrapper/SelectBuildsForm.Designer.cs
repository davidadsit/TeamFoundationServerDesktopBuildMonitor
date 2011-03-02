namespace CCTfsWrapper
{
    partial class SelectBuildsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BuildsListView = new System.Windows.Forms.ListView();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblServerUri = new System.Windows.Forms.Label();
            this.ChangeServerLink = new System.Windows.Forms.LinkLabel();
            this.AddBuildsButton = new System.Windows.Forms.Button();
            this.TeamProjectDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BuildsListView
            // 
            this.BuildsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BuildsListView.Location = new System.Drawing.Point(15, 36);
            this.BuildsListView.Name = "BuildsListView";
            this.BuildsListView.Size = new System.Drawing.Size(721, 503);
            this.BuildsListView.TabIndex = 0;
            this.BuildsListView.UseCompatibleStateImageBehavior = false;
            this.BuildsListView.View = System.Windows.Forms.View.List;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(12, 12);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(41, 13);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "Server:";
            // 
            // lblServerUri
            // 
            this.lblServerUri.AutoSize = true;
            this.lblServerUri.Location = new System.Drawing.Point(59, 12);
            this.lblServerUri.Name = "lblServerUri";
            this.lblServerUri.Size = new System.Drawing.Size(77, 13);
            this.lblServerUri.TabIndex = 2;
            this.lblServerUri.Text = "[Select Server]";
            this.lblServerUri.Resize += new System.EventHandler(this.lblServerUri_Resize);
            // 
            // ChangeServerLink
            // 
            this.ChangeServerLink.AutoSize = true;
            this.ChangeServerLink.Location = new System.Drawing.Point(142, 12);
            this.ChangeServerLink.Name = "ChangeServerLink";
            this.ChangeServerLink.Size = new System.Drawing.Size(78, 13);
            this.ChangeServerLink.TabIndex = 3;
            this.ChangeServerLink.TabStop = true;
            this.ChangeServerLink.Text = "Change Server";
            this.ChangeServerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChangeServerLink_LinkClicked);
            // 
            // AddBuildsButton
            // 
            this.AddBuildsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBuildsButton.Location = new System.Drawing.Point(610, 545);
            this.AddBuildsButton.Name = "AddBuildsButton";
            this.AddBuildsButton.Size = new System.Drawing.Size(126, 23);
            this.AddBuildsButton.TabIndex = 4;
            this.AddBuildsButton.Text = "Add Selected Builds";
            this.AddBuildsButton.UseVisualStyleBackColor = true;
            this.AddBuildsButton.Click += new System.EventHandler(this.AddBuildsButton_Click);
            // 
            // TeamProjectDropDown
            // 
            this.TeamProjectDropDown.Enabled = false;
            this.TeamProjectDropDown.FormattingEnabled = true;
            this.TeamProjectDropDown.Location = new System.Drawing.Point(547, 9);
            this.TeamProjectDropDown.Name = "TeamProjectDropDown";
            this.TeamProjectDropDown.Size = new System.Drawing.Size(188, 21);
            this.TeamProjectDropDown.TabIndex = 5;
            this.TeamProjectDropDown.SelectedIndexChanged += new System.EventHandler(this.TeamProjectDropDown_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Team Project:";
            // 
            // SelectBuildsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 572);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TeamProjectDropDown);
            this.Controls.Add(this.AddBuildsButton);
            this.Controls.Add(this.ChangeServerLink);
            this.Controls.Add(this.lblServerUri);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.BuildsListView);
            this.Name = "SelectBuildsForm";
            this.Text = "Select Builds";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView BuildsListView;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblServerUri;
        private System.Windows.Forms.LinkLabel ChangeServerLink;
        private System.Windows.Forms.Button AddBuildsButton;
        private System.Windows.Forms.ComboBox TeamProjectDropDown;
        private System.Windows.Forms.Label label1;
    }
}
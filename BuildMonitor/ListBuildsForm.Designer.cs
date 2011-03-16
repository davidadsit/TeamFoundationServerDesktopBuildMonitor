namespace BuildMonitor
{
    partial class ListBuildsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListBuildsForm));
            this.BuildListView = new System.Windows.Forms.ListView();
            this.Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddBuilds = new System.Windows.Forms.Button();
            this.systemTrayNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // BuildListView
            // 
            this.BuildListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BuildListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Name,
            this.Status});
            this.BuildListView.FullRowSelect = true;
            this.BuildListView.Location = new System.Drawing.Point(0, 0);
            this.BuildListView.Name = "BuildListView";
            this.BuildListView.Size = new System.Drawing.Size(595, 378);
            this.BuildListView.TabIndex = 0;
            this.BuildListView.UseCompatibleStateImageBehavior = false;
            this.BuildListView.View = System.Windows.Forms.View.Details;
            this.BuildListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BuildListView_KeyDown);
            // 
            // Name
            // 
            this.Name.Text = "Build";
            this.Name.Width = 139;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 174;
            // 
            // btnAddBuilds
            // 
            this.btnAddBuilds.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddBuilds.Location = new System.Drawing.Point(0, 384);
            this.btnAddBuilds.Name = "btnAddBuilds";
            this.btnAddBuilds.Size = new System.Drawing.Size(595, 23);
            this.btnAddBuilds.TabIndex = 1;
            this.btnAddBuilds.Text = "Add Builds";
            this.btnAddBuilds.UseVisualStyleBackColor = true;
            this.btnAddBuilds.Click += new System.EventHandler(this.btnAddBuilds_Click);
            // 
            // systemTrayNotifyIcon
            // 
            this.systemTrayNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTrayNotifyIcon.Icon")));
            this.systemTrayNotifyIcon.Visible = true;
            this.systemTrayNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.systemTrayNotifyIcon_MouseClick);
            // 
            // ListBuildsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 407);
            this.Controls.Add(this.btnAddBuilds);
            this.Controls.Add(this.BuildListView);
            this.Text = "Builds";
            this.Resize += new System.EventHandler(this.ListBuildsForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView BuildListView;
        private System.Windows.Forms.ColumnHeader Name;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.Button btnAddBuilds;
        private System.Windows.Forms.NotifyIcon systemTrayNotifyIcon;
    }
}


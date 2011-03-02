using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CCTfsWrapper
{
	/// <summary>
	/// Summary description for InputBox.
	/// </summary>
	public class InputBox : Form
	{
		private static InputBox inputBox;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private readonly Container components;

		private Button buttonCancel;
		private Button buttonOK;
		private TextBox inputTextBox;
		private Label promptLabel;

		private InputBox()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public static string Show(string Prompt, string Title, string DefaultResponse)
		{
			if (inputBox == null)
			{
				inputBox = new InputBox();
			}
			inputBox.promptLabel.Text = Prompt;
			inputBox.Text = Title;
			inputBox.inputTextBox.Text = DefaultResponse;
			if (inputBox.ShowDialog() == DialogResult.OK)
			{
				return inputBox.inputTextBox.Text;
			}
			else
			{
				return String.Empty;
			}
		}

		public static string Show(string Prompt)
		{
			return Show(Prompt, Application.ProductName, String.Empty);
		}

		public static string Show(string Prompt, string Title)
		{
			return Show(Prompt, Title, String.Empty);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			buttonOK = new Button();
			buttonCancel = new Button();
			promptLabel = new Label();
			inputTextBox = new TextBox();
			SuspendLayout();
			// 
			// buttonOK
			// 
			buttonOK.DialogResult = DialogResult.OK;
			buttonOK.Location = new Point(304, 16);
			buttonOK.Name = "buttonOK";
			buttonOK.TabIndex = 0;
			buttonOK.Text = "OK";
			// 
			// buttonCancel
			// 
			buttonCancel.DialogResult = DialogResult.Cancel;
			buttonCancel.Location = new Point(304, 48);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "Cancel";
			// 
			// promptLabel
			// 
			promptLabel.Location = new Point(8, 16);
			promptLabel.Name = "promptLabel";
			promptLabel.Size = new Size(288, 104);
			promptLabel.TabIndex = 2;
			promptLabel.Text = "label1";
			// 
			// inputTextBox
			// 
			inputTextBox.Location = new Point(8, 128);
			inputTextBox.Name = "inputTextBox";
			inputTextBox.Size = new Size(368, 20);
			inputTextBox.TabIndex = 3;
			inputTextBox.Text = "";
			// 
			// InputBox
			// 
			AutoScaleBaseSize = new Size(5, 13);
			ClientSize = new Size(384, 164);
			Controls.Add(inputTextBox);
			Controls.Add(promptLabel);
			Controls.Add(buttonCancel);
			Controls.Add(buttonOK);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "InputBox";
			Text = "InputBox";
			ResumeLayout(false);
		}
	}
}
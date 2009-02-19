using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MTDBCreator
{
	/// <summary>
	/// Summary description for frmStatus.
	/// </summary>
	public class frmStatus : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.ProgressBar mbar_progress;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button mbtn_cancel;


		private System.Windows.Forms.Label mlbl_status;

		private delegate void SetControlString(string strMessage) ; 
		private delegate void SetProgressValue() ; 
		private delegate void NonArgFunc() ; 

		public delegate void dlgSetErrorMessage(string strError) ; 
		public delegate void dlgSetStatusMessage(string strStatus) ; 
		public delegate void dlgSetPercentComplete(int percentDone) ; 

		private int mint_percent_done = 0 ; 
		private int mint_step_size = 2 ;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button mbtn_close;
		private System.Windows.Forms.TextBox txtErrors;
		private System.Windows.Forms.Label label2; 

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmStatus()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			mbar_progress.Minimum = 0 ; 
			mbar_progress.Maximum = 100 ; 
			mbar_progress.Step = mint_step_size ; 
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblProgress = new System.Windows.Forms.Label();
			this.mbar_progress = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.mbtn_cancel = new System.Windows.Forms.Button();
			this.mlbl_status = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.mbtn_close = new System.Windows.Forms.Button();
			this.txtErrors = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblProgress
			// 
			this.lblProgress.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProgress.Location = new System.Drawing.Point(10, 10);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(72, 20);
			this.lblProgress.TabIndex = 0;
			this.lblProgress.Text = "Progress:";
			// 
			// mbar_progress
			// 
			this.mbar_progress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mbar_progress.Location = new System.Drawing.Point(82, 10);
			this.mbar_progress.Name = "mbar_progress";
			this.mbar_progress.Size = new System.Drawing.Size(308, 20);
			this.mbar_progress.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Status:";
			// 
			// mbtn_cancel
			// 
			this.mbtn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mbtn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.mbtn_cancel.Location = new System.Drawing.Point(160, 176);
			this.mbtn_cancel.Name = "mbtn_cancel";
			this.mbtn_cancel.Size = new System.Drawing.Size(80, 24);
			this.mbtn_cancel.TabIndex = 3;
			this.mbtn_cancel.Text = "Cancel";
			this.mbtn_cancel.Click += new System.EventHandler(this.mbtn_cancel_Click);
			// 
			// mlbl_status
			// 
			this.mlbl_status.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mlbl_status.Location = new System.Drawing.Point(58, 10);
			this.mlbl_status.Name = "mlbl_status";
			this.mlbl_status.Size = new System.Drawing.Size(332, 20);
			this.mlbl_status.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.mbar_progress);
			this.panel1.Controls.Add(this.lblProgress);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.DockPadding.All = 10;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(400, 40);
			this.panel1.TabIndex = 5;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.mlbl_status);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.DockPadding.All = 10;
			this.panel2.Location = new System.Drawing.Point(0, 40);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(400, 40);
			this.panel2.TabIndex = 6;
			// 
			// mbtn_close
			// 
			this.mbtn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mbtn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.mbtn_close.Location = new System.Drawing.Point(160, 176);
			this.mbtn_close.Name = "mbtn_close";
			this.mbtn_close.Size = new System.Drawing.Size(80, 24);
			this.mbtn_close.TabIndex = 8;
			this.mbtn_close.Text = "Close";
			this.mbtn_close.Visible = false;
			this.mbtn_close.Click += new System.EventHandler(this.mbtn_close_Click);
			// 
			// txtErrors
			// 
			this.txtErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtErrors.Location = new System.Drawing.Point(64, 88);
			this.txtErrors.Multiline = true;
			this.txtErrors.Name = "txtErrors";
			this.txtErrors.ReadOnly = true;
			this.txtErrors.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtErrors.Size = new System.Drawing.Size(328, 72);
			this.txtErrors.TabIndex = 10;
			this.txtErrors.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(10, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 20);
			this.label2.TabIndex = 9;
			this.label2.Text = "Errors:";
			// 
			// frmStatus
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.mbtn_cancel;
			this.ClientSize = new System.Drawing.Size(400, 206);
			this.ControlBox = false;
			this.Controls.Add(this.txtErrors);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.mbtn_close);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.mbtn_cancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmStatus";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Status";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void ClearErrorMessages() 
		{
			txtErrors.Text = "" ;
		}

		public bool HasErrorMessages
		{
			get
			{
				if (txtErrors.TextLength == 0)
					return false ;
				else
					return true ;
				
			}
		}

		public bool CloseButtonVisible
		{
			set 
			{
				mbtn_close.Visible = value ;
			}
		}

		public void SetPrecentComplete(int percent_done)
		{
			try
			{
				if (!IsHandleCreated)
					return ; 
				// if the new status is greater, or if its been reset to a newer value then update
				if (mint_percent_done + mint_step_size < percent_done || mint_percent_done > percent_done)
				{
					mint_percent_done = percent_done ; 
					Invoke(new SetProgressValue(this.SetProgressVal), null) ; 
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message + e.StackTrace) ; 
			}
		}

		/// <summary>
		/// Prevent this form from ever being closed.  It can be hidden/shown, but not closed.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(CancelEventArgs e)
		{
			try
			{
				base.OnClosing(e);
				e.Cancel = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + ex.StackTrace) ; 
			}
		}


		private void SetProgressVal()
		{
			try
			{
				mbar_progress.Value = mint_percent_done ; 
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + ex.StackTrace) ; 
			}
		}

		public void Reset()
		{
			try
			{
				mint_percent_done = 0 ; 
				SetProgressVal() ;

				this.ClearErrorMessages() ;
				this.CloseButtonVisible = false ;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + ex.StackTrace) ; 
			}
		}

		public void SetErrorMessage(string strError)
		{
			try
			{
				if (!IsHandleCreated)
					return ; 
				Invoke(new SetControlString(this.SetErrorString), new object [] {strError}) ; 
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString()) ; 
			}
		}
		
		public void SetStatusMessage(string strStatus)
		{
			try
			{
				if (!IsHandleCreated)
					return ; 
				Invoke(new SetControlString(this.SetStatusString), new object [] {strStatus}) ; 
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString()) ; 
			}
		}

		private void SetErrorString(string strMessage)
		{
			try
			{
				this.txtErrors.Text += strMessage + "\r\n" ; 
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + ex.StackTrace) ; 
			}
		}


		private void SetStatusString(string strMessage)
		{
			try
			{
				this.mlbl_status.Text = strMessage ; 
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + ex.StackTrace) ; 
			}
		}

		public void ShowStatusBox(object sender, object event_args)
		{
			try
			{
				this.Text = (string) event_args ;
				this.mint_percent_done = 0 ;
				mbar_progress.Value = 0 ;
				ShowDialog() ; 
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + ex.StackTrace) ; 
			}
		}

		private void mbtn_cancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.DialogResult = DialogResult.Cancel;
				this.Hide();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + ex.StackTrace) ; 
			}
		}

		private void mbtn_close_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.DialogResult = DialogResult.OK;
				this.Hide();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + ex.StackTrace) ; 
			}

		}
	}
}

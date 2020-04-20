using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Desktop.UI {
	/// <summary>
	/// AddPassword dialog support.
	/// </summary>
	public class AddPassword : System.Windows.Forms.Form {
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button submit;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.TextBox confirmPassword;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox systemName;
		private System.Windows.Forms.TextBox description;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker expires;
		private System.Windows.Forms.TextBox username;
		private System.Windows.Forms.Label label6;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor for the class.
		/// </summary>
		public AddPassword() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
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
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPassword));
            this.systemName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.confirmPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.expires = new System.Windows.Forms.DateTimePicker();
            this.username = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // systemName
            // 
            this.systemName.Location = new System.Drawing.Point(115, 28);
            this.systemName.Name = "systemName";
            this.systemName.Size = new System.Drawing.Size(250, 22);
            this.systemName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "System:";
            // 
            // description
            // 
            this.description.Location = new System.Drawing.Point(115, 137);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(471, 22);
            this.description.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(29, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description:";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(115, 82);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(250, 22);
            this.password.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(29, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(210, 222);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(90, 26);
            this.submit.TabIndex = 7;
            this.submit.Text = "&OK";
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(316, 222);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(90, 26);
            this.cancel.TabIndex = 8;
            this.cancel.Text = "&Cancel";
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // confirmPassword
            // 
            this.confirmPassword.Location = new System.Drawing.Point(115, 110);
            this.confirmPassword.Name = "confirmPassword";
            this.confirmPassword.PasswordChar = '*';
            this.confirmPassword.Size = new System.Drawing.Size(250, 22);
            this.confirmPassword.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(29, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "Confirm:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(29, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Expires:";
            // 
            // expires
            // 
            this.expires.Location = new System.Drawing.Point(115, 165);
            this.expires.Name = "expires";
            this.expires.Size = new System.Drawing.Size(240, 22);
            this.expires.TabIndex = 6;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(115, 54);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(250, 22);
            this.username.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(29, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "Username:";
            // 
            // AddPassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(512, 222);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.expires);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.confirmPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.description);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.systemName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Password";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// Called when the cancel button is click. The form is then closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancel_Click(object sender, System.EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			Close();
		}

		/// <summary>
		/// Called when the submit button is clicked. It then saves the 
		/// username/password combination.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void submit_Click(object sender, System.EventArgs e) {
			if( systemName.Text.Length == 0 ) {
				MessageBox.Show(this, "System name is a required value. Try again.");
				return;
			}
			if( password.Text != confirmPassword.Text ) {
				MessageBox.Show(this, "Passwords do not match. Try again.");
				return;
			}
			_SystemName = systemName.Text;
			_Username = username.Text;
			_Password = password.Text;
			_Description = description.Text;
			_Expires = expires.Value;
			this.DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// Gets/Sets the system name
		/// </summary>
		public string SystemName {
			get {
				return _SystemName;
			}
			set {
				_SystemName = value;
				systemName.Text = value;
			}
		}

		/// <summary>
		/// Gets/Sets the username
		/// </summary>
		public string Username {
			get {
				return _Username;
			}
			set {
				_Username = value;
				username.Text = _Username;
			}
		}

		/// <summary>
		///  Gets/sets the password
		/// </summary>
		public string Password {
			get {
				return _Password;
			}
			set {
				_Password = value;
				password.Text = value;
			}
		}

		/// <summary>
		/// Gets/sets the Description
		/// </summary>
		public string Description {
			get {
				return _Description;
			}
			set {
				_Description = value;
				description.Text = value;
			}
		}

		/// <summary>
		/// Gets/sets the Expires
		/// </summary>
		public DateTime Expires {
			get {
				return _Expires;
			}
			set {
				_Expires = value;
				expires.Value = value;
			}
		}

		/// <summary>
		/// When called will enter the edit mode for the form.
		/// </summary>
		public void EditMode() {
			isEditing = true;
			this.Text = "Edit Password";
		}

		private string _SystemName;
		private string _Username;
		private string _Password;
		private string _Description;
		private DateTime _Expires;
		private bool isEditing = false;
	}
}

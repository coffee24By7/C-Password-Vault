using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.DirectoryServices.AccountManagement;

using Framework.DomainLayer.User;
using Framework.DomainLayer.Security;
using Security.ApplicationSecurity;
using Framework.Logging;
using Framework.DomainLayer.Log;
using Framework.EntityObject;
using Framework.DomainLayer.Config;

namespace Desktop.UI {
	/// <summary>
	/// Main form for managing passwords.
	/// </summary>
	public class PasswordSafe : System.Windows.Forms.Form {
		private string passwordStoreFolder = @"C:\PasswordStore";

        # region class control definitions

        private Color TabBackGroundDefault = System.Drawing.Color.Transparent;
        private Color TabForeGroundDefault = System.Drawing.Color.Black;
        private Color HighlightTabBackGroundDefault = System.Drawing.Color.Transparent;
        private Color HighlightTabForeGroundDefault = System.Drawing.Color.Black;
        private Color SelectionOptionListBackGroundDefault = System.Drawing.Color.Transparent;
        private Color SelectedListBackGroundDefault = System.Drawing.Color.Transparent;
        private Color WindowBackGroundDefault = System.Drawing.Color.Transparent;
        private Form MyParent = null;
        private Hashtable configHT = new Hashtable();
        private LoginToken loginAuthenticationToken;
        private static Splash sp = new Splash();

        # endregion class control defintions

        private string passwordStoreName = @"C:\\PasswordStore\\" + Environment.UserName.ToString() + "_PasswordStore.xml";
        private System.Windows.Forms.DataGrid passwords;
		private System.Data.DataSet passwordData;
		private System.Data.DataView passwordView;
		private bool dataChanged = false;
        private System.Windows.Forms.Button add;
		private bool isClosing = false;
		private System.Windows.Forms.Timer logoutTimer;
		private DateTime logoutTime;
        private System.ComponentModel.IContainer components;
        private ToolTip toolTip1;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton save;
        private Telerik.WinControls.UI.RadButton delete;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton view;
        private Telerik.WinControls.UI.RadButton edit;
        private Telerik.WinControls.UI.RadLabel radLabel2;
		private string encryptKey;

        public PasswordSafe()
        {
        }

        public PasswordSafe(string verifyUser, string verifyPW)
        {
            RunWindow(verifyUser, verifyPW);

        }

        public void RunFromOutSide(string verifyUser, string verifyPW)
        {
            RunWindow(verifyUser, verifyPW);
        }

        private void RunWindow(string verifyUser, string verifyPW)
        {
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Retrieve the user's guid and then generate the encryption
			// key from it. This encrypt key is then used to load the
			// encrypted file into memory, decrypt it, and then use the
			// resulting XML to build the data set used in the program.
			//
            try
            {
                SetupColorScheme();
                Guid userGuid = (Guid) UserPrincipal.Current.Guid;
                encryptKey = encryptGuid(userGuid.ToString("N"));
                loadData();

                this.Focus();
                try { sp.Hide(); }
                catch { }
            }
            catch (System.Exception sysExp)
            {
                Framework.Logging.Log.Write(LogLevel.EXCEP, "Desktop", "Security", "Log", loginAuthenticationToken.workingAsUserInfo.UserName, "Error setting application to run : " + sysExp.Message.ToString());
            }
        }

        private void SetupColorScheme()
        {

            try { TabBackGroundDefault = System.Drawing.Color.FromName(ConfigurationManager.AppSettings["TabBackgroundColor"].ToString()); }
            catch { }
            try { TabForeGroundDefault = System.Drawing.Color.FromName(ConfigurationManager.AppSettings["TabForegroundColor"].ToString()); }
            catch { }
            try { HighlightTabBackGroundDefault = System.Drawing.Color.FromName(ConfigurationManager.AppSettings["HighlightTabBackgroundColor"].ToString()); }
            catch { }
            try { HighlightTabForeGroundDefault = System.Drawing.Color.FromName(ConfigurationManager.AppSettings["HighlightTabForegroundColor"].ToString()); }
            catch { }
            try { SelectionOptionListBackGroundDefault = System.Drawing.Color.FromName(ConfigurationManager.AppSettings["SelectionOptionListBackGroundColor"].ToString()); }
            catch { }
            try { SelectedListBackGroundDefault = System.Drawing.Color.FromName(ConfigurationManager.AppSettings["SelectedListBackGroundColor"].ToString()); }
            catch { }
            try { WindowBackGroundDefault = System.Drawing.Color.FromName(ConfigurationManager.AppSettings["WindowBackGroundColor"].ToString()); }
            catch { }

        }
		/// <summary>
		/// Used to create an encrypt key from the user's AD guid.
		/// </summary>
		/// <param name="strPassword">The string representation of the Guid.</param>
		/// <returns>An encryption key generated from the input argument.</returns>
		private string encryptGuid(string strPassword) {
			// fold the password. Don't do a complete fold since that's
			// easier to figure out.
			int len = strPassword.Length / 2;
			char[] chars = strPassword.ToCharArray();
			for( int i = 0; i < len; i++ ) {
				char swap = chars[i];
				chars[i] = chars[i+len-1];
				chars[i+len-1] = swap;
			}
			string foldedPassword = new string(chars);

			// convert the string into an array of bytes
			ASCIIEncoding asc = new ASCIIEncoding();
			byte[] b = asc.GetBytes(foldedPassword);

			// get the MD5 encoder and compute the hash of the password
			MD5 md5 = new MD5CryptoServiceProvider();
			md5.Initialize();
			byte[] r = md5.ComputeHash(b);

			// convert to base64 so it can be stored as a string of characters
			string str = Convert.ToBase64String(r);
			return str;
		}

		/// <summary>
		/// Loads the data from the encryted XML file.
		/// </summary>
		private void loadData() {

			passwordData = new DataSet();

			try {
				string path = Application.ExecutablePath;
				path = path.Substring(0, path.LastIndexOf("\\")+1);
				if( !path.EndsWith("\\") )
					path += "\\";
				passwordData.ReadXmlSchema(path+"passwordstore.xsd");
			}
			catch( Exception e ) {
				MessageBox.Show(this, "Error locating the 'PasswordStore.xsd' file. Plese contact support.\n"+e.Message,
					"Password Safe Error", MessageBoxButtons.OK);
				Environment.Exit(-1);
			}

			try {	
				if( !Directory.Exists(passwordStoreFolder) ) {
					throw new ApplicationException();
				}

				if( File.Exists(passwordStoreName) ) {
					TextReader tr = File.OpenText(passwordStoreName);
					string data = tr.ReadToEnd();
					try { 
						data = Crypt.Decrypt(data, encryptKey);
						tr.Close();
					}
					catch {
						MessageBox.Show(this, "Unable to decrypt password store. Please contact support.", 
							"Password Safe Error", MessageBoxButtons.OK);
						Environment.Exit(-1);
					}
					try {
						StringReader sr = new StringReader(data);
						passwordData.ReadXml(sr);
					}
					catch {
						MessageBox.Show(this, "Unable to read the Password Store data stream. Please contact support.",
							"Password Safe Error", MessageBoxButtons.OK);
						Environment.Exit(-1);
					}
				}
			}
			catch {
				MessageBox.Show(this, "There was an error reading from your C:\\PasswordStore drive. Make sure you have your home folder mapped "+
					"to your U: drive and that you have access rights to that drive and then try running Password Safe again. If "+
					"this problem persists, please contact support.", "Password Safe Error", MessageBoxButtons.OK);
				Environment.Exit(-1);
			}

			passwordView.Table = passwordData.Tables[0];

			//
			// Create a Grid Table Style. Map it to the "Customers" Table.
			//
			DataGridTableStyle aGridTableStyle = new DataGridTableStyle();
			aGridTableStyle.MappingName = "Entry";
			//
			// Create GridColumnStyle objects for the grid columns 
			//
			DataGridTextBoxColumn aCol1 = new DataGridTextBoxColumn();
			DataGridTextBoxColumn aCol2 = new DataGridTextBoxColumn();
			DataGridTextBoxColumn aCol3 = new DataGridTextBoxColumn();
			DataGridTextBoxColumn aCol4 = new DataGridTextBoxColumn();
			DataGridTextBoxColumn aCol5 = new DataGridTextBoxColumn();
			DataGridTextBoxColumn aCol6 = new DataGridTextBoxColumn();
			//
			// Set column 2's caption, width and disable editing.
			//
			aCol1.MappingName = "System";
			aCol1.HeaderText = "System";
			aCol1.Width = 75;
			aCol1.Alignment = HorizontalAlignment.Left;
			aCol1.TextBox.Enabled = true;
			aCol1.NullText = "";
			//
			// Set column 3 and 4's caption, width and enable editing.
			// Since these values are optional set their Null values.
			//
			aCol2.MappingName = "Password";
			aCol2.HeaderText = "Password";
			aCol2.Width = 0;
			aCol2.Alignment = HorizontalAlignment.Left;
			aCol2.NullText = "";
			aCol2.TextBox.Enabled = true;
			aCol2.TextBox.PasswordChar = '*';
			//aCol2.
			//aCol2.Format = "yyyy-MM-dd";

			aCol3.MappingName = "Description";
			aCol3.HeaderText = "Description";
			aCol3.Width = 528;
			aCol3.Alignment = HorizontalAlignment.Left;
			aCol3.NullText = "";
			aCol3.TextBox.Enabled = true; 
			//aCol3.Format = "#0.00";

			aCol4.MappingName = "Created";
			aCol4.HeaderText = "Created";
			aCol4.Width = 50;
			aCol4.Alignment = HorizontalAlignment.Left;
			aCol4.NullText = "";
			aCol4.TextBox.Enabled = false;
			aCol4.Format = "MM/dd/yy";

			aCol5.MappingName = "Expires";
			aCol5.HeaderText = "Expires";
			aCol5.Width = 50;
			aCol5.Alignment = HorizontalAlignment.Left;
			aCol5.NullText = "";
			aCol5.TextBox.Enabled = true;
			aCol5.Format = "MM/dd/yy";

			aCol6.MappingName = "Username";
			aCol6.HeaderText = "Username";
			aCol6.Width = 0;
			aCol6.Alignment = HorizontalAlignment.Left;
			aCol6.NullText = "";
			aCol6.TextBox.Enabled = true;
			aCol6.TextBox.PasswordChar = '*';

			//
			// Add the GridColumnStyles to the DataGrid's Column Styles collection.
			// Place the "ID" column (column 1) last since it is not visible.
			//
			aGridTableStyle.GridColumnStyles.Add(aCol1);
			aGridTableStyle.GridColumnStyles.Add(aCol2);
			aGridTableStyle.GridColumnStyles.Add(aCol3);
			aGridTableStyle.GridColumnStyles.Add(aCol4);
			aGridTableStyle.GridColumnStyles.Add(aCol5);
			aGridTableStyle.GridColumnStyles.Add(aCol6);
			//
			// Add the GridColumnStyles to the aGridTableStyle.
			//
			passwords.TableStyles.Add(aGridTableStyle);

         	passwords.DataSource = passwordData.Tables["Entry"];
			passwords.Expand(-1);
			if( passwordData.Tables[0].Rows.Count > 0 ) {
				passwords.NavigateTo(0, "Entry");
			}

			updateButtons();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordSafe));
            this.passwords = new System.Windows.Forms.DataGrid();
            this.passwordView = new System.Data.DataView();
            this.add = new System.Windows.Forms.Button();
            this.logoutTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.save = new Telerik.WinControls.UI.RadButton();
            this.delete = new Telerik.WinControls.UI.RadButton();
            this.view = new Telerik.WinControls.UI.RadButton();
            this.edit = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.passwords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.passwordView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            this.SuspendLayout();
            // 
            // passwords
            // 
            this.passwords.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.passwords.CaptionText = "Password List";
            this.passwords.DataMember = "";
            this.passwords.DataSource = this.passwordView;
            this.passwords.FlatMode = true;
            this.passwords.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.passwords.Location = new System.Drawing.Point(0, 0);
            this.passwords.Name = "passwords";
            this.passwords.ReadOnly = true;
            this.passwords.Size = new System.Drawing.Size(881, 545);
            this.passwords.TabIndex = 0;
            // 
            // passwordView
            // 
            this.passwordView.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.passwordStore_ListChanged);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(74, 576);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(90, 26);
            this.add.TabIndex = 1;
            this.add.Text = "&Add New";
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // logoutTimer
            // 
            this.logoutTimer.Interval = 10000;
            this.logoutTimer.Tick += new System.EventHandler(this.logoutTimer_Tick);
            // 
            // radButton1
            // 
            this.radButton1.BackColor = System.Drawing.Color.Yellow;
            this.radButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.radButton1.ForeColor = System.Drawing.Color.Black;
            this.radButton1.Location = new System.Drawing.Point(775, 580);
            this.radButton1.Name = "radButton1";
            // 
            // 
            // 
            this.radButton1.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radButton1.Size = new System.Drawing.Size(95, 23);
            this.radButton1.TabIndex = 158;
            this.radButton1.Text = "Exit";
            this.radButton1.ThemeName = "Vista";
            this.toolTip1.SetToolTip(this.radButton1, "Exiting without \"Save\" results in data loss");
            this.radButton1.Click += new System.EventHandler(this.exit_Click);
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.Color.Lime;
            this.save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.save.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.save.ForeColor = System.Drawing.Color.Black;
            this.save.Location = new System.Drawing.Point(664, 580);
            this.save.Name = "save";
            // 
            // 
            // 
            this.save.RootElement.ForeColor = System.Drawing.Color.Black;
            this.save.Size = new System.Drawing.Size(95, 23);
            this.save.TabIndex = 159;
            this.save.Text = "Save";
            this.save.ThemeName = "Vista";
            this.toolTip1.SetToolTip(this.save, "Exiting without \"Save\" results in data loss");
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.Red;
            this.delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.delete.ForeColor = System.Drawing.Color.Black;
            this.delete.Location = new System.Drawing.Point(481, 580);
            this.delete.Name = "delete";
            // 
            // 
            // 
            this.delete.RootElement.ForeColor = System.Drawing.Color.Black;
            this.delete.Size = new System.Drawing.Size(95, 23);
            this.delete.TabIndex = 160;
            this.delete.Text = "Delete";
            this.delete.ThemeName = "Vista";
            this.toolTip1.SetToolTip(this.delete, "Exiting without \"Save\" results in data loss");
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // view
            // 
            this.view.BackColor = System.Drawing.Color.Yellow;
            this.view.Cursor = System.Windows.Forms.Cursors.Hand;
            this.view.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.view.ForeColor = System.Drawing.Color.Black;
            this.view.Location = new System.Drawing.Point(380, 580);
            this.view.Name = "view";
            // 
            // 
            // 
            this.view.RootElement.ForeColor = System.Drawing.Color.Black;
            this.view.Size = new System.Drawing.Size(95, 23);
            this.view.TabIndex = 159;
            this.view.Text = "View";
            this.view.ThemeName = "Vista";
            this.toolTip1.SetToolTip(this.view, "Exiting without \"Save\" results in data loss");
            this.view.Click += new System.EventHandler(this.view_Click);
            // 
            // edit
            // 
            this.edit.BackColor = System.Drawing.Color.Yellow;
            this.edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.edit.ForeColor = System.Drawing.Color.Black;
            this.edit.Location = new System.Drawing.Point(279, 580);
            this.edit.Name = "edit";
            // 
            // 
            // 
            this.edit.RootElement.ForeColor = System.Drawing.Color.Black;
            this.edit.Size = new System.Drawing.Size(95, 23);
            this.edit.TabIndex = 162;
            this.edit.Text = "Edit";
            this.edit.ThemeName = "Vista";
            this.toolTip1.SetToolTip(this.edit, "Exiting without \"Save\" results in data loss");
            this.edit.Click += new System.EventHandler(this.edit_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(353, 551);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(167, 17);
            this.radLabel1.TabIndex = 161;
            this.radLabel1.Text = "Selected Password Actions";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(707, 551);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(114, 17);
            this.radLabel2.TabIndex = 163;
            this.radLabel2.Text = "Completion Action";
            // 
            // PasswordSafe
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(882, 614);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.edit);
            this.Controls.Add(this.view);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.save);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.add);
            this.Controls.Add(this.passwords);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(900, 659);
            this.MinimumSize = new System.Drawing.Size(900, 659);
            this.Name = "PasswordSafe";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password Safe";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PasswordSafe_Closing);
            this.VisibleChanged += new System.EventHandler(this.PasswordSafe_VisibleChanged);
            this.MouseEnter += new System.EventHandler(this.PasswordSafe_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this.passwords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.passwordView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// Event method called whenever the list has changed in some way.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void passwordStore_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e) {
			switch( e.ListChangedType ) {
			case ListChangedType.ItemAdded:
			case ListChangedType.ItemChanged:
			case ListChangedType.ItemDeleted:
				if( !dataChanged )
					this.Text += " *";
				dataChanged = true;
				updateButtons();
				break;
			default:
				break;
			}
		}

		/// <summary>
		/// Called when the form is asked to close.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PasswordSafe_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			isClosing = true;
			if( dataChanged ) {
				saveData();
			}
		}

		/// <summary>
		/// Called when the exit button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exit_Click(object sender, System.EventArgs e) {
            try
            {
                this.Hide(); if (MyParent.WindowState.Equals(FormWindowState.Minimized)) MyParent.WindowState = FormWindowState.Normal; MyParent.Show(); MyParent.Focus();
            }
            catch (Exception ex)
            { }
        }

		/// <summary>
		/// Called when the add button is clicked. This pops up the Add Password modal dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void add_Click(object sender, System.EventArgs e) {
			AddPassword ap = new AddPassword();
			if( ap.ShowDialog(this) == DialogResult.OK ) {
				
				DataRow dr = passwordData.Tables[0].NewRow();
				dr["System"] = ap.SystemName;
				dr["Username"] = ap.Username;
				dr["Password"] = ap.Password;
				dr["Description"] = ap.Description;
				dr["Created"] = DateTime.Now;
				dr["Expires"] = ap.Expires;
				passwordData.Tables[0].Rows.Add(dr);
			}
		}

		/// <summary>
		/// Called when the edit button is clicked. This pops up the Edit Password modal dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void edit_Click(object sender, System.EventArgs e) {
			
			AddPassword ap = new AddPassword();
			ap.EditMode();

			DateTime created;
			
			try {
				created = (DateTime)passwords[passwords.CurrentRowIndex, 3];
			}
			catch {}
			try {
				ap.SystemName = (string)passwords[passwords.CurrentRowIndex, 0];
			}
			catch {}
			try {
				ap.Username = (string)passwords[passwords.CurrentRowIndex, 5];
			}
			catch {
				ap.Username = "<Not Specified>";
			}
			try {
				ap.Password = (string)passwords[passwords.CurrentRowIndex, 1];
			}
			catch {}
			try {
				ap.Description = (string)passwords[passwords.CurrentRowIndex, 2];
			}
			catch {}
			try {
				ap.Expires = (DateTime)passwords[passwords.CurrentRowIndex,4];
			}
			catch{}

			if( ap.ShowDialog(this) == DialogResult.OK ) {
				passwords[passwords.CurrentRowIndex,0] = ap.SystemName;
				passwords[passwords.CurrentRowIndex,1] = ap.Password;
				passwords[passwords.CurrentRowIndex,2] = ap.Description;
				passwords[passwords.CurrentRowIndex,4] = ap.Expires;
				passwords[passwords.CurrentRowIndex,5] = ap.Username;
				//if( !dataChanged )
				//	this.Text += " *";
				//dataChanged = true;
				//updateButtons();
			}
		}

		/// <summary>
		/// Called when the delete button is pressed to delete a password.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void delete_Click(object sender, System.EventArgs e) {
			DialogResult dr = MessageBox.Show(this, "Are you sure you want to delete the selected entry?", 
				"Delete Confirmation", MessageBoxButtons.YesNo);
			if( dr == DialogResult.Yes ) 
				passwordData.Tables[0].DefaultView.Delete(passwords.CurrentRowIndex);
		}

		/// <summary>
		/// Updates the button enabled stated depending on the current state of the password list.
		/// </summary>
		private void updateButtons() {
			if( passwordView.Count == 0 ) {
				delete.Enabled = false;
				edit.Enabled = false;
				view.Enabled = false;
			}
			else {
				delete.Enabled = true;
				edit.Enabled = true;
				view.Enabled = true;
			}

			if( dataChanged )
				save.Enabled = true;

			logoutTime = DateTime.Now.AddMinutes(15);
		}

		/// <summary>
		/// Called when the View button is clicked to show the current row's password.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void view_Click(object sender, System.EventArgs e) {
	
			if( passwordView.Count > 0 ) {
				viewSelectedPassword();				
			}
		}

		/// <summary>
		/// Shows a message box with the current password.
		/// </summary>
		private void viewSelectedPassword() {
			string pw = (string)passwords[passwords.CurrentRowIndex,1];
			string un = string.Empty;
			try {
				un = (string)passwords[passwords.CurrentRowIndex,5];
			}
			catch {
				un = "<Not Specified>";
			}
			MessageBox.Show(this, "User name: "+un+"\r\nPassword: "+pw, "View Password", MessageBoxButtons.OK);
			logoutTime = DateTime.Now.AddMinutes(15);
		}

		/// <summary>
		/// Called when the save button is clicked to manually save the password store file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void save_Click(object sender, System.EventArgs e) {
			if( saveData() )
				MessageBox.Show(this, "Data saved successfully.", "Save Confirmation", MessageBoxButtons.OK);
			logoutTime = DateTime.Now.AddMinutes(15);
		}

		/// <summary>
		/// Saves the password data in memory to the password store. This method encrypts the data in memory and
		/// then writes out to the file in encrypted format.
		/// </summary>
		/// <returns></returns>
		private bool saveData() {

			if( isClosing ) {
				DialogResult dr = MessageBox.Show(this,
					"There have been changes to your password file. Would you like to save before exiting?",
					"Save Confirmation", MessageBoxButtons.YesNo);
				if( dr == DialogResult.No )
					return false;
			}

			MemoryStream ms = new MemoryStream();
			passwordData.WriteXml(ms);
			string data = System.Text.Encoding.UTF8.GetString(ms.GetBuffer());

			data = Crypt.Encrypt(data, encryptKey);

			try {
				if( File.Exists(passwordStoreName) ) 
					File.SetAttributes(passwordStoreName, FileAttributes.Normal);

				StreamWriter sw = new StreamWriter(passwordStoreName);
				sw.Write(data);
				sw.Close();

				File.SetAttributes(passwordStoreName, FileAttributes.Hidden|FileAttributes.ReadOnly);
			}
			catch {
				MessageBox.Show(this, "There is a problem writing the password store file on your U: drive. Make sure you have mapped "+
					"a U: drive to your home directory and try again. If the problem persists, please contact support.",
					"Password Safe Error", MessageBoxButtons.OK);
				return false;
			}

			save.Enabled = false;
			this.Text = "Password Safe";
			dataChanged = false;

			return true;
		}

		/// <summary>
		/// Called every second and is used to logout the user if they've not interacted
		/// with the application in 15 minutes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void logoutTimer_Tick(object sender, System.EventArgs e) {
			if( logoutTime < DateTime.Now ) {
				if( dataChanged )
					saveData();
				Application.Exit();
			}
		}

        private void PasswordSafe_MouseEnter(object sender, EventArgs e)
        {

        }

        private void PasswordSafe_VisibleChanged(object sender, EventArgs e)
        {
        }

 	}
}

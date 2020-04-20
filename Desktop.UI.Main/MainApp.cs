#region Eugene Larkin Notice

// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the owner.

// $Workfile: Main.cs $
//
// AUTHOR:
//
// DESCRIPTION:
// 
//
#endregion

using System;
using System.Collections;
using System.Configuration;
using System.Drawing;
// make it possible to use word and outlook
using System.Windows.Forms;
using Framework.DomainLayer.Config;
using Framework.DomainLayer.Security;
using Framework.DomainLayer.User;
using Framework.EntityObject;
using Framework.Logging;
using Security.ApplicationSecurity;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Text;

namespace Desktop.UI
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class MainApp : System.Windows.Forms.Form
    {
        # region designercode
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainApp));
            this.logTab = new Telerik.WinControls.UI.TabItem();
            this.tabItem1 = new Telerik.WinControls.UI.TabItem();
            this.tabItem2 = new Telerik.WinControls.UI.TabItem();
            this.tabItem3 = new Telerik.WinControls.UI.TabItem();
            this.tabItem4 = new Telerik.WinControls.UI.TabItem();
            this.d = new Telerik.WinControls.UI.TabItem();
            this.tabItem6 = new Telerik.WinControls.UI.TabItem();
            this.tabItem7 = new Telerik.WinControls.UI.TabItem();
            this.tabItem8 = new Telerik.WinControls.UI.TabItem();
            this.tabItem9 = new Telerik.WinControls.UI.TabItem();
            this.tabItem10 = new Telerik.WinControls.UI.TabItem();
            this.tabItem11 = new Telerik.WinControls.UI.TabItem();
            this.LogOffButton = new Telerik.WinControls.UI.RadButton();
            this.ExitButton = new Telerik.WinControls.UI.RadButton();
            this.radOffice2007ScreenTip1 = new Telerik.WinControls.UI.RadOffice2007ScreenTip();
            this.radComboBoxItem1 = new Telerik.WinControls.UI.RadComboBoxItem();
            this.radComboBoxItem2 = new Telerik.WinControls.UI.RadComboBoxItem();
            this.TopXToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PasswordsafeButton = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.LogOffButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radOffice2007ScreenTip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordsafeButton)).BeginInit();
            this.SuspendLayout();
            // 
            // logTab
            // 
            this.logTab.CanFocus = true;
            this.logTab.Class = "TabItem";
            // 
            // logTab.ContentPanel
            // 
            this.logTab.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.logTab.ContentPanel.CausesValidation = true;
            this.logTab.Name = "logTab";
            // 
            // tabItem1
            // 
            this.tabItem1.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.tabItem1.CanFocus = true;
            this.tabItem1.Class = "TabItem";
            // 
            // tabItem1.ContentPanel
            // 
            this.tabItem1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.tabItem1.ContentPanel.CausesValidation = true;
            this.tabItem1.IsSelected = true;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.StretchHorizontally = false;
            this.tabItem1.StretchVertically = false;
            this.tabItem1.Text = "Search";
            // 
            // tabItem2
            // 
            this.tabItem2.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.tabItem2.CanFocus = true;
            this.tabItem2.Class = "TabItem";
            // 
            // tabItem2.ContentPanel
            // 
            this.tabItem2.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.tabItem2.ContentPanel.CausesValidation = true;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.StretchHorizontally = false;
            this.tabItem2.StretchVertically = false;
            this.tabItem2.Text = "Search";
            // 
            // tabItem3
            // 
            this.tabItem3.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.tabItem3.CanFocus = true;
            this.tabItem3.Class = "TabItem";
            // 
            // tabItem3.ContentPanel
            // 
            this.tabItem3.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.tabItem3.ContentPanel.CausesValidation = true;
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.StretchHorizontally = false;
            this.tabItem3.StretchVertically = false;
            this.tabItem3.Text = "Advanced";
            // 
            // tabItem4
            // 
            this.tabItem4.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.tabItem4.CanFocus = true;
            this.tabItem4.Class = "TabItem";
            // 
            // tabItem4.ContentPanel
            // 
            this.tabItem4.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.tabItem4.ContentPanel.CausesValidation = true;
            this.tabItem4.Name = "tabItem4";
            this.tabItem4.StretchHorizontally = false;
            this.tabItem4.StretchVertically = false;
            this.tabItem4.Text = "Attachment";
            // 
            // d
            // 
            this.d.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.d.CanFocus = true;
            this.d.Class = "TabItem";
            // 
            // d.ContentPanel
            // 
            this.d.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.d.ContentPanel.CausesValidation = true;
            this.d.Name = "d";
            this.d.StretchHorizontally = false;
            this.d.StretchVertically = false;
            this.d.Text = "Edit";
            // 
            // tabItem6
            // 
            this.tabItem6.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.tabItem6.CanFocus = true;
            this.tabItem6.Class = "TabItem";
            // 
            // tabItem6.ContentPanel
            // 
            this.tabItem6.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.tabItem6.ContentPanel.CausesValidation = true;
            this.tabItem6.Name = "tabItem6";
            this.tabItem6.StretchHorizontally = false;
            this.tabItem6.StretchVertically = false;
            this.tabItem6.Text = "Edit";
            // 
            // tabItem7
            // 
            this.tabItem7.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.tabItem7.CanFocus = true;
            this.tabItem7.Class = "TabItem";
            // 
            // tabItem7.ContentPanel
            // 
            this.tabItem7.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.tabItem7.ContentPanel.CausesValidation = true;
            this.tabItem7.Name = "tabItem7";
            this.tabItem7.StretchHorizontally = false;
            this.tabItem7.StretchVertically = false;
            this.tabItem7.Text = "Edit";
            // 
            // tabItem8
            // 
            this.tabItem8.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.tabItem8.CanFocus = true;
            this.tabItem8.Class = "TabItem";
            // 
            // tabItem8.ContentPanel
            // 
            this.tabItem8.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.tabItem8.ContentPanel.CausesValidation = true;
            this.tabItem8.Name = "tabItem8";
            this.tabItem8.StretchHorizontally = false;
            this.tabItem8.StretchVertically = false;
            this.tabItem8.Text = "Security";
            // 
            // tabItem9
            // 
            this.tabItem9.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.tabItem9.CanFocus = true;
            this.tabItem9.Class = "TabItem";
            // 
            // tabItem9.ContentPanel
            // 
            this.tabItem9.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.tabItem9.ContentPanel.CausesValidation = true;
            this.tabItem9.Name = "tabItem9";
            this.tabItem9.StretchHorizontally = false;
            this.tabItem9.StretchVertically = false;
            this.tabItem9.Text = "Account";
            // 
            // tabItem10
            // 
            this.tabItem10.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.tabItem10.CanFocus = true;
            this.tabItem10.Class = "TabItem";
            // 
            // tabItem10.ContentPanel
            // 
            this.tabItem10.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.tabItem10.ContentPanel.CausesValidation = true;
            this.tabItem10.Name = "tabItem10";
            this.tabItem10.StretchHorizontally = false;
            this.tabItem10.StretchVertically = false;
            this.tabItem10.Text = "Neighbor";
            // 
            // tabItem11
            // 
            this.tabItem11.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.tabItem11.CanFocus = true;
            this.tabItem11.Class = "TabItem";
            // 
            // tabItem11.ContentPanel
            // 
            this.tabItem11.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.tabItem11.ContentPanel.CausesValidation = true;
            this.tabItem11.Name = "tabItem11";
            this.tabItem11.StretchHorizontally = false;
            this.tabItem11.StretchVertically = false;
            this.tabItem11.Text = "Mail";
            // 
            // LogOffButton
            // 
            this.LogOffButton.Location = new System.Drawing.Point(0, 0);
            this.LogOffButton.Name = "LogOffButton";
            this.LogOffButton.Size = new System.Drawing.Size(0, 0);
            this.LogOffButton.TabIndex = 0;
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(0, 0);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(0, 0);
            this.ExitButton.TabIndex = 0;
            // 
            // radOffice2007ScreenTip1
            // 
            this.radOffice2007ScreenTip1.CaptionVisible = true;
            this.radOffice2007ScreenTip1.Description = "Override this property and provide custom screentip template description in Desig" +
                "nTime.";
            this.radOffice2007ScreenTip1.FooterVisible = false;
            this.radOffice2007ScreenTip1.Location = new System.Drawing.Point(0, 0);
            this.radOffice2007ScreenTip1.Name = "radOffice2007ScreenTip1";
            // 
            // 
            // 
            this.radOffice2007ScreenTip1.ScreenTipElement.Description = "Override this property and provide custom screentip template description in Desig" +
                "nTime.";
            this.radOffice2007ScreenTip1.ScreenTipElement.TemplateType = typeof(Telerik.WinControls.UI.RadOffice2007ScreenTipElement);
            this.radOffice2007ScreenTip1.Size = new System.Drawing.Size(173, 53);
            this.radOffice2007ScreenTip1.TabIndex = 0;
            this.radOffice2007ScreenTip1.TemplateType = typeof(Telerik.WinControls.UI.RadOffice2007ScreenTipElement);
            // 
            // radComboBoxItem1
            // 
            this.radComboBoxItem1.Name = "radComboBoxItem1";
            // 
            // radComboBoxItem2
            // 
            this.radComboBoxItem2.Name = "radComboBoxItem2";
            // 
            // TopXToolTip
            // 
            this.TopXToolTip.AutomaticDelay = 200;
            this.TopXToolTip.IsBalloon = true;
            this.TopXToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // PasswordsafeButton
            // 
            this.PasswordsafeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordsafeButton.BackColor = System.Drawing.Color.LightGreen;
            this.PasswordsafeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PasswordsafeButton.Location = new System.Drawing.Point(123, 50);
            this.PasswordsafeButton.Name = "PasswordsafeButton";
            // 
            // 
            // 
            this.PasswordsafeButton.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.Auto;
            this.PasswordsafeButton.Size = new System.Drawing.Size(133, 23);
            this.PasswordsafeButton.TabIndex = 83;
            this.PasswordsafeButton.Text = "Password Safe";
            this.PasswordsafeButton.ThemeName = "OfficeGlass";
            this.PasswordsafeButton.Click += new System.EventHandler(this.PasswordsafeButton_Click);
            // 
            // MainApp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(1, 1);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(430, 178);
            this.Controls.Add(this.PasswordsafeButton);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(3, 3);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(448, 223);
            this.Name = "MainApp";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogOffButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radOffice2007ScreenTip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordsafeButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        # endregion designercode

        # region class control definitions
        private UserDataEntity[] alUserMatchesInGroup;
        private UsersInGroup uig = null;
        private LoginToken loginAuthenticationToken;
        private UserDataEntity[] workingUserFound;
        private Users wau = null;
        private LoginControl lc = new LoginControl();
        private static Splash sp = new Splash();
        private PasswordSafe PasswordSafeWindow = new PasswordSafe();
        private bool PasswordSafeFirstRun = true;

        private bool showAllErrors = false;
        private static bool showPleaseWait = false;
        private static LoginToken staticLoginAuth;
        private static Hashtable staticConfigHT;
        private static Form Myself = null;
        private Color TabBackGroundDefault = System.Drawing.Color.Transparent;
        private Color TabForeGroundDefault = System.Drawing.Color.Black;
        private Color HighlightTabBackGroundDefault = System.Drawing.Color.Transparent;
        private Color HighlightTabForeGroundDefault = System.Drawing.Color.Black;
        private Color SelectionOptionListBackGroundDefault = System.Drawing.Color.Transparent;
        private Color SelectedListBackGroundDefault = System.Drawing.Color.Transparent;
        private Color WindowBackGroundDefault = System.Drawing.Color.Transparent;

        private static DateTime SessionTimer = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["SessionTimeOut"].ToString()));

        # endregion class control definitions

        # region privatedeclarations
        //private RadColorDialog radColorDialog1;
        private Hashtable configHT = new Hashtable(); 
        private RadOffice2007ScreenTip radOffice2007ScreenTip1;
        private ToolTip TopXToolTip;
        private TabItem tabItem1;
        private TabItem tabItem2;
        private TabItem tabItem3;
        private TabItem tabItem4;
        private TabItem tabItem6;
        private TabItem d;
        private TabItem tabItem7;
        private TabItem tabItem8;
        private TabItem tabItem9;
        private TabItem tabItem10;
        private TabItem tabItem11;
        private RadComboBoxItem radComboBoxItem1;
        private RadComboBoxItem radComboBoxItem2;
        private TabItem logTab;
        private System.ComponentModel.IContainer components = null;
        private RadButton LogOffButton;
        private RadButton ExitButton;
        private string title = String.Empty;
        private string _data = String.Empty;
        private RadLabel radLabel14;
        private RadButton PasswordsafeButton;
        private string _subject = String.Empty;
        # endregion privatedeclarations

 
        [STAThread]
        static void Main(string[] args)
        {
            try
            {

                try
                {
                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["ShowPleaseWait"].ToString()))
                        sp.Show();
                }
                catch
                {
                }

                if (args.Length > 0)
                {
                    // Convert the input arguments 
                    try
                    {
                        System.Windows.Forms.Application.Run(new MainApp(args[0], args[1]));
                    }
                    catch (System.FormatException)
                    {
                        MessageBox.Show("Paramaters passed do not match those required.");
                        Framework.Logging.Log.Write(LogLevel.EXCEP, "ISODesktop", "Main", "Log", "System", "Main (Main Method) - Paramaters passed do not match those required.");
                        return;
                    }
                }
                else
                {
                    System.Windows.Forms.Application.Run(new MainApp("", ""));
                }

            }
            catch (System.Exception e)
            {
                if (e.Message.ToString().Contains("Object reference not set to an instance of an object"))
                { }
                else
                    Framework.Logging.Log.Write(LogLevel.EXCEP, "ISODesktop", "Main", "Log", "System", "Main (Main Method) - " + e.Message.ToString());
            }
        }


        // MYCODEMYCODEMYCODEMYCODE
        
        /* ************************************************************************************************************************************************** */
        /* @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ */
        /* &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& */
        /* ************************************************************************************************************************************************** */
        private MainApp()
        {

        }

        public MainApp(string verifyUser, string verifyPW)
        {
            //
            // Required for Windows Form Designer support
            //
            try
            {

                InitializeComponent();
                Myself = this;
                SetupColorScheme();

                try
                {
                    if (PasswordSafeFirstRun)
                    {
                        PasswordSafeWindow = new PasswordSafe("Administrator", "DUMMY");
                        PasswordSafeWindow.Show();
                        PasswordSafeFirstRun = false;
                    }
                    else
                    {
                        PasswordSafeWindow.Show();
                    }

                    this.WindowState = FormWindowState.Minimized;
                    PasswordSafeWindow.Focus();
                }
                catch (Exception ex)
                {
                    try
                    {
                        PasswordSafeWindow = new PasswordSafe("Administrator", "DUMMY");
                        PasswordSafeWindow.Show();
                        PasswordSafeFirstRun = false;
                    }
                    catch
                    {
                        MessageBox.Show("Sorry but the application could not be loaded.  Consult the logs and contact support. Error : " + ex.Message.ToString());
                    }
                }

            }
            catch
            {
            }
        }

        public static bool SessionActive()
        {
            try
            {
                if (SessionTimer < DateTime.Now)
                {
                    // redirect the user back to the login page
                    return false;
                }
                else
                {
                    SessionTimer = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["SessionTimeOut"].ToString()));
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static Form ReturnReferenceToMyself()
        {
            return Myself;
        }

        public static LoginToken GetLoginAuthData()
        {
            return staticLoginAuth;
        }

        public static Hashtable GetConfigurationData()
        {
            return staticConfigHT;
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

        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */


        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */


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

         private void Main_Load(object sender, System.EventArgs e)
        {
            try
            {

            }
            catch
            { }

        }
        


         private void PasswordsafeButton_Click(object sender, EventArgs e)
         {
             try
             {
                 if (PasswordSafeFirstRun)
                 {
                     PasswordSafeWindow = new PasswordSafe("Administrator", "DUMMY");
                     PasswordSafeWindow.Show();
                     PasswordSafeFirstRun = false;
                 }
                 else
                 {
                     PasswordSafeWindow.Show();
                 }

                 this.WindowState = FormWindowState.Minimized;
                 PasswordSafeWindow.Focus();
             }
             catch (Exception ex)
             {
                 try
                 {
                     PasswordSafeWindow = new PasswordSafe("Administrator", "DUMMY");
                     PasswordSafeWindow.Show();
                     PasswordSafeFirstRun = false;
                 }
                 catch
                 {
                     MessageBox.Show("Sorry but the application could not be loaded.  Consult the logs and contact support. Error : " + ex.Message.ToString());
                 }
             }
         }

        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
        /* ************************************************************************************************************************************************** */
    }
}

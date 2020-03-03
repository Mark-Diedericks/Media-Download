using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Media_Download
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private enum SettingsPage
        {
            DEFAULT = 0,
            THEME = 1,
            ABOUT = 2
        }

        #region Window

        private AppTheme lightTheme;
        private AppTheme darkTheme;
        private Accent currentAccent;
        private List<string> accents;

        public MainWindow()
        {
            accents = new List<string>();
            accents.Add("Red");
            accents.Add("Green");
            accents.Add("Blue");
            accents.Add("Purple");
            accents.Add("Orange");
            accents.Add("Lime");
            accents.Add("Emerald");
            accents.Add("Teal");
            accents.Add("Cyan");
            accents.Add("Cobalt");
            accents.Add("Indigo");
            accents.Add("Violet");
            accents.Add("Pink");
            accents.Add("Magenta");
            accents.Add("Crimson");
            accents.Add("Amber");
            accents.Add("Yellow");
            accents.Add("Brown");
            accents.Add("Olive");
            accents.Add("Steel");
            accents.Add("Mauve");
            accents.Add("Taupe");
            accents.Add("Sienna");

            if (Properties.Settings.Default.DefaultSaveLocation.Count() <= 0)
            {
                String music = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                Properties.Settings.Default.DefaultSaveLocation = music;
                Properties.Settings.Default.Save();
            }

            InitializeComponent();

            string a = Properties.Settings.Default.ThemeAccent;

            SettingsThemeAccentList.ItemsSource = accents;
            SettingsThemeAccentList.SelectedValue = a;

            lightTheme = ThemeManager.GetAppTheme("BaseLight");
            darkTheme = ThemeManager.GetAppTheme("BaseDark");
            currentAccent = ThemeManager.GetAccent(a);

            UpdateTheme();

            SetActiveSettingsPage(SettingsPage.DEFAULT);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.ThemeAccent = currentAccent.Name;
            Properties.Settings.Default.Save();
        }
        private void MetroWindow_Paste(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focEl = Keyboard.FocusedElement;
            if (focEl is TextBox)
            {
                (focEl as TextBox).Paste();
            }
            else
            {
                txtLink.Clear();
                txtLink.Paste();
            }
        }

        #endregion

        #region MainPage

        private void btnLinkPaste_Click(object sender, RoutedEventArgs e)
        {
            txtLink.Clear();
            txtLink.Paste();
        }

        private void btnFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog cofd = new CommonOpenFileDialog();
            cofd.IsFolderPicker = true;
            cofd.Multiselect = false;
            cofd.EnsurePathExists = true;

            if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                lblFolder.Text = cofd.FileName;
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/rg3/youtube-dl/blob/master/README.md");
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.btnOK.IsEnabled = false;

            if (Directory.Exists(lblFolder.Text))
            {
                Uri outUri;
                if (Uri.TryCreate(txtLink.Text, UriKind.Absolute, out outUri))
                {
                    string mod = "";

                    if (rdbtnAudio.IsChecked == true)
                        mod = "--extract-audio --audio-format mp3";
                    else if (rdbtnVideo.IsChecked == true)
                        mod = "-f \"bestvideo[ext=mp4]+bestaudio[ext=m4a]/mp4\"";
                    else
                        mod = txtFormat.Text.Trim();

                    string savedir = lblFolder.Text + "\\";
                    string curdir = Environment.CurrentDirectory + "\\Dependencies\\youtube-dl.exe";
                    string batfile = CreateBatFile(savedir, curdir, mod, txtLink.Text);

                    if (!File.Exists(batfile))
                    {
                        MessageBox.Show("Could not create the download bat, try running the program as an administrator.", "Error");
                        this.btnOK.IsEnabled = true;
                        return;
                    }

                    ProcessStartInfo cmdsi = new ProcessStartInfo
                    {
                        WorkingDirectory = lblFolder.Text,
                        FileName = batfile
                    };

                    Process cmdDownload = Process.Start(cmdsi);
                    cmdDownload.WaitForExit();

                    try
                    {
                        File.Delete(batfile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not delete the download bat file.", "Error");
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid URL/Link.");
                }
            }
            else
            {
                MessageBox.Show("Please select a folder to which the files will be saved.");
            }

            this.btnOK.IsEnabled = true;
        }

        #endregion

        #region Helpers

        private void UpdateTheme()
        {
            if (currentAccent == null || lightTheme == null || darkTheme == null)
                return;

            if(Properties.Settings.Default.ThemeLight)
                ThemeManager.ChangeAppStyle(this, currentAccent, lightTheme);
            else
                ThemeManager.ChangeAppStyle(this, currentAccent, darkTheme);
        }

        private void UpdateYoutubeDL(Action OnCompletedAction)
        {
            string curdir = Environment.CurrentDirectory + "\\Dependencies\\youtube-dl.exe";
            string wd = lblFolder.Text;

            Thread ExecutionThread = new Thread((ThreadStart)delegate
            {
                ProcessStartInfo cmdsi = new ProcessStartInfo
                {
                    WorkingDirectory = wd,
                    FileName = "cmd.exe",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = "/C \"" + curdir + "\" --update && exit",
                    Verb = "runas",
                };

                Process cmdUpdate = Process.Start(cmdsi);
                cmdUpdate.WaitForExit();

                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => OnCompletedAction?.Invoke()));
            });

            ExecutionThread.Start();
        }

        private void UpdateFinished()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            { 
                SettingsIconLoad.Visibility = Visibility.Hidden;
                SettingsIconTick.Visibility = Visibility.Visible;
                btnUpdate.Content = "Updated";
                btnUpdate.IsEnabled = false;
            }));
        }

        private string CreateBatFile(string saveDir, string appDir, string mod, string link)
        {
            StringBuilder batText = new StringBuilder();
            batText.AppendLine("cd " + "\"" + saveDir + "\"");
            batText.AppendLine(":retry");
            batText.AppendLine("\"" + appDir + "\"" + " -i --download-archive downloaded.txt --socket-timeout 1 --retries infinite " + mod + " -o \"%%(title)s.%%(ext)s\" " + link + " -c");
            batText.AppendLine("IF NOT \"%ERRORLEVEL%\"==\"0\" GOTO :retry");
            batText.AppendLine("@echo COMPLETE, ERRORLEVEL = %ERRORLEVEL%");
            batText.AppendLine("exit");

            string batdir = saveDir + "\\download.bat";
            StreamWriter sw = new StreamWriter(batdir, false);
            sw.Write(batText.ToString());
            sw.Flush();
            sw.Close();

            return batdir;
        }

        private void SetActiveSettingsPage(SettingsPage page)
        {
            switch(page)
            {
                case SettingsPage.DEFAULT:
                    SettingsPageDefault.Visibility = Visibility.Visible;
                    SettingsPageTheme.Visibility = Visibility.Hidden;
                    SettingsPageAbout.Visibility = Visibility.Hidden;

                    btnSettingsDefault.IsChecked = true;
                    btnSettingsTheme.IsChecked = false;
                    btnSettingsAbout.IsChecked = false;
                    break;
                case SettingsPage.THEME:
                    SettingsPageDefault.Visibility = Visibility.Hidden;
                    SettingsPageTheme.Visibility = Visibility.Visible;
                    SettingsPageAbout.Visibility = Visibility.Hidden;

                    btnSettingsDefault.IsChecked = false;
                    btnSettingsTheme.IsChecked = true;
                    btnSettingsAbout.IsChecked = false;
                    break;
                case SettingsPage.ABOUT:
                    SettingsPageDefault.Visibility = Visibility.Hidden;
                    SettingsPageTheme.Visibility = Visibility.Hidden;
                    SettingsPageAbout.Visibility = Visibility.Visible;

                    btnSettingsDefault.IsChecked = false;
                    btnSettingsTheme.IsChecked = false;
                    btnSettingsAbout.IsChecked = true;
                    break;
            }
        }

        #endregion

        #region SettingsMenu

        private void btnSettingsDefault_Click(object sender, RoutedEventArgs e)
        {
            SetActiveSettingsPage(SettingsPage.DEFAULT);
        }

        private void btnSettingsTheme_Click(object sender, RoutedEventArgs e)
        {
            SetActiveSettingsPage(SettingsPage.THEME);
        }

        private void btnSettingsAbout_Click(object sender, RoutedEventArgs e)
        {
            SetActiveSettingsPage(SettingsPage.ABOUT);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            flyoutSettings.IsOpen = true;
        }

        #region DEFAULTS
        
        private void btnFolderDefault_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog cofd = new CommonOpenFileDialog();
            cofd.IsFolderPicker = true;
            cofd.Multiselect = false;
            cofd.EnsurePathExists = true;

            if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Properties.Settings.Default.DefaultSaveLocation = cofd.FileName;
                Properties.Settings.Default.Save();
            }
        }

        #endregion

        #region THEMES
        
        private void rdbtnThemeLight_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ThemeLight = true;
            Properties.Settings.Default.ThemeDark = false;
            Properties.Settings.Default.Save();

            if(currentAccent != null && lightTheme != null)
                ThemeManager.ChangeAppStyle(this, currentAccent, lightTheme);
        }

        private void rdbtnThemeDark_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ThemeLight = false;
            Properties.Settings.Default.ThemeDark = true;
            Properties.Settings.Default.Save();

            if (currentAccent != null && lightTheme != null)
                ThemeManager.ChangeAppStyle(this, currentAccent, darkTheme);
        }

        private void SettingsThemeAccentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentAccent == null)
                return;

            Properties.Settings.Default.ThemeAccent = (string)SettingsThemeAccentList.SelectedValue;

            if (Properties.Settings.Default.ThemeAccent == null)
                Properties.Settings.Default.ThemeAccent = "Cobalt";

            Properties.Settings.Default.Save();

            Accent temp = ThemeManager.GetAccent(Properties.Settings.Default.ThemeAccent);

            if (temp != null)
                currentAccent = temp;

            UpdateTheme();
        }

        #endregion

        #region ABOUT

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdate.IsEnabled = false;

            SettingsIconLoad.Visibility = Visibility.Visible;
            SettingsIconTick.Visibility = Visibility.Hidden;
            btnUpdate.Content = "Checking For Updates";

            Dispatcher.InvokeAsync(() => UpdateYoutubeDL(UpdateFinished));
        }

        #endregion

        #endregion
    }
}

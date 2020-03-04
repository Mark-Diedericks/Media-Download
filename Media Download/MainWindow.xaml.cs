using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Media_Download
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly string YOUTUBE_DL_LATEST = "http://rg3.github.io/youtube-dl/update/LATEST_VERSION";
        private readonly string YOUTUBE_DL_README = "https://github.com/rg3/youtube-dl/blob/master/README.md";

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

        private HashSet<Uri> downloads;
        private HashSet<Uri> failures;
        private int begun_count;
        private int ended_count;

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
                string music = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
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

            downloads = new HashSet<Uri>();
            failures = new HashSet<Uri>();
            begun_count = 0;
            ended_count = 0;

            Dispatcher.InvokeAsync(() => VersionYoutubeDL(VersionFinished));
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
            CommonOpenFileDialog cofd = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Multiselect = false,
                EnsurePathExists = true
            };

            if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                lblFolder.Text = cofd.FileName;
            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(YOUTUBE_DL_README);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            string mod = txtFormat.Text.Trim();

            if (rdbtnAudio.IsChecked == true)
                mod = "--extract-audio --audio-format mp3";
            else if (rdbtnVideo.IsChecked == true)
                mod = "-f \"bestvideo[ext=mp4]+bestaudio[ext=m4a]/mp4\"";

            Dispatcher.InvokeAsync(() => DownloadMediaFile(lblFolder.Text, txtLink.Text, mod, DownloadFinished));
        }

        private void txtRemain_Click(object sender, MouseButtonEventArgs e)
        {
            downloads.Clear();
            failures.Clear();
            begun_count = 0;
            ended_count = 0;

            txtRemain.Text = "0 of 0";
            txtRemain.ToolTip = "";
        }

        private void txtUpdates_Click(object sender, MouseButtonEventArgs e)
        {
            flyoutSettings.IsOpen = true;
            SetActiveSettingsPage(SettingsPage.ABOUT);
        }

        #endregion

        #region Helpers

        private void DownloadMediaFile(string folder, string link, string mod, Action<Uri, int> OnCompletedAction)
        {
            Thread ExecutionThread = new Thread((ThreadStart)delegate
            {
                Uri outUri = null;
                int exitCode = 0;

                if (Directory.Exists(folder))
                {
                    if (Uri.TryCreate(link, UriKind.Absolute, out outUri))
                    {
                        downloads.Add(outUri);
                        begun_count++;

                        //Update status textblock
                        string remaining_content = string.Format("{0} of {1}", ended_count, begun_count);
                        if (failures.Count > 0)
                            remaining_content += string.Format(" - {0} Failed", failures.Count);

                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { txtRemain.Text = remaining_content; txtRemain.Visibility = Visibility.Visible; }));

                        string savedir = folder + "\\";
                        string curdir = Environment.CurrentDirectory + "\\Dependencies\\youtube-dl.exe";
                        string batfile = CreateBatFile(savedir, curdir, mod, link);

                        if (!File.Exists(batfile))
                        {
                            MessageBox.Show("Could not create the download bat, try running the program as an administrator.", "Error");
                            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => OnCompletedAction?.Invoke(outUri, 1)));
                            return;
                        }

                        ProcessStartInfo cmdsi = new ProcessStartInfo
                        {
                            WorkingDirectory = folder,
                            FileName = batfile
                        };

                        if (!Properties.Settings.Default.ShowConsole)
                        {
                            cmdsi.CreateNoWindow = true;
                            cmdsi.WindowStyle = ProcessWindowStyle.Hidden;
                        }

                        Process cmdDownload = Process.Start(cmdsi);
                        cmdDownload.WaitForExit();
                        exitCode = cmdDownload.ExitCode;

                        try
                        {
                            File.Delete(batfile);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not delete the download bat file.", "Error");
                            Debug.WriteLine(ex.Message);
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

                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => OnCompletedAction?.Invoke(outUri, exitCode)));
            });

            ExecutionThread.Start();
        }
        private void DownloadFinished(Uri address, int exitCode)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                if (address is null)
                    return;

                downloads.Remove(address);
                ended_count++;

                //Validate download
                if (exitCode != 0)
                    failures.Add(address);

                //Update status textblock
                string remaining_content = string.Format("{0} of {1}", ended_count, begun_count);
                if (failures.Count > 0)
                    remaining_content += string.Format(" - {0} Failed", failures.Count);

                txtRemain.Text = remaining_content;
                txtRemain.Visibility = Visibility.Visible;
                txtRemain.ToolTip = string.Join("\n", failures);
            }));
        }

        private void UpdateYoutubeDL(Action<int> OnCompletedAction)
        {
            string location = Environment.CurrentDirectory + "\\Dependencies\\youtube-dl.exe";

            Thread ExecutionThread = new Thread((ThreadStart)delegate
            {
                ProcessStartInfo cmdsi = new ProcessStartInfo
                {
                    FileName = location,
                    Arguments = "--update",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process cmdUpdate = Process.Start(cmdsi);
                cmdUpdate.WaitForExit();

                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => OnCompletedAction?.Invoke(cmdUpdate.ExitCode)));
            });

            ExecutionThread.Start();
        }

        private void UpdateFinished(int exitCode)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                SettingsIconLoad.Visibility = Visibility.Hidden;

                if (exitCode > 0)
                {
                    btnUpdate.IsEnabled = true;
                    btnUpdate.Content = "Retry Update";
                    SettingsIconCross.Visibility = Visibility.Visible;
                }
                else
                {
                    btnUpdate.IsEnabled = false;
                    btnUpdate.Content = "Updated";
                    SettingsIconTick.Visibility = Visibility.Visible;
                    txtUpdates.Visibility = Visibility.Hidden;
                }
            }));
        }

        private void VersionYoutubeDL(Action<int, string, string> OnCompletedAction)
        {
            string location = Environment.CurrentDirectory + "\\Dependencies\\youtube-dl.exe";

            Thread ExecutionThread = new Thread((ThreadStart)delegate
            {
                ProcessStartInfo cmdsi = new ProcessStartInfo
                {
                    FileName = location,
                    Arguments = "--version",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true
                };

                Process cmdVersion = Process.Start(cmdsi);

                StringBuilder output = new StringBuilder();
                while (!cmdVersion.StandardOutput.EndOfStream)
                    output.AppendLine(cmdVersion.StandardOutput.ReadLine());

                string current_version = output.ToString().Trim();
                cmdVersion.WaitForExit();



                string latest_version = "";
                try
                {
                    latest_version = new WebClient().DownloadString(YOUTUBE_DL_LATEST);
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }

                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => OnCompletedAction?.Invoke(cmdVersion.ExitCode, current_version, latest_version)));
            });

            ExecutionThread.Start();
        }

        private void VersionFinished(int exitCode, string current_version, string latest_version)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                if (string.Equals(current_version.Trim(), latest_version.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    txtUpdates.Visibility = Visibility.Hidden;
                    SettingsIconTick.Visibility = Visibility.Visible;
                    btnUpdate.Content = "Force Update";
                }
                else
                {
                    txtUpdates.Visibility = Visibility.Visible;
                    btnUpdate.Content = "Update";
                }
            }));
        }

        private void UpdateTheme()
        {
            if (currentAccent == null || lightTheme == null || darkTheme == null)
                return;

            if (Properties.Settings.Default.ThemeLight)
                ThemeManager.ChangeAppStyle(this, currentAccent, lightTheme);
            else
                ThemeManager.ChangeAppStyle(this, currentAccent, darkTheme);
        }

        private string CreateBatFile(string saveDir, string appDir, string mod, string link)
        {
            StringBuilder batText = new StringBuilder();
            batText.AppendLine("cd " + "\"" + saveDir + "\"");
            batText.AppendLine("set /a a=0");
            batText.AppendLine(":retry");
            batText.AppendLine("set /a a=%a%+1");
            batText.AppendLine("\"" + appDir + "\"" + " -i --download-archive downloaded.txt --socket-timeout 1 --retries infinite " + mod + " -o \"%%(title)s.%%(ext)s\" " + link + " -c");
            batText.AppendLine("IF %a% GEQ 10 GOTO :complete");
            batText.AppendLine("IF %ERRORLEVEL% NEQ 0 GOTO :retry");
            batText.AppendLine(":complete");
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
            SettingsIconCross.Visibility = Visibility.Hidden;
            btnUpdate.Content = "Checking For Updates";

            Dispatcher.InvokeAsync(() => UpdateYoutubeDL(UpdateFinished));
        }

        #endregion

        #endregion

    }
}

using FortniteLauncherV2.Common;
using FortniteLauncherV2.Common.Launcher;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace FortniteLauncherV2.App.Pages
{
    /// <summary>
    /// Interaction logic for PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Wpf.Ui.Controls.UiPage
    {
        void ProcessRunning()
        {
            ButtonLaunch.Visibility = Visibility.Collapsed;
            ButtonStop.Visibility = Visibility.Visible;
        }

        void ProcessNotRunning()
        {
            ButtonLaunch.Visibility = Visibility.Visible;
            ButtonStop.Visibility = Visibility.Collapsed;
        }

        void PathVaild()
        {
            string bmp = Config.FortniteGameEnginePath + Strings.FortniteSplashImage;
            Bitmap bitmap = new Bitmap(bmp);
            FortniteSplashImage.Source = new BitmapImage(new Uri(bmp));

            InvalidPath.IsOpen = false;
            ButtonLaunch.IsEnabled = true;
        }

        void PathInvalid()
        {
            InvalidPath.IsOpen = true;
            ButtonLaunch.IsEnabled = false;

            FortniteSplashImage.Source = null;
        }
        #region Services
        private readonly ISnackbarService _snackbarService;
        SnackbarService snackbarService = new SnackbarService();

        public void ShowSnackbar(string SnackarText = null, string SnackbarContent = null, SymbolRegular SnackbarSymbol = SymbolRegular.GlanceDefault12, ControlAppearance SnackbarControlAppearance = ControlAppearance.Dark)
        {
            _snackbarService.Show(SnackarText, SnackbarContent, SnackbarSymbol, SnackbarControlAppearance);
        }
        #endregion
        public PlayPage()
        {
            InitializeComponent();

            snackbarService.SetSnackbarControl(playSnackbar);
            _snackbarService = snackbarService;

            //Config.FortniteGameEnginePath = @"D:\Games\Fortnite";
            if (!Build.IsPathValid(Config.FortniteGameEnginePath))
            {
                InvalidPath.IsOpen = true;
                ButtonLaunch.IsEnabled = false;

                //string bmp = Config.FortniteGameEnginePath + Strings.FortniteSplashImage;
                //Bitmap bitmap = new Bitmap(bmp);
                //FortniteSplashImage.Source = new BitmapImage(new Uri(bmp));
            }

            if (Build.IsPathValid(Config.FortniteGameEnginePath))
            {
                string bmp = Config.FortniteGameEnginePath + Strings.FortniteSplashImage;
                Bitmap bitmap = new Bitmap(bmp);
                FortniteSplashImage.Source = new BitmapImage(new Uri(bmp));
            }

            CraniumSSLToggle.IsChecked = Config.bUseCranium;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_tick;
            timer.Start();

            ButtonStop.Visibility = Visibility.Collapsed;
            ArgumentsBox.Text = Strings.LaunchArguments;

            SuspendBEToggle.IsChecked = Config.bSuspendBE;

            SuspendEACToggle.IsChecked = Config.bSuspendEAC;
        }

        void timer_tick(object sender, EventArgs e)
        {
            if(Build.IsPathValid(Config.FortniteGameEnginePath))
            {
                PathVaild();
            }
            if (!Build.IsPathValid(Config.FortniteGameEnginePath))
            {
                PathInvalid();
            }
            if (Globals.FortniteProcess != null)
            {
                if (ProcessHelper.IsProcessRunning(Globals.FortniteProcess.Id))
                {
                    ProcessRunning();
                }
                else if (!ProcessHelper.IsProcessRunning(Globals.FortniteProcess.Id))
                {
                    ProcessNotRunning();
                }
            }
        }

        private void CraniumSSLToggle_Checked(object sender, RoutedEventArgs e)
        {
            Config.bUseCranium = true;
        }

        private void CraniumSSLToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Config.bUseCranium = false;
        }

        private void SuspendBEToggle_Checked(object sender, RoutedEventArgs e)
        {
            Config.bSuspendBE = true;
        }

        private void SuspendBEToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Config.bSuspendBE = false;
        }

        private void SuspendEACToggle_Checked(object sender, RoutedEventArgs e)
        {
            Config.bSuspendEAC = true;
        }

        private void SuspendEACToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Config.bSuspendEAC = false;
        }

        private void ButtonLaunch_Click(object sender, RoutedEventArgs e)
        {
            if (Build.IsPathValid(Config.FortniteGameEnginePath))
            {
                if (Config.bUseCranium)
                {
                    Globals.FortniteProcess = Launcher.LaunchFortniteWithArumentsAndTryBypassSSL(Config.FortniteGameEnginePath, ArgumentsBox.Text, Strings.GetCraniumLocation(Config.bDebug));
                }
                else if (!Config.bUseCranium)
                {
                    Globals.FortniteProcess = Launcher.LaunchFortniteWithArumentsAndTryBypassSSL(Config.FortniteGameEnginePath, ArgumentsBox.Text, Strings.GetAuroraSSLLocation(Config.bDebug));
                }

                //Globals.FortniteProcess.WaitForExit();

                ProcessRunning();

                _snackbarService.Show("Fortnite", "Fortnite was launched!", SymbolRegular.ThumbLike24, ControlAppearance.Success);
            }
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            Globals.FortniteProcess.Kill();
            ProcessNotRunning();
            Globals.FortniteProcess = null; //i need to do this otherwise it will crash beacuse process is not actually running
        }
    }
}

using FortniteLauncher.Helpers;
using FortniteLauncher.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrivateServerPage : Page
    {
        void StatusInstalled()
        {
            LawinServerStatus.Title = "Installed";
            LawinServerStatus.Message = "LawinServer is installed";
            LawinServerStatus.Severity = InfoBarSeverity.Success;
            LawinServerStatus.IsOpen = true;

            LawinServerStatusBox.Text = "Installed";
            StartStopBar.Visibility = Visibility.Visible;
        }
        void StatusUninstalled()
        {
            LawinServerStatus.Title = "Not Installed";
            LawinServerStatus.Message = "LawinServer is not installed";
            LawinServerStatus.Severity = InfoBarSeverity.Error;
            LawinServerStatus.IsOpen = true;

            LawinServerStatusBox.Text = "Not Installed";
            StartStopBar.Visibility = Visibility.Collapsed;
        }
        public PrivateServerPage()
        {
            this.InitializeComponent();
            CheckStatus();
        }

        void CheckStatus()
        {
            if (LawinServerHelper.IsLawinServerInstalled())
            {
                StatusInstalled();
            }
            else
            {
                StatusUninstalled();
            }
        }

        private void InstallLawinBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LawinServerHelper.IsLawinServerInstalled())
            {
                DialogService.ShowSimpleDialog("LawinServer is already installed!", "");
                return;
            }
            try
            {
                LawinServerHelper.InstallLawinServer();

                if (LawinServerHelper.IsLawinServerInstalled())
                {
                    StatusInstalled();
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("An error occured. Error message: " + ex.Message, "Install Failed");
            }
        }

        private void UninstallLawinBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!LawinServerHelper.IsLawinServerInstalled())
            {
                DialogService.ShowSimpleDialog("LawinServer is not installed!", "");
                return;
            }
            try
            {
                LawinServerHelper.UninstallLawinServer();

                if (!LawinServerHelper.IsLawinServerInstalled())
                {
                    StatusUninstalled();
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("An error occured. Error message: " + ex.Message, "Uninnstall Failed");
            }
        }

        private void ReinstallLawinBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!LawinServerHelper.IsLawinServerInstalled())
            {
                DialogService.ShowSimpleDialog("LawinServer is not installed!", "");
                return;
            }
            try
            {
                LawinServerHelper.UninstallLawinServer();

                if (LawinServerHelper.IsLawinServerInstalled())
                {
                    StatusUninstalled();
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("An error occured. Error message: " + ex.Message, "Uninnstall Failed");
            }

            if (LawinServerHelper.IsLawinServerInstalled())
            {
                DialogService.ShowSimpleDialog("LawinServer is already installed!", "");
                return;
            }
            try
            {
                LawinServerHelper.InstallLawinServer();

                if (LawinServerHelper.IsLawinServerInstalled())
                {
                    StatusInstalled();
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("An error occured. Error message: " + ex.Message, "Install Failed");
            }
        }

        private void StartLawinBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LawinServerHelper.IsLawinServerRunning())
            {
                DialogService.ShowSimpleDialog("LawinServer is already running!", "");
                return;
            }
            try
            {
                LawinServerHelper.TryInstallNodePackagesAndStart();
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("An error occured. Error message: " + ex.Message, "Start Failed");
            }
        }

        private void StopLawinBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LawinServerHelper.Stop();
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("An error occured. Error message: " + ex.Message, "Stop Failed");
            }
        }
    }
}

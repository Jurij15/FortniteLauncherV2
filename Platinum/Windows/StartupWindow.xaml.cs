using Platinum.Helpers;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Platinum.Windows
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Wpf.Ui.Controls.Window.FluentWindow
    {
        async Task PutTaskDelayWelcomeBack()
        {
            DLLHelper.InitSavedDLLs();
            await Task.Delay(1000);
        }

        public StartupWindow()
        {
            InitializeComponent();
            Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
            Wpf.Ui.Appearance.Accent.ApplySystemAccent();
            OnStartup();
            WelcomeProcessRing.IsIndeterminate = true;
        }

        private async void OnStartup()
        {
            PreparingAppBlock.Text = "  Loading The Application";
            await PutTaskDelayWelcomeBack();
            //Settings.SettingsValues.bShouldShowWelcomeBackWindow = false;
            this.Hide();
            if (Wpf.Ui.Appearance.Theme.GetSystemTheme() == Wpf.Ui.Appearance.SystemThemeType.Dark)
            {
                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
            }
            else if (Wpf.Ui.Appearance.Theme.GetSystemTheme() == Wpf.Ui.Appearance.SystemThemeType.Light)
            {
                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
            }
            MainWindow betterMainWindow = new MainWindow();
            betterMainWindow.Show();
            betterMainWindow.ShowActivated = true;
            betterMainWindow.ShowInTaskbar = true;
        }
    }
}

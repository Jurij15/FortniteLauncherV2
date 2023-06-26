using FortniteLauncher.Dialogs;
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
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            SSLBypassDLLPathBox.Text = Config.SSLBypassDLL;
            ConsoleBypassDLLPathBox.Text = Config.ConsoleDLL;
            MemoryBypassDLLPathBox.Text = Config.MemoryLeakFixDLL;
        }

        private async void AuthCard_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Auth Settings";
            dialog.Content = new ConfigureAuthDialog(dialog);

            await dialog.ShowAsync();
        }

        private void ResetAppBtn_Click(object sender, RoutedEventArgs e)
        {
            Globals.ResetApp(true);
        }

        private void SSLBypassDLLPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.SSLBypassDLL = ((TextBox)sender).Text;
        }

        private void ConsoleBypassDLLPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MemoryBypassDLLPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

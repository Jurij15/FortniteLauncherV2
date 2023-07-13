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
using FortniteLauncher.Services;
using FortniteLauncher.Cores;
using FortniteLauncher.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Pages.SettingsPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameSettingsPage : Page
    {
        public GameSettingsPage()
        {
            this.InitializeComponent();

            SSLBypassDLLPathBox.Text = Config.SSLBypassDLL;
            ConsoleBypassDLLPathBox.Text = Config.ConsoleDLL;
            MemoryBypassDLLPathBox.Text = Config.MemoryLeakFixDLL;
        }

        private void SSLBypassDLLPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.SSLBypassDLL = ((TextBox)sender).Text;
            Settings.SaveNewSSLBypassDLLConfigConfig(((TextBox)sender).Text);
        }

        private void ConsoleBypassDLLPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.ConsoleDLL = ((TextBox)sender).Text;
            Settings.SaveNewConsoleDLLConfigConfig(((TextBox)sender).Text);
        }

        private void MemoryBypassDLLPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Config.MemoryLeakFixDLL = ((TextBox)sender).Text;
            Settings.SaveNewMemoryLeakFixDLLConfigConfig(((TextBox)sender).Text);
        }

        private void DLLLibraryCard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(DLLLibrarySettingsPage), "DLL Library", false);
        }

        private async void SelectSSLBypassDLL_Click(object sender, RoutedEventArgs e)
        {
            await DLLLibraryHelper.ShowDLLLibrary_SelectDLLDialog();

            SSLBypassDLLPathBox.Text = DLLLibraryHelper.ISelectedDLLPath;
        }

        private async void SelectConsoleDLL_Click(object sender, RoutedEventArgs e)
        {
            await DLLLibraryHelper.ShowDLLLibrary_SelectDLLDialog();

            ConsoleBypassDLLPathBox.Text = DLLLibraryHelper.ISelectedDLLPath;
        }

        private async void SelectMemoryLeakBypassDLL_Click(object sender, RoutedEventArgs e)
        {
            await DLLLibraryHelper.ShowDLLLibrary_SelectDLLDialog();

            MemoryBypassDLLPathBox.Text = DLLLibraryHelper.ISelectedDLLPath;
        }
    }
}

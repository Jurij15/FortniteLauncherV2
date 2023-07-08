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

        }

        private void ConsoleBypassDLLPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MemoryBypassDLLPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DLLLibraryCard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(DLLLibrarySettingsPage), "DLL Library", false);
        }
    }
}

using FortniteLauncher.Cores;
using FortniteLauncher.Dialogs;
using FortniteLauncher.Pages.InjectPages;
using FortniteLauncher.Pages.SettingsPages;
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
using Windows.Gaming.Input.ForceFeedback;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InjectPage : Page
    {
        public static int ProcessID { get; set; }
        public InjectPage()
        {
            this.InitializeComponent();
        }

        private async void ShowProcesses_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Select a ProcessID";
            dialog.Content = new InstanceManagerDialog(dialog);

            await dialog.ShowAsync();

            PIDBox.Value = ProcessID;
        }

        private void ConsoleInject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Injector.InjectDll(Convert.ToInt32(PIDBox.Value), Settings.ConsoleDLLConfig);
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("An error occured while injecting. Error message: " + ex.Message, "Error");
                throw;
            }
        }

        private void OpenGameSettings_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            Globals.Objects.MainNavigation.SelectedItem = Globals.Objects.MainNavigation.SettingsItem;
            NavigationService.NavigateHiearchical(typeof(SettingsPage), "Settings", true);
            NavigationService.NavigateHiearchical(typeof(GameSettingsPage), "Game Settings", false);
        }

        private void CustomInject_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.NavigateHiearchical(typeof(CustomDLLInjectPage), "Custom DLL", false);
        }
    }
}

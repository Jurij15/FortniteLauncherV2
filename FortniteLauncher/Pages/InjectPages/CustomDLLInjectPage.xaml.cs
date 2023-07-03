using FortniteLauncher.Cores;
using FortniteLauncher.Dialogs;
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
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Pages.InjectPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomDLLInjectPage : Page
    {
        public static int ProcessID { get; set; }
        public CustomDLLInjectPage()
        {
            this.InitializeComponent();
        }
        private void InjectBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Injector.InjectDll(Convert.ToInt32(PIDBox.Value), DLLPathBox.Text);
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("An error occured while injecting. Error message: " + ex.Message, "Error");
                throw;
            }
        }

        private async void FilePicker_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = (StorageFile)await DialogService.OpenFilePickerToSelectSingleFile(Windows.Storage.Pickers.PickerViewMode.List);
            if (file != null)
            {
                if (file.FileType == ".dll")
                {
                    DLLPathBox.Text = file.Path;
                }
                else
                {
                    DialogService.ShowSimpleDialog("Selected file is not a DLL!", "Error");
                }
            }
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
    }
}

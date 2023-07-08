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

namespace FortniteLauncher.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDLLToLibraryDialog : Page
    {
        public AddDLLToLibraryDialog(ContentDialog PresenterDialog)
        {
            this.InitializeComponent();

            PresenterDialog.PrimaryButtonText = "Add";
            PresenterDialog.PrimaryButtonClick += PresenterDialog_PrimaryButtonClick;

            PresenterDialog.CloseButtonText = "Cancel";

            PresenterDialog.DefaultButton = ContentDialogButton.Primary;
        }

        private async void PresenterDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Managers.DLLibraryManager manager = new Managers.DLLibraryManager();
            await manager.AddDllToLib(DLLNameBox.Text, DLLPathBox.Text);


        }

        private async void ExploreBtn_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = (StorageFile)await DialogService.OpenFilePickerToSelectSingleFile(Windows.Storage.Pickers.PickerViewMode.List);
            if (file == null) { return; }

            if (file.FileType == ".dll")
            {
                DLLPathBox.Text = file.Path;
            }
        }
    }
}

using FortniteLauncher.ContentWindows;
using FortniteLauncher.Dialogs;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinUIEx;

namespace FortniteLauncher.Services
{
    public class DialogService
    {
        public static void ShowSimpleDialog(object Content, string Title)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = Title;
            dialog.Content = Content;

            dialog.CloseButtonText = "OK";

            dialog.DefaultButton = ContentDialogButton.Close;

            dialog.ShowAsync();
        }

        public static void ShowGalleryDialog()
        {
            GalleryWindow window = new GalleryWindow();
            window.Show();
        }

        public static ContentControl CreateContentDialog(string Title, object Content)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = Title;
            dialog.Content = Content;

            return dialog;
        }

        public static async Task<object> OpenFilePickerToSelectSingleFile(PickerViewMode ViewMode)
        {
            // Create a file picker
            FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var window = Globals.Objects.MainWindow;
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Initialize the folder picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            // Set options for your file picker
            openPicker.ViewMode = ViewMode;
            openPicker.FileTypeFilter.Add("*");

            // Open the picker for the user to pick a file
            return await openPicker.PickSingleFileAsync();

        }

        public static async Task<StorageFolder> OpenFolderPickerToSelectSingleFile(PickerViewMode ViewMode)
        {
            // Create a file picker
            FolderPicker openPicker = new Windows.Storage.Pickers.FolderPicker();

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var window = Globals.Objects.MainWindow;
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Initialize the folder picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            // Set options for your file picker
            openPicker.ViewMode = ViewMode;
            openPicker.FileTypeFilter.Add("*");

            // Open the picker for the user to pick a file
            return await openPicker.PickSingleFolderAsync();

        }
    }
}

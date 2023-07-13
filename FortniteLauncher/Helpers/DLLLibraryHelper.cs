using FortniteLauncher.Dialogs;
using FortniteLauncher.Services;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Helpers
{
    public class DLLLibraryHelper
    {
        public static string ISelectedDLLPath {get; private set;}
        public static string ISelectedDLLName {get; private set;}

        public static void SetSelectedDLL(string SelectedDLLPath, string SelectedDLLName)
        {
            ISelectedDLLName = SelectedDLLName;
            ISelectedDLLPath = SelectedDLLPath;
        }

        public static async Task ShowDLLLibrary_SelectDLLDialog()
        {
            ContentDialog dialog = DialogService.CreateBareContentDialog();
            dialog.Title = "Select DLL from Library";
            dialog.Content = new DLLLibrary_SelectDLLDialog(dialog);

            dialog.CloseButtonText = "Cancel";
            dialog.PrimaryButtonText = "Select";
            /*
            dialog.PrimaryButtonClick += ((s, e) => 
            { 

            });
            */

            dialog.DefaultButton = ContentDialogButton.Primary;

            await dialog.ShowAsync();

            SetSelectedDLL(DLLLibrary_SelectDLLDialog.OnDialogClose_SelectedDLLPath, DLLLibrary_SelectDLLDialog.OnDialogClose_SelectedDLLName);
        }
    }
}

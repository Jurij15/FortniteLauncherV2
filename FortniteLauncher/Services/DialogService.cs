using FortniteLauncher.Dialogs;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

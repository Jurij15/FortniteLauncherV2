using FortniteLauncher.Enums;
using FortniteLauncher.Managers;
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

namespace FortniteLauncher.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConfigureAuthDialog : Page
    {
        public ConfigureAuthDialog(ContentDialog PresenterDialog)
        {
            this.InitializeComponent();

            PresenterDialog.PrimaryButtonClick += PresenterDialog_PrimaryButtonClick;
            PresenterDialog.PrimaryButtonText = "Save";

            PresenterDialog.CloseButtonText = "Cancel";

            PresenterDialog.DefaultButton = ContentDialogButton.Primary;

            UsernameBox.Text = Globals.GetPlayerUsername();
            PasswordBox.Text = Globals.GetPlayerPassword();
        }

        private void PresenterDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!string.IsNullOrEmpty(UsernameBox.Text) && !string.IsNullOrWhiteSpace(UsernameBox.Text))
            {
                Globals.SetPlayerUsername(UsernameBox.Text);
            }
            if (!string.IsNullOrEmpty(PasswordBox.Text) && !string.IsNullOrWhiteSpace(PasswordBox.Text))
            {
                Globals.SetPlayerPassword(PasswordBox.Text);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using FortniteLauncher.Pages;
using FortniteLauncher.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InstanceManagerDialog : Page
    {
        public InstanceManagerDialog(ContentDialog PresenterDialog)
        {
            this.InitializeComponent();

            foreach (var item in ProcessHelper.GetAllFortniteProcessesPID())
            {
                ListViewItem listItem = new ListViewItem();
                listItem.Content = item.MainWindowTitle;
                listItem.Name = item.Id.ToString();

                InstancesBox.Items.Add(listItem);
            }

            PresenterDialog.PrimaryButtonClick += PresenterDialog_PrimaryButtonClick;
            PresenterDialog.PrimaryButtonText = "Select";

            PresenterDialog.CloseButtonText = "Cancel";

            PresenterDialog.DefaultButton = ContentDialogButton.Primary;
        }

        private void PresenterDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (InstancesBox.SelectedItem != null)
            {
                int pid = Convert.ToInt32(((ListViewItem)InstancesBox.SelectedItem).Name.ToString());

                InjectPage.ProcessID = pid;
            }

            sender.Hide();
        }
    }
}

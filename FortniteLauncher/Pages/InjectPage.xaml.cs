using FortniteLauncher.Cores;
using FortniteLauncher.Dialogs;
using FortniteLauncher.Helpers;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input.ForceFeedback;
using Windows.Storage;
using Windows.UI.Popups;
using WinUIEx.Messaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InjectPage : Page
    {
        bool PIDPresent = false;
        bool PathPresent = false;
        public static int ProcessID { get; set; }
        public InjectPage()
        {
            this.InitializeComponent();

            PIDBox.Value = 0;
        }

        private async void ShowProcesses_Click(object sender, RoutedEventArgs e)
        {
            StackPanel panel = new StackPanel();
            ListView view = new ListView();

            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.Contains("Fortnite") && !process.ProcessName.Contains("Launcher"))
                {
                    ListViewItem NewItem = new ListViewItem();
                    NewItem.Content = process.ProcessName;
                    NewItem.Name = process.Id.ToString();
                    NewItem.Tag = process.Id;

                    view.Items.Add(NewItem);
                }
            }

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Select a Process";
            dialog.Content = view;

            dialog.CloseButtonText = "Cancel";
            dialog.PrimaryButtonClick += Dialog_PrimaryButtonClick;
            dialog.PrimaryButtonText = "Select";

            dialog.DefaultButton = ContentDialogButton.Primary;

            await dialog.ShowAsync();
        }

        private async void Dialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var selecteditem = ((ListView)sender.Content).SelectedItem;

            if (selecteditem == null) 
            {
                return; 
            }

            PIDBox.Value = Convert.ToInt32(((ListViewItem)selecteditem).Name);


        }

        private async void SelectDLLBtn_Click(object sender, RoutedEventArgs e)
        {
            await DLLLibraryHelper.ShowDLLLibrary_SelectDLLDialog();

            DLLPathBox.Text = DLLLibraryHelper.ISelectedDLLPath;
        }

        private void InjectButton_Click(object sender, RoutedEventArgs e)
        {
            Injector.InjectDll(Convert.ToInt32(PIDBox.Value), DLLPathBox.Text);
        }

        private void Box_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            if (sender.Value > 1)
            {
                PIDPresent = true;
            }
        }

        private void DLLPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           PathPresent = true;
        }

        void ChangeInjectBtnState()
        {
            if (PathPresent && PIDPresent)
            {
                InjectButton.IsEnabled = true;
            }
        }
    }
}

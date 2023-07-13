using CommunityToolkit.Common.Parsers.Core;
using FortniteLauncher.Cores;
using FortniteLauncher.Managers;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Pages.SettingsPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DLLLibrarySettingsPage : Page
    {
        void LoadDLLs(string IfNameContainsThis = null)
        {
            Managers.DLLibraryManager manager = new Managers.DLLibraryManager();

            if (IfNameContainsThis != null)
            {
                DLLListSelector.Items.Clear();
                foreach (var item in manager.GetAllDLLGUIDsThatNameContains(IfNameContainsThis))
                {
                    string name = manager.GetDLLNameByGUID(item);

                    ListViewItem NewItem = new ListViewItem();
                    NewItem.Name = item;
                    NewItem.Content = name;

                    DLLListSelector.Items.Add(NewItem);
                }
            }
            else
            {
                DLLListSelector.Items.Clear();
                foreach (var item in manager.GetAllDLLGuids())
                {
                    string name = manager.GetDLLNameByGUID(item);

                    ListViewItem NewItem = new ListViewItem();
                    NewItem.Name = item;
                    NewItem.Content = name;

                    DLLListSelector.Items.Add(NewItem);
                }
            }
        }
        public DLLLibrarySettingsPage()
        {
            this.InitializeComponent();

            LoadDLLs();

            DLLNameBox.Text = "Select a DLL to begin";
        }

        private void DLLListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Managers.DLLibraryManager manager = new Managers.DLLibraryManager();
            if (DLLListSelector.SelectedItem == null)
            {
                DLLNameBox.Text = "Select a DLL to begin";
                DLLPathBox.Text = "";
                DLLGuidBox.Text = "";
                return;
            }
            string guid = (DLLListSelector.SelectedItem as ListViewItem).Name as string;

            string name = manager.GetDLLNameByGUID(guid);
            string path = manager.GetDLLPathByGUID(guid);

            DLLNameBox.Text = name;
            DLLPathBox.Text = path;
            DLLGuidBox.Text = "GUID: "+guid;

            if (!File.Exists(path))
            {
                DLLPathBox.Text = "File does not exist!";
                DLLPathBox.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
            }
        }

        private async void AddDLLCard_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Add a DLL";
            dialog.Content = new Dialogs.AddDLLToLibraryDialog(dialog);

            dialog.Closed += Dialog_Closed;

            await dialog.ShowAsync();
        }

        private void Dialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            LoadDLLs();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DLLibraryManager manager = new DLLibraryManager();
            manager.SaveNewDLLNameConfigToGuid((DLLListSelector.SelectedItem as ListViewItem).Name as string, DLLNameEditBox.Text);
            manager.SaveNewDLLPathConfigToGuid((DLLListSelector.SelectedItem as ListViewItem).Name as string, DLLPathEditBox.Text);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            DLLibraryManager manager = new DLLibraryManager();

            string guid = (DLLListSelector.SelectedItem as ListViewItem).Name as string;
            DLLListSelector.SelectedItem = null;

            manager.DeleteDLL(guid);

            LoadDLLs();
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            LoadDLLs(sender.Text);
        }
    }
}

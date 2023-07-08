using CommunityToolkit.Common.Parsers.Core;
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
        void LoadDLLs()
        {
            Managers.DLLibraryManager manager = new Managers.DLLibraryManager();

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
        public DLLLibrarySettingsPage()
        {
            this.InitializeComponent();

            LoadDLLs();

            DLLNameBox.Text = "Select a DLL to begin";
        }

        private void DLLListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Managers.DLLibraryManager manager = new Managers.DLLibraryManager();
            string guid = (DLLListSelector.SelectedItem as ListViewItem).Name as string;

            string name = manager.GetDLLNameByGUID(guid);
            string path = manager.GetDLLPathByGUID(guid);

            DLLNameBox.Text = name;
            DLLPathBox.Text = path;
            DLLGuidBox.Text = guid;
        }

        private async void AddDLLCard_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Add a DLL";
            dialog.Content = new Dialogs.AddDLLToLibraryDialog(dialog);

            await dialog.ShowAsync();

            dialog.Closed += Dialog_Closed;
        }

        private void Dialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            LoadDLLs();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

using FortniteLauncher.Classes;
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
    public sealed partial class DLLLibrary_SelectDLLDialog : Page
    {
        public static string OnDialogClose_SelectedDLLName { get; set; }
        public static string OnDialogClose_SelectedDLLPath { get; set; }
        public DLLLibrary_SelectDLLDialog(ContentDialog PresenterDialog)
        {
            this.InitializeComponent();

            PresenterDialog.PrimaryButtonClick += PresenterDialog_PrimaryButtonClick;

            LoadDLLs();
        }

        private void PresenterDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (DLLSelector.SelectedItem == null)
            {
                OnDialogClose_SelectedDLLName = null;
                OnDialogClose_SelectedDLLPath = null;
                return;
            }

            StackPanel content = ((ListViewItem)DLLSelector.SelectedItem).Content as StackPanel;

            foreach (var item in ((StackPanel)content).Children)
            {
                if (item.GetType() == typeof(TextBlock) && ((TextBlock)item).Name == "path")
                {
                    OnDialogClose_SelectedDLLPath = ((TextBlock)item).Text;
                }
                else
                {
                    continue;
                }
            }
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            LoadDLLs(SearchBox.Text);
        }

        void LoadDLLs(string IfNameContainsThis = null)
        {
            Managers.DLLibraryManager manager = new Managers.DLLibraryManager();
            if (IfNameContainsThis != null)
            {
                DLLSelector.Items.Clear();
                foreach (var item in manager.GetAllDLLGUIDsThatNameContains(IfNameContainsThis))
                {
                    string name = manager.GetDLLNameByGUID(item);

                    ListViewItem NewItem = new ListViewItem();
                    NewItem.Name = item;

                    StackPanel content = new StackPanel();
                    TextBlock nameBlock = new TextBlock();
                    TextBlock pathBlock = new TextBlock();

                    content.Spacing = 2;
                    content.Padding = new Thickness(6);

                    nameBlock.Text = name;
                    nameBlock.Name = "dllname";
                    nameBlock.FontSize = 16;
                    nameBlock.HorizontalAlignment = HorizontalAlignment.Left;

                    pathBlock.Text = manager.GetDLLPathByGUID(item);
                    pathBlock.Name = "path";
                    pathBlock.FontSize = 11;
                    pathBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    pathBlock.Foreground = Application.Current.Resources["TextFillColorSecondaryBrush"] as SolidColorBrush;

                    content.Children.Add(nameBlock);
                    content.Children.Add(pathBlock);

                    NewItem.Content = content;

                    DLLSelector.Items.Add(NewItem);
                }
            }
            else
            {

                DLLSelector.Items.Clear();
                foreach (var item in manager.GetAllDLLGuids())
                {
                    string name = manager.GetDLLNameByGUID(item);

                    ListViewItem NewItem = new ListViewItem();
                    NewItem.Name = item;

                    StackPanel content = new StackPanel();
                    TextBlock nameBlock = new TextBlock();
                    TextBlock pathBlock = new TextBlock();

                    content.Spacing = 2;
                    content.Padding = new Thickness(6);

                    nameBlock.Text = name;
                    nameBlock.Name = "dllname";
                    nameBlock.FontSize = 16;
                    nameBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    
                    pathBlock.Text = manager.GetDLLPathByGUID(item);
                    pathBlock.Name = "path";
                    pathBlock.FontSize = 11;
                    pathBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    pathBlock.Foreground = Application.Current.Resources["TextFillColorSecondaryBrush"] as SolidColorBrush;

                    content.Children.Add(nameBlock);
                    content.Children.Add(pathBlock);

                    NewItem.Content = content;

                    DLLSelector.Items.Add(NewItem);
                }
            }
        }
    }
}

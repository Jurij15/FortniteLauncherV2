using Platinum.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Platinum.Pages
{
    /// <summary>
    /// Interaction logic for PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Wpf.Ui.Controls.UiPage
    {
        bool bCustomSSLBypass = false;
        public PlayPage()
        {
            InitializeComponent();
            CurrentlySelectedDLLBlock.Text = "Currently selected: " + System.IO.Path.GetFileName(DLLHelper.GetDefaultSSLBypassDLL()); Globals.CurrentlySelectedSSLBypassDLL = DLLHelper.GetDefaultSSLBypassDLL();
        }

        private void BuildsPageCard_Click(object sender, RoutedEventArgs e)
        {
            Globals.GNavigation.Navigate(typeof(BuildsPage));
        }

        private void LaunchBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ServerPageCard_Click(object sender, RoutedEventArgs e)
        {
            Globals.GNavigation.Navigate(typeof(ServerPage));
        }

        private void OpenDLLSelectorBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectDLLDialog.ShowAndWaitAsync();
            DLLHelper.AddAllSavedDLLsToArray();
            DLLsList.Items.Clear();
            foreach (var item in DLLHelper.SavedDLLsArray)
            {
                DLLsList.Items.Add(item);
            }
        }

        private void SelectDLLDialog_ButtonLeftClick(object sender, RoutedEventArgs e)
        {
            Globals.CurrentlySelectedSSLBypassDLL = DLLsList.SelectedItem.ToString();
            //MessageBox.Show(DLLsList.SelectedItem.ToString());
            CurrentlySelectedDLLBlock.Text = "Currently selected: " + System.IO.Path.GetFileName(Globals.CurrentlySelectedSSLBypassDLL);
            SelectDLLDialog_ButtonRightClick(sender, e);
        }

        private void SelectDLLDialog_ButtonRightClick(object sender, RoutedEventArgs e)
        {
            SelectDLLDialog.Hide();
        }

        private void AddDLLToSaves_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

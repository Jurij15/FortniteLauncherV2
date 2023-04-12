using Platinum.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace Platinum.Pages
{
    /// <summary>
    /// Interaction logic for PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Page
    {
        //bool bCustomSSLBypass = false;
        public PlayPage()
        {
            InitializeComponent();
            CurrentlySelectedDLLBlock.Text = "Currently selected: " + System.IO.Path.GetFileName(DLLHelper.GetDefaultSSLBypassDLL()); Globals.CurrentlySelectedSSLBypassDLL = DLLHelper.GetDefaultSSLBypassDLL();

            //MessageBox.Show("Called!");

            launchArgumentsBox.MaxLength = 25; //this does somehow not limit characters, idk why

            if (!string.IsNullOrEmpty(Globals.CurrentlySelectedBuildPath) && BuildHelper.IsPathValid(Globals.CurrentlySelectedBuildPath))
            {
                BuildUnselectedWarningBar.IsOpen = false;
                string bmp = Globals.CurrentlySelectedBuildPath + FortniteStrings.FortniteSplashImage;
                SplashImage.Source = new BitmapImage(new Uri(bmp));

                CurrentBuildNameBox.Text = Globals.CurrentlySelectedBuild;
                CurrentPathBox.Text = Globals.CurrentlySelectedBuildPath;
                
                ReadyIcon.Visibility = Visibility.Visible;
                UnreadyIcon.Visibility = Visibility.Collapsed;
            }
            else
            {
                BuildUnselectedWarningBar.IsOpen = true;
                CurrentBuildNameBox.Text = "Unselected";
                CurrentPathBox.Text = "Unset";

                ReadyIcon.Visibility = Visibility.Collapsed;
                UnreadyIcon.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrEmpty(Globals.CurrentLaunchArgunments))
            {
                Globals.CurrentLaunchArgunments = FortniteStrings.GetNewLaunchArgumentsForCranium("Player");
                launchArgumentsBox.Text = Globals.CurrentLaunchArgunments;
            }
            else
            {
                launchArgumentsBox.Text = Globals.CurrentLaunchArgunments;
            }

            MarkAsGameserverProcess.IsEnabled = false;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timer_tick;
            timer.Start();
        }

        void timer_tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Globals.CurrentlySelectedBuildPath) && !string.IsNullOrEmpty(Globals.CurrentLaunchArgunments) && !string.IsNullOrEmpty(Globals.CurrentlySelectedSSLBypassDLL) && BuildHelper.IsPathValid(Globals.CurrentlySelectedBuildPath))
            {
                LaunchBtn.IsEnabled = true;
            }
            else
            {
                LaunchBtn.IsEnabled = false;
            }
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

        private void ArgumentBuilderBtn_Click(object sender, RoutedEventArgs e)
        {
            Globals.GNavigation.Navigate(typeof(ArgumentBuilder));
        }
    }
}

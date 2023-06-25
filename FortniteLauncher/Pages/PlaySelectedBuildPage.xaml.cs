using FortniteLauncher.Helpers;
using FortniteLauncher.Managers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
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

namespace FortniteLauncher.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlaySelectedBuildPage : Page
    {
        public PlaySelectedBuildPage()
        {
            this.InitializeComponent();

            BuildsManager manager = new BuildsManager();

            string buildname = manager.GetBuildNameByGUID(Globals.CurrentlySelectedBuildGUID);
            string buildpath = manager.GetBuildPathByGUID(Globals.CurrentlySelectedBuildGUID);
            string buildSeason = manager.GetBuildSeasonByGUID(Globals.CurrentlySelectedBuildGUID);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(buildpath + Globals.FortniteStrings.FortniteSplashImage);
            BannerImg.Source = bitmapImage;

            StatusBox.Text = buildSeason;

            FortniteVersionBlock.Text = "Fortnite";

            if (BuildsHelper.IsPathValid(buildpath))
            {
                PlayButton.Visibility = Visibility.Visible;
                PlayButton.IsEnabled = true;
            }
            else
            {
                PlayButton.Visibility = Visibility.Visible;
                PlayButton.IsEnabled = false;
                ToolTipService.SetToolTip(PlayButton, "Path is not valid!");
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

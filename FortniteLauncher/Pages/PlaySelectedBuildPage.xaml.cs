using FortniteLauncher.Enums;
using FortniteLauncher.Helpers;
using FortniteLauncher.Managers;
using FortniteLauncher.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.WebUI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlaySelectedBuildPage : Page
    {
        private string _buildPath;
        private string _buildName;
        private string _buildSeason;

        private string _buildGUID;
        public PlaySelectedBuildPage()
        {
            this.InitializeComponent();

            BuildsManager manager = new BuildsManager();

            _buildName = manager.GetBuildNameByGUID(Globals.CurrentlySelectedBuildGUID);
            _buildPath = manager.GetBuildPathByGUID(Globals.CurrentlySelectedBuildGUID);
            _buildSeason = manager.GetBuildSeasonByGUID(Globals.CurrentlySelectedBuildGUID);
            _buildGUID = Globals.CurrentlySelectedBuildGUID;

            if (BuildsHelper.IsPathValid(_buildPath))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri(_buildPath + Globals.FortniteStrings.FortniteSplashImage);
                BannerImg.Source = bitmapImage;
            }
            else
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri("ms-appx:///Assets/FNDefaultIcon.png", UriKind.Absolute);
                BannerImg.Source = bitmapImage;
            }

            StatusBox.Text = _buildSeason;

            FortniteVersionBlock.Text = "Fortnite";

            if (BuildsHelper.IsPathValid(_buildPath))
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

            var _enumval = Enum.GetValues(typeof(FortniteSeasons)).Cast<FortniteSeasons>();
            SeasonsComboEdit.ItemsSource = _enumval;

            var selecteditem = Enum.Parse(typeof(FortniteSeasons), _buildSeason);
            SeasonsComboEdit.SelectedItem = selecteditem;

            foreach (var item in Globals.GalleryImages)
            {
                Image img = new Image();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri("ms-appx:///"+item, UriKind.Absolute);
                img.Source = bitmapImage;

                Gallery.Items.Add(img);
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_buildName != BuildNameEditBox.Text && !string.IsNullOrEmpty(BuildNameEditBox.Text) && !string.IsNullOrWhiteSpace(BuildNameEditBox.Text))
            {
                BuildsManager manager = new BuildsManager();
                manager.SaveNewBuildNameConfigToGuid(_buildGUID, BuildNameEditBox.Text);
            }
            if (_buildPath != BuildPathEditBox.Text && !string.IsNullOrEmpty(BuildPathEditBox.Text) && !string.IsNullOrWhiteSpace(BuildPathEditBox.Text))
            {
                BuildsManager manager = new BuildsManager();
                manager.SaveNewBuildPathConfigToGuid(_buildGUID, BuildPathEditBox.Text);
            }
            if (_buildSeason != ((FortniteSeasons)SeasonsComboEdit.SelectedItem).ToString())
            {
                BuildsManager manager = new BuildsManager();
                manager.SaveNewBuildSeasonConfigToGuid(_buildGUID, ((FortniteSeasons)SeasonsComboEdit.SelectedItem).ToString());
            }
        }

        private void DeleteConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            BuildsManager manager = new BuildsManager();
            manager.DeleteBuild(_buildGUID);

            NavigationService.FrameGoBack();
        }
    }
}

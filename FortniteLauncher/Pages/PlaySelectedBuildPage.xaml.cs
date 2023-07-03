using FortniteLauncher.Cores;
using FortniteLauncher.Dialogs;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.WebUI;
using Windows.Web.UI;

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

        private Process FortniteProcess;
        private int FortniteProcessID;
        public PlaySelectedBuildPage()
        {
            this.InitializeComponent();

            BuildsManager manager = new BuildsManager();

            _buildName = manager.GetBuildNameByGUID(Globals.CurrentlySelectedBuildGUID);
            _buildPath = manager.GetBuildPathByGUID(Globals.CurrentlySelectedBuildGUID);
            _buildSeason = manager.GetBuildSeasonByGUID(Globals.CurrentlySelectedBuildGUID);
            _buildGUID = Globals.CurrentlySelectedBuildGUID;

            MainWindowHelper.NavigationResourceDictionary.RemoveContentBackground();

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

            StatusBox.Text = StringsHelper.AddSpacesToNumbers(_buildSeason);

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

            LaunchArgsBox.Text = Globals.FortniteStrings.GetReadyLaunchArguments(Globals.GetPlayerUsername(), Globals.GetPlayerPassword());
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (BuildsHelper.IsPathValid(_buildPath))
            {
                PlayButton.IsEnabled = false;
                PlayCore core = new PlayCore();
                FortniteProcess = core.LaunchFortnite(_buildPath+Globals.FortniteStrings.Fortnite64ShippingExecutablePath, LaunchArgsBox.Text, null, null, false, false);
                FortniteProcessID = FortniteProcess.Id;
                PlayButton.IsEnabled = true;

                if (!core.bLaunchStatus)
                {
                    DialogService.ShowSimpleDialog("An error occured while launching. Error message " + core.GetLaunchErrorsIfAny(), "Error");
                }
                else
                {
                    if (SSLBypassToggle.IsOn)
                    {
                        try
                        {
                            Injector.InjectDll(FortniteProcessID, Config.SSLBypassDLL);
                        }
                        catch (Exception ex)
                        {
                            DialogService.ShowSimpleDialog("An error occured while injecting SSL bypass DLL. Error message: " + ex.Message, "Error");
                        }
                    }
                    if (InjectMemoryLeakFixDll.IsOn)
                    {
                        try
                        {
                            Injector.InjectDll(FortniteProcessID, Config.MemoryLeakFixDLL);
                        }
                        catch (Exception ex)
                        {
                            DialogService.ShowSimpleDialog("An error occured while injecting memory leak fix DLL. Error message: " + ex.Message, "Error");
                        }
                    }
                    if (InjectConsoleDLLSwitch.IsOn)
                    {
                        try
                        {
                            Injector.InjectDll(FortniteProcessID, Config.ConsoleDLL);
                        }
                        catch (Exception ex)
                        {
                            DialogService.ShowSimpleDialog("An error occured while injecting console DLL. Error message: " + ex.Message, "Error");
                        }
                    }

                    NotificationService.SendSimpleToast("Launch Sucessful!", "Enjoy playing Fortnite " + StatusBox.Text, "PID "+FortniteProcessID.ToString(),1.5);
                }
            }
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

            //MainWindowHelper.NavigationResourceDictionary.AddContentBackground();

            //refresh the page
            /*
            if (Globals.Objects.MainFrame.CurrentSourcePageType == typeof(PlayPage))
            {
                NavigationService.UpdateBreadcrumb("Select a build", true);
                NavigationService.ShowBreadcrumb();
                Globals.Objects.MainFrame.Navigate(typeof(PlayPage));
            }
            */
        }

        private void DeleteConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            BuildsManager manager = new BuildsManager();
            manager.DeleteBuild(_buildGUID);

            NavigationService.FrameGoBack();
        }

        private void GalleryGrid_Click(object sender, RoutedEventArgs e)
        {
            DialogService.CreateGalleryWindow();
        }

        private void ConfigureUsername_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Auth Settings";
            dialog.Content = new ConfigureAuthDialog(dialog);

            dialog.ShowAsync();

            LaunchArgsBox.Text = Globals.FortniteStrings.GetReadyLaunchArguments(Globals.GetPlayerUsername(), Globals.GetPlayerPassword());
        }

        private void LaunchSettingsExpander_Expanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            try
            {
                BuildSettingsExpander.IsExpanded = false;
            }
            catch (Exception)
            {
            }
        }

        private void BuildSettingsExpander_Expanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            LaunchSettingsExpander.IsExpanded = false;
        }

        private void LaunchSettingsExpander_Loaded(object sender, RoutedEventArgs e)
        {
            //((Expander)sender).IsExpanded = true;
        }

        private void InjectRelated_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.UpdateBreadcrumb("Inject", true);
            NavigationService.ShowBreadcrumb();
            Globals.Objects.MainFrame.Navigate(typeof(InjectPage));
        }
    }
}

using CommunityToolkit.Labs.WinUI;
using FortniteLauncher.Dialogs;
using FortniteLauncher.Enums;
using FortniteLauncher.Helpers;
using FortniteLauncher.Managers;
using FortniteLauncher.Services;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using FortniteLauncher.Controls;
using FortniteLauncher.Json;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayPage : Page
    {
        public PlayPage()
        {
            this.InitializeComponent();

            LoadBuilds(null);
        }

        void LoadBuilds(string IfContainsThisInName) //so you can load them with parameters
        {

            if (IfContainsThisInName == null)
            {
                BuildsManager manager = new BuildsManager();
                List<string> guids = manager.GetAllGuids().ToList();

                foreach (var item in guids)
                {
                    BuildJson json = manager.GetBuildJsonWithGuid(item);

                    BuildCard card = new BuildCard();
                    card.BuildName = json.Name;
                    card.BuildSeason = StringsHelper.AddSpacesToNumbers(json.Season);
                    card.BuildJson = json;

                    Image image = new Image();

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.UriSource = new Uri(json.Path + Globals.FortniteStrings.FortniteSplashImage);

                    image.Source = bitmapImage;
                    card.BuildImageSource = bitmapImage;

                    //card.SeasonImage = image;

                    ItemsPanel.Items.Add(card);
                }
            }
            else
            {

            }
        }

        private async void AddBuildBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Add a build";
            dialog.Content = new AddBuildDialog(dialog);

            dialog.CloseButtonText = "Cancel";

            dialog.PrimaryButtonText = "Add";

            dialog.DefaultButton = ContentDialogButton.Primary;

            await dialog.ShowAsync();
        }

        private void ItemsPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Globals.SavedBuildsGuids.Clear();
            //ItemsPanel.Items.Clear();
        }

        private async void BulkAddBuildsBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Add a build";
            dialog.Content = new BulkAddBuilds(dialog);

            dialog.CloseButtonText = "Cancel";

            dialog.PrimaryButtonText = "Add";

            dialog.DefaultButton = ContentDialogButton.Primary;

            await dialog.ShowAsync();
        }

        private void ItemsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadBuilds();

            //ItemsPanel.Items.Add(new BuildCard() { BuildJson = new BuildJson(), BuildName = "8,40", BuildSeason = "Season 8"});
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            string GUID = args.SelectedItem.ToString();

            Globals.CurrentlySelectedBuildGUID = GUID;

            BuildsManager manager = new BuildsManager();

            ItemsPanel.Items.Clear();


            if (Globals.Objects.MainFrame is null)
            {
                DialogService.ShowSimpleDialog("Frame is null!", "Error");
                return;
            }


            try
            {
                //NavigationService.Navigate(typeof(PlaySelectedBuildPage), "Play " + manager.GetBuildNameByGUID(GUID), false);
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog(ex.Message, "Error");
                throw;
            }
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // i dont think, i can use this, due to the structure of how i made builds saving work
            /*
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suitableItems = new List<string>();
                var splitText = sender.Text.ToLower().Split(" ");
                foreach (var cat in BuildsManager.Statistics.AllBuildsNames)
                {
                    var found = splitText.All((key) =>
                    {
                        return cat.ToLower().Contains(key);
                    });
                    if (found)
                    {
                        suitableItems.Add(cat);
                    }
                }
                if (suitableItems.Count == 0)
                {
                    suitableItems.Add("No results found");
                }
                sender.ItemsSource = suitableItems;
            }
            */

            if (string.IsNullOrEmpty(sender.Text) || string.IsNullOrWhiteSpace(sender.Text))
            {
                LoadBuilds(null);
            }

            LoadBuilds(sender.Text);
        }

        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private void ViewsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemsPanel.Items.Clear();
            LoadBuilds(null);
        }
    }
}

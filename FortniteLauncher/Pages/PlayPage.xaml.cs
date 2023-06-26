using CommunityToolkit.Labs.WinUI;
using FortniteLauncher.Dialogs;
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

            BuildsManager manager = new BuildsManager();
            Globals.SavedBuildsGuids.Clear();
            foreach (var item in manager.GetAllBuildGuids())
            {
                Globals.SavedBuildsGuids.Add(item);
            }

            LoadBuilds(null);
        }

        async Task CreateCard(string BuildGUID)
        {
            BuildsManager manager = new BuildsManager();

            string buildname = manager.GetBuildNameByGUID(BuildGUID);
            string buildpath = manager.GetBuildPathByGUID(BuildGUID);
            string buildSeason = StringsHelper.AddSpacesToNumbers(manager.GetBuildSeasonByGUID(BuildGUID));

            SettingsCard NewCard = new SettingsCard();
            StackPanel content = new StackPanel();
            Image seasonImage = new Image();
            TextBlock BuildNameBlock = new TextBlock();
            TextBlock BuildSeasonBlock = new TextBlock();

            NewCard.Name = BuildGUID;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(buildpath + Globals.FortniteStrings.FortniteSplashImage);

            seasonImage.Source = bitmapImage;
            seasonImage.Height = 150;
            seasonImage.Width = 120;

            BuildNameBlock.Text = buildname;
            BuildSeasonBlock.Text = buildSeason;

            BuildNameBlock.FontWeight = FontWeights.Medium;
            BuildSeasonBlock.FontSize = 12;
            BuildSeasonBlock.Foreground = Application.Current.Resources["TextFillColorSecondaryBrush"] as SolidColorBrush;

            BuildNameBlock.HorizontalAlignment = HorizontalAlignment.Center;
            BuildSeasonBlock.HorizontalAlignment = HorizontalAlignment.Center;

            content.Spacing = 1;

            content.Children.Add(seasonImage);
            content.Children.Add(BuildNameBlock);
            content.Children.Add(BuildSeasonBlock);

            NewCard.Content = content;

            NewCard.IsClickEnabled = true;
            NewCard.IsActionIconVisible = false;

            NewCard.Click += NewCard_ClickAsync;

            await Task.Delay(7);

            if (!BuildsHelper.IsPathValid(buildpath))
            {
                //NewCard.IsEnabled = false;
                ToolTipService.SetToolTip(NewCard, "Path to this build is invalid!");
                BuildSeasonBlock.Text = "Invalid path!";
            }

            ItemsPanel.Items.Add(NewCard);
        }

        private async void NewCard_ClickAsync(object sender, RoutedEventArgs e)
        {
            string GUID = (sender as SettingsCard).Name;

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
                NavigationService.NavigateHiearchical(typeof(PlaySelectedBuildPage), "Play " + manager.GetBuildNameByGUID(GUID), false);
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog(ex.Message, "Error");
                throw;
            }
        }

        async void LoadBuilds(string IfContainsThisInName) //so you can load them with parameters
        {
            if (IfContainsThisInName != null)
            {
                foreach (var item in Globals.SavedBuildsGuids)
                {
                    await CreateCard(item);
                }
            }
            else
            {
                foreach (var item in Globals.SavedBuildsGuids)
                {
                    await CreateCard(item);
                }
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
            //_versionGuids.Clear();
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
                NavigationService.NavigateHiearchical(typeof(PlaySelectedBuildPage), "Play " + manager.GetBuildNameByGUID(GUID), false);
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

        }

        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }
    }
}

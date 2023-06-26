using FortniteLauncher.Enums;
using FortniteLauncher.Managers;
using FortniteLauncher.Pages;
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
using Windows.Web.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddBuildDialog : Page
    {
        public AddBuildDialog(ContentDialog PresentorDialog)
        {
            this.InitializeComponent();

            var _enumval = Enum.GetValues(typeof(FortniteSeasons)).Cast<FortniteSeasons>();
            SeasonsCombo.ItemsSource = _enumval;

            PresentorDialog.PrimaryButtonClick += PresentorDialog_PrimaryButtonClick;
        }

        private async void PresentorDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            BuildsManager manager = new BuildsManager();
            string bResult = await manager.CreateBuild(BuildNameBox.Text, BuildPathBox.Text, (FortniteSeasons)SeasonsCombo.SelectedItem, true);
            if (bResult == null)
            {
                sender.Hide();
                DialogService.ShowSimpleDialog("Provided path is not a valid path. Check that it contains the FortniteGame and Engine folders and try again.", "Error");
            }
            else
            {
                sender.Hide();

                //refresh the page
                NavigationService.UpdateBreadcrumb("Select a build", true);
                NavigationService.ShowBreadcrumb();
                Globals.Objects.MainFrame.Navigate(typeof(PlayPage));
            }
        }
    }
}

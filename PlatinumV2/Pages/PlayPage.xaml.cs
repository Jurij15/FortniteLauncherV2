using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using NavigationService;
using PlatinumV2.Classes;
using PlatinumV2.Controls;
using PlatinumV2.Helpers;
using PlatinumV2.Interop;
using PlatinumV2.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUIEx.Messaging;
using Microsoft.UI.Xaml.Media.Animation;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserDataTasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PlatinumV2.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayPage : Page
    {
        public static SeasonControl _cachedControl = null;
        SeasonDefinitionsManager seasonDefinitionsManager;

        private void LoadVersions()
        {
            SeasonsView.Items.Clear();
            List<FortniteSeasonBaseClass> seasonsSources = new List<FortniteSeasonBaseClass>();

            seasonsSources = seasonDefinitionsManager.GetAllSeasonDefinitions();

            foreach (var item in seasonsSources)
            {
                SeasonControl control = new SeasonControl();
                control.SeasonCardBackgroundImageSource = ImageHelper.GetImageSource(item.SplashImagePath);
                control.SeasonBaseClass = item;
                control.SeasonDisplayName = item.SeasonDisplayName;
                control.ChapterDisplayName = item.ChapterDisplayName;

                control.Width = 190;
                control.Height = 290;

                control.PointerReleased += Control_PointerReleased;

                SeasonsView.Items.Add(control);
            }
            seasonsSources.Clear();
        }

        private async void Control_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            _cachedControl = sender as SeasonControl;

            (sender as SeasonControl).PrepareToAnimate();

            //await Task.Delay(50);
            //prepare anims
            ConnectedAnimationService.GetForCurrentView().DefaultDuration = new TimeSpan(0, 0, 0, 0, 400);
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardSeasonAnimation", sender as SeasonControl);
            await Task.Delay(50);

            //hide the breadcrumbs
            NavigationService.NavigationService.ChangeBreadcrumbVisibility(false);
            NavigationService.NavigationService.MainNavigation.AlwaysShowHeader = false;
            NavigationService.NavigationService.MainFrame.Navigate(typeof(PlayFortnitePage), sender, new SuppressNavigationTransitionInfo());
            await Task.Delay(50);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationService.NavigationService.ChangeBreadcrumbVisibility(true);
            NavigationService.NavigationService.MainNavigation.AlwaysShowHeader = true;
            base.OnNavigatedTo(e);
        }

        public PlayPage()
        {
            this.InitializeComponent();
            this.DataContext = this;

            seasonDefinitionsManager = new SeasonDefinitionsManager();
            LoadVersions();
        }

        private async void GridView_Loaded(object sender, RoutedEventArgs e)
        {
            var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("BackSeasonAnimation");
            if (_cachedControl != null && anim != null)
            {
                //MessageBox.Show("completed");
                if (anim != null)
                {
                    anim.TryStart(_cachedControl);
                }

                _cachedControl.UndoAnimate();
                _cachedControl = null;
            }
            await Task.Delay(70);
            LoadVersions();
        }
    }
}

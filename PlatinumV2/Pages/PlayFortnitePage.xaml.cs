using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PlatinumV2.Interop;
using Microsoft.UI.Composition.SystemBackdrops;
using CommunityToolkit.WinUI.UI.Media;
using PlatinumV2.Controls;
using PlatinumV2.Classes;
using Microsoft.UI.Xaml.Media.Animation;
using PlatinumV2.Helpers;
using System.Threading.Tasks;
using Windows.Devices.Pwm;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PlatinumV2.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayFortnitePage : Page
    {
        SeasonControl _seasonControl;
        FortniteSeasonBaseClass _baseClass;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //prepare the properties for binding
            _seasonControl = (SeasonControl)e.Parameter;
            _baseClass = _seasonControl.SeasonBaseClass;

            Img.Source = ImageHelper.GetImageSource("ms-appx:///Assets\\HD_transparent_picture.png");
            Img.Source = ImageHelper.GetImageSource(_baseClass.SeasonImagePath);
            InfoGrid.Visibility = Visibility.Visible;

            await Task.Delay(50);

            //ContentScroller.ScrollToVerticalOffset(208);
            ContentScroller.ScrollToVerticalOffset(104);

            await Task.Delay(50);

            var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardSeasonAnimation");
            anim.Completed += Anim_Completed;
            if (anim != null)
            {
                anim.TryStart(FortniteSplashImage);
            }


            base.OnNavigatedTo(e);

            //ShellPage.ChangePaneState(false);

            MainWindow.AppTitleBarBackButton.Visibility = Visibility.Visible;
            MainWindow.AppTitleBarBackButton.Click += AppTitleBarBackButton_Click;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            _baseClass = null;

            NavigationService.NavigationService.ChangeBreadcrumbVisibility(true);
            //just a bug in the service, this just fixes it temporarely
            NavigationService.NavigationService.MainNavigation.AlwaysShowHeader = true;
            MainWindow.AppTitleBarBackButton.Visibility = Visibility.Collapsed;

            if (BackButtonPressed)
            {
                //ConnectedAnimationService.GetForCurrentView().DefaultDuration = new TimeSpan(0, 0, 0, 4);
                var anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackSeasonAnimation", FortniteSplashImage);
                anim.Configuration = new DirectConnectedAnimationConfiguration();
                BackButtonPressed = false;
            }
            base.OnNavigatingFrom(e);
        }

        bool BackButtonPressed;
        private void AppTitleBarBackButton_Click(object sender, RoutedEventArgs e)
        {
            BackButtonPressed = true;
            MainWindow.AppTitleBarBackButton.Click -= AppTitleBarBackButton_Click;
            Img.Source = ImageHelper.GetImageSource("ms-appx:///Assets\\HD_transparent_picture.png");
            NavigationService.NavigationService.Navigate(typeof(PlayPage), NavigationService.NavigationService.NavigateAnimationType.NoAnimation);
        }

        private void Anim_Completed(ConnectedAnimation sender, object args)
        {
        }

        public PlayFortnitePage()
        {
            this.InitializeComponent(); 
            this.DataContext = this;
        }

        //from https://github.com/yoshiask/FluentStore , preparing for parallax view style
        /*
        private void UpdateHeroImageSpacer(FrameworkElement imageElem)
        {
            // Height of the card including padding and spacing
            double cardHeight = InfoGrid.ActualHeight + InfoGrid.Margin.Top + InfoGrid.Margin.Bottom
                + ((StackPanel)ScrollContent.Content).Spacing * 2;

            // Required amount of additional spacing to place the card at the bottom of the hero image,
            // or at the bottom of the page (whichever places the card higher up)
            double offsetA = imageElem.ActualHeight - cardHeight;
            double offsetB = ActualHeight - cardHeight;
            double offset;

            if (offsetA > offsetB)
            {
                offset = offsetB;
            }
            else
            {
                offset = offsetA;
            }
            HeroImageSpacer.Height = Math.Max(offset, 0);
        }
        */

        private void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void InfoGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Calculate the new height for HeroImageSpacer
            double newHeight = PView.ActualHeight - e.NewSize.Height;

            // Ensure the new height is at least 20 pixels above the bottom
            double minHeightAboveBottom = 20;
            newHeight = Math.Max(newHeight-280, minHeightAboveBottom);

            // Set the new height for HeroImageSpacer
            HeroImageSpacer.Height = newHeight;

            // Ensure at least 20 pixels of space below the InfoPane
            double minSpaceBelowInfoPane = 20;
            double spaceBelowInfoPane = ContentScroller.ActualHeight - (e.NewSize.Height + minSpaceBelowInfoPane);

            // Adjust the height of the InfoPane if needed
            if (spaceBelowInfoPane < 0)
            {
                InfoGrid.Height += spaceBelowInfoPane;
            }
        }

        private void ContentScroller_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //PlayButton.Content = ContentScroller.VerticalOffset.ToString();
        }
    }
}

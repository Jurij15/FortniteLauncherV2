using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Services
{
    public enum NavigationPages
    {
        Home,
        Play, //select the build to play
        PrivateServer,//?
        About, //about the launcher //might just merge it into settings
        Settings, 
        //these are not displayed in navigation view, will be navigated to using breadcrumb
        SelectedBuildPage, //after build was selected, show launch options
        PlayerOptionsPage //idk, for stuff like username if using auth argument
    }
    public class NavigationService
    {
        public static void Navigate(Type PageType, string CustomBreadcrumbText = null, bool RemovePreviousBreadcrumbs = false)
        {

        }

        public static void UpdateBreadcrumb(string AddText, bool RemovePrevious)
        {
            if (RemovePrevious)
            {
                Globals.Breadcrumbs.Clear();
            }
            Globals.Breadcrumbs.Add(AddText);

            Globals.UpdateBreadcrumb();
        }

        public static void RemoveBreadcrumbSpecificElement(string SpecificElementName)
        {
            Globals.Breadcrumbs.Remove(SpecificElementName);

            Globals.UpdateBreadcrumb();
        }

        public static void NavigateHiearchical(Type TargetPageType, string BreadcrumbText, bool RemovePreviousText)
        {
            if (TargetPageType == null) { return; }
            UpdateBreadcrumb(BreadcrumbText, RemovePreviousText);

            SlideNavigationTransitionInfo info = new SlideNavigationTransitionInfo();
            info.Effect = SlideNavigationTransitionEffect.FromRight;
            Globals.Objects.MainFrame.Navigate(TargetPageType, null, info);

            ShowBreadcrumb();
        }

        public static bool CanGoBack()
        {
            return Globals.Objects.MainFrame.CanGoBack;
        }

        public static void FrameGoBack(bool bNavigatingHome = false)
        {
            if (!CanGoBack())
            {
                return;
            }
            if (Globals.Breadcrumbs.Count > 1)
            {
                Globals.Breadcrumbs.Remove(Globals.Breadcrumbs[Globals.Breadcrumbs.Count]);
            }
            if (bNavigatingHome)
            {
                Globals.Objects.MainBreadcrumb.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            }
            Globals.UpdateBreadcrumb();
            Globals.Objects.MainFrame.GoBack();
        }

        public static void HideBreadcrumb()
        {
            Globals.Objects.MainBreadcrumb.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            Globals.Objects.MainNavigation.AlwaysShowHeader = false;
        }
        public static void ShowBreadcrumb()
        {
            Globals.Objects.MainBreadcrumb.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            Globals.Objects.MainNavigation.AlwaysShowHeader = true;
        }
    }
}

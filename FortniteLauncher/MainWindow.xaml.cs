using FortniteLauncher.Services;
using Microsoft.UI;
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
using Windows.UI.ApplicationSettings;
using WinUIEx;
using FortniteLauncher.Pages;
using FortniteLauncher.Cores;
using FortniteLauncher.Managers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        void SetCapitionButtonColorForWin10()
        {
            var res = Microsoft.UI.Xaml.Application.Current.Resources;
            Action<Windows.UI.Color> SetTitleBarButtonForegroundColor = (Windows.UI.Color color) => { res["WindowCaptionForeground"] = color; };
            var currentTheme = ((FrameworkElement)Content).ActualTheme;
            if (currentTheme == ElementTheme.Dark)
            {
                SetTitleBarButtonForegroundColor(Colors.White);
            }
            else if (currentTheme == ElementTheme.Light)
            {
                SetTitleBarButtonForegroundColor(Colors.Black);
            }
            else
            {
                if (App.Current.RequestedTheme == ApplicationTheme.Dark)
                {
                    SetTitleBarButtonForegroundColor(Colors.White);
                }
                else
                {
                    SetTitleBarButtonForegroundColor(Colors.Black);
                }
            }
        }
        void InitDesign()
        {
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            this.CenterOnScreen();
            this.SetIsResizable(false);

            SetCapitionButtonColorForWin10();

            MicaBackdrop abackdrop = new MicaBackdrop();

            /*
            if (true)
            {
                //light
                abackdrop.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt; //idk if i should keep this in, it looks nice but idk
            }
            else
            {
                abackdrop.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base;
            }
            */
            this.SystemBackdrop = abackdrop;
        }

        void SetGlobalObjects()
        {
            Globals.Objects.MainWindow = this;
            Globals.Objects.MainFrame = RootFrame;
            Globals.Objects.MainBreadcrumb = MainBreadcrumb;
            Globals.Objects.MainNavigation = MainNavigation;
        }
        public MainWindow()
        {
            this.InitializeComponent();
            InitDesign();
            SetGlobalObjects();
        }

        void OnMainWindowStartup()
        {
            Settings.GetSettings();
        }

        private void AppTitleBackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainNavigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigationService.UpdateBreadcrumb("Settings", true);
                NavigationService.ShowBreadcrumb();
                RootFrame.Navigate(typeof(SettingsPage));
            }
            if (args.InvokedItemContainer == HomeItem)
            {
                NavigationService.UpdateBreadcrumb("Home", true);
                NavigationService.HideBreadcrumb();
                RootFrame.Navigate(typeof(HomePage));
            }
            if (args.InvokedItemContainer == PlayPageItem)
            {
                NavigationService.UpdateBreadcrumb("Select a build", true);
                NavigationService.ShowBreadcrumb();
                RootFrame.Navigate(typeof(PlayPage));
            }
            if (args.InvokedItemContainer == PrivateServerItem)
            {
                NavigationService.UpdateBreadcrumb("Private Server", true);
                NavigationService.ShowBreadcrumb();
                RootFrame.Navigate(typeof(PrivateServerPage));
            }
            if (args.InvokedItemContainer == AboutItem)
            {
                NavigationService.UpdateBreadcrumb("About", true);
                NavigationService.ShowBreadcrumb();
                RootFrame.Navigate(typeof(AboutPage));
            }

            GC.Collect(); //idk, trying to lower ram usage

            MainNavigation.PaneTitle = Globals.PlayerUsername;
        }

        private void MainBreadcrumb_ItemClicked(BreadcrumbBar sender, BreadcrumbBarItemClickedEventArgs args)
        {
            RootFrame.GoBack();
            Globals.Breadcrumbs.RemoveAt(1);
        }

        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Globals.Objects.MainWindowXamlRoot = this.Content.XamlRoot;

            TotalBuildsBlock.Text = "Total: "+ new BuildsManager().GetAllBuildGuids().Count.ToString();
        }
    }
}

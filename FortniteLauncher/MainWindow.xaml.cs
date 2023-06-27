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
using ColorCode.Compilation.Languages;
using FortniteLauncher.Dialogs;

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

            this.SetWindowSize(1500, 820);
            this.CenterOnScreen();
            this.SetIsResizable(false);

            SetCapitionButtonColorForWin10();

            MicaBackdrop abackdrop = new MicaBackdrop();

            this.Title = "Fortnite Launcher";

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
            OnMainWindowStartup();
        }

        void OnMainWindowStartup()
        {
            Settings.GetSettings();

            foreach (var item in Directory.GetFiles("Assets/GalleryContentImages/"))
            {
                Globals.GalleryImages.Add(item);
            }

            NotificationService.InitToast();

            Globals.PrefetchSavedBuilds();
            BuildsManager.Statistics.InitAllBuildsStats();

            MainNavigation.SelectedItem = HomeItem;
            RootFrame.Navigate(typeof(HomePage));
            NavigationService.UpdateBreadcrumb("Home", true);
            NavigationService.HideBreadcrumb();

        }

        private void AppTitleBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.FrameGoBack();
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

        private void MainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (RootFrame.CurrentSourcePageType == typeof(PlaySelectedBuildPage))
            {
                NavigationService.FrameGoBack();
            }
            if (args.IsSettingsSelected)
            {
                NavigationService.UpdateBreadcrumb("Settings", true);
                NavigationService.ShowBreadcrumb();
                RootFrame.Navigate(typeof(SettingsPage));
            }
            if (args.SelectedItemContainer == HomeItem)
            {
                NavigationService.UpdateBreadcrumb("Home", true);
                NavigationService.HideBreadcrumb();
                RootFrame.Navigate(typeof(HomePage));
            }
            if (args.SelectedItemContainer == PlayPageItem)
            {
                NavigationService.UpdateBreadcrumb("Select a build", true);
                NavigationService.ShowBreadcrumb();
                RootFrame.Navigate(typeof(PlayPage));
            }
            if (args.SelectedItemContainer == PrivateServerItem)
            {
                NavigationService.UpdateBreadcrumb("Private Server", true);
                NavigationService.ShowBreadcrumb();
                RootFrame.Navigate(typeof(PrivateServerPage));
            }
            if (args.SelectedItemContainer == AboutItem)
            {
                NavigationService.UpdateBreadcrumb("About", true);
                NavigationService.ShowBreadcrumb();
                RootFrame.Navigate(typeof(AboutPage));
            }
            if (args.SelectedItemContainer == InjectItem)
            {
                NavigationService.UpdateBreadcrumb("Inject", true);
                NavigationService.ShowBreadcrumb();
                RootFrame.Navigate(typeof(InjectPage));
            }

            GC.Collect(); //idk, trying to lower ram usage

            AuthUsernameBlock.Text = Globals.GetPlayerUsername();
        }

        private async void PaneContent_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Globals.Objects.MainWindowXamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Auth Settings";
            dialog.Content = new ConfigureAuthDialog(dialog);

            await dialog.ShowAsync();
        }

        private void AppTitlePaneButton_Click(object sender, RoutedEventArgs e)
        {
            bool Current = MainNavigation.IsPaneOpen;
            if (Current)
            {
                MainNavigation.IsPaneOpen = false;
            }
            else
            {
                MainNavigation.IsPaneOpen= true;
            }
        }
    }
}

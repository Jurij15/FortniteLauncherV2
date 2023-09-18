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
using Windows.System;
using FortniteLauncher.Pages.GuidesPages;
using static FortniteLauncher.Services.NavigationService;
using FortniteLauncher.Classes;
using Microsoft.UI.Windowing;

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

            if (Globals.Settings.ShowContentBackgroundLayer)
            {
                MainNavigationDisableContentBackgroundDictionary.ThemeDictionaries.Clear();
            }

            this.SystemBackdrop = abackdrop;

            this.SetIcon("Assets\\FNLauncher_Icon.ico");
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

            InitializeNavigationService(MainNavigation, MainBreadcrumb, RootFrame);

            OnMainWindowStartup();
        }

        void OnMainWindowStartup()
        {

            foreach (var item in Directory.GetFiles("Assets/GalleryContentImages/"))
            {
                Globals.GalleryImages.Add(item);
            }

            Logger.Log(LogImportance.Info, LogSource.Startup, "Found " + Globals.GalleryImages.Count + " gallery images!");

            NotificationService.InitToast();

            MainNavigation.SelectedItem = HomeItem;
            NavigationService.Navigate(typeof(HomePage), "Home", true);
            NavigationService.ChangeBreadcrumbVisibility(false);

            if (!Globals.bIsFirstTimeRun)
            {
                InfoDot.Visibility = Visibility.Collapsed;
            }

        }

        private void AppTitleBackButton_Click(object sender, RoutedEventArgs e)
        {
            /* Crashes idk why
            if (RootFrame.BackStack[RootFrame.BackStack.Count].SourcePageType == typeof(HomePage))
            {
                NavigationService.FrameGoBack(true);
            }
            else
            {
                NavigationService.FrameGoBack();
            }
            */
        }

        private void MainBreadcrumb_ItemClicked(BreadcrumbBar sender, BreadcrumbBarItemClickedEventArgs args)
        {
            if (args.Index < NavigationService.BreadCrumbs.Count - 1)
            {
                var crumb = (Breadcrumb)args.Item;
                crumb.NavigateToFromBreadcrumb(args.Index);
            }

            Logger.Log(LogImportance.Info, LogSource.Navigation, "Breadcrumb item clicked");
        }

        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            try
            {
                Logger.Log(LogImportance.Info, LogSource.Navigation, "Frame Navigated to " + e.Content);
            }
            catch (Exception ex)
            {
            }
        }

        private async void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Globals.Objects.MainWindowXamlRoot = this.Content.XamlRoot;
            Logger.Log(LogImportance.Info, LogSource.AppCore, "Initialized app XamlRoot");

            if (Globals.bIsFirstTimeRun)
            {
                ContentDialog dialog = DialogService.CreateContentDialog("", new DebugTestingWelcomeDialog());
                dialog.CloseButtonText = "OK";
                await dialog.ShowAsync();
            }
        }

        private void MainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                NavigationService.Navigate(typeof(SettingsPage), "Settings", true);
            }
            if (args.SelectedItemContainer == HomeItem)
            {
                NavigationService.Navigate(typeof(HomePage), "Home", true);
                ChangeBreadcrumbVisibility(false);
            }
            if (args.SelectedItemContainer == PlayPageItem)
            {
                NavigationService.Navigate(typeof(PlayPage), "Select a build", true);
            }
            if (args.SelectedItemContainer == PrivateServerItem)
            {
                NavigationService.Navigate(typeof(PrivateServerPage), "Private Server", true);
            }
            if (args.SelectedItemContainer == AboutItem)
            {
                NavigationService.Navigate(typeof(AboutPage), "About", true);
            }
            if (args.SelectedItemContainer == InjectItem)
            {
                NavigationService.Navigate(typeof(InjectPage), "Inject", true);
            }
            if (args.SelectedItemContainer == GuidesItem)
            {
                NavigationService.Navigate(typeof(GuidesPage), "Guides", true);
            }

            AuthUsernameBlock.Text = Globals.Settings.PlayerAuthUsername;

            Logger.Log(LogImportance.Info, LogSource.Navigation, "Selected page for navigation: "+args.SelectedItemContainer.Content.ToString());
        }

        private async void PaneContent_Click(object sender, RoutedEventArgs e)
        {
            InfoDot.Visibility = Visibility.Collapsed;
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

            Logger.Log(LogImportance.Info, LogSource.AppCore, "IsPaneOpen state changed to "+MainNavigation.IsPaneOpen.ToString());
        }

        private void MainNavigation_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}

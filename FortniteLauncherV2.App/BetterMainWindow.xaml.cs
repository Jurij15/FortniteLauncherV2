using FortniteLauncherV2.App.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace FortniteLauncherV2.App
{
    /// <summary>
    /// Interaction logic for BetterMainWindow.xaml
    /// </summary>
    public partial class BetterMainWindow : Wpf.Ui.Controls.UiWindow
    {
        #region Services
        private readonly IThemeService _themeService;

        private readonly ISnackbarService _snackbarService;

        SnackbarService snackbarService = new SnackbarService();
        ThemeService themeService = new ThemeService(); 
        #endregion
        public BetterMainWindow()
        {
            InitializeComponent();
            //RootSnackBar.Show();

            //snackbarService.SetSnackbarControl(RootSnackBar);
            //_snackbarService = snackbarService;
            _themeService = themeService;
            themeService.SetSystemAccent();

            //ShowRootSnackbar("hello", "hi,");

            //Settings.Init();
            Globals.SetupConsole();
            //RootDialog.ShowAndWaitAsync();
            Globals.bRootDialog = RootDialog;
            Globals.NavStore = NavigationStore;
            //MessageBox.Show("Version: "+ Version.VersionString, "Version Report", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ThemeFooter_Click(object sender, RoutedEventArgs e)
        {
            if (themeService.GetTheme() == Wpf.Ui.Appearance.ThemeType.Dark)
            {
                themeService.SetTheme(Wpf.Ui.Appearance.ThemeType.Light);
            }
            else if (themeService.GetTheme() == Wpf.Ui.Appearance.ThemeType.Light)
            {
                themeService.SetTheme(Wpf.Ui.Appearance.ThemeType.Dark);
            }
        }

        private void NavigationStore_Navigated(Wpf.Ui.Controls.Interfaces.INavigation sender, Wpf.Ui.Common.RoutedNavigationEventArgs e)
        {
            //NavigationStore.Navigate(2);
            //MessageBox.Show("navigated");
        }

        public void SetPageService(IPageService pageService)
    => NavigationStore.PageService= pageService;

        private void RootDialog_ButtonRightClick(object sender, RoutedEventArgs e)
        {
            RootDialog.Hide();
        }
    }
}

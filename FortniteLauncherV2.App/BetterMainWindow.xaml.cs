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

        #region Functions
        /*
        public void ShowRootSnackbar(string SnackarText = null, string SnackbarContent = null, SymbolRegular SnackbarSymbol = SymbolRegular.GlanceDefault12, ControlAppearance SnackbarControlAppearance = ControlAppearance.Dark)
        {
            _snackbarService.Show(SnackarText, SnackbarContent, SnackbarSymbol, SnackbarControlAppearance);
        }
        */
        //this ShowSnackbar will appear in all pages and anywhere there is a snackbar control
        public void ShowSnackbar(string SnackarText = null, string SnackbarContent = null, SymbolRegular SnackbarSymbol = SymbolRegular.GlanceDefault12, ControlAppearance SnackbarControlAppearance = ControlAppearance.Dark)
        {
            _snackbarService.Show(SnackarText, SnackbarContent, SnackbarSymbol, SnackbarControlAppearance);
        }
        #endregion
        public BetterMainWindow()
        {
            InitializeComponent();
            //RootSnackBar.Show();
            NavigationStore.Navigate(1);

            //snackbarService.SetSnackbarControl(RootSnackBar);
            //_snackbarService = snackbarService;
            _themeService = themeService;
            themeService.SetSystemAccent();
            //ShowRootSnackbar("hello", "hi,");
            NavigationStore.Navigate(typeof(AboutPage));  

            Settings.Init();
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
    }
}

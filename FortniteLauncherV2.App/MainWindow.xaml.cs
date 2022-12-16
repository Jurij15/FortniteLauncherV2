using FortniteLauncherV2.App.Windows;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace FortniteLauncherV2.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Wpf.Ui.Controls.UiWindow
    {
        #region services
        private bool bInitialized = false;

        private readonly IThemeService _themeService;

        private readonly IDialogControl _dialogControl;

        private readonly ISnackbarService _snackbarService;

        private readonly ISnackbarControl _snackbarControl;
        #endregion

        public enum Tabs : int
        {
            Play,
            Player, 
            Server,
            Inject, 
            Settings,
            About,
            None
        }

        public string GetCurrentlySelectedTabName()
        {
            TabItem currentTab = (TabItem)ControlTabs.SelectedItem;
            return currentTab.Name;
        }

        public Tabs IdentifyCurrentTab()
        {
            if (GetCurrentlySelectedTabName() == "PlayTab")
            {
                return Tabs.Play;
            }

            return Tabs.None;
        }

        public void ShowSnacBar(Tabs CurrentTab, string SnackarText = null, SymbolRegular SnackbarSymbol = SymbolRegular.GlanceDefault12, ControlAppearance SnackbarControlAppearance = ControlAppearance.Info)
        {
            if (CurrentTab == Tabs.Play)
            {
                PlayTabSnackBar.Content= SnackarText;
                _snackbarService.Show("hi");
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            SnackbarService snackbarService = new SnackbarService();
            DialogService dialogService = new DialogService();


            snackbarService.SetSnackbarControl(PlayTabSnackBar);
            _snackbarService = snackbarService;
            //_dialogControl = dialogService.GetDialogControl();

            _themeService= new ThemeService();

            _themeService.SetSystemAccent();

            ControlTabs.SelectedIndex = 0;

            this.WindowCornerPreference = Wpf.Ui.Appearance.WindowCornerPreference.Round;
            ShowSnacBar(IdentifyCurrentTab());
        }
        private void UiWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

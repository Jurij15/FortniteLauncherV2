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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace FortniteLauncherV2.App.Pages
{
    /// <summary>
    /// Interaction logic for PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Wpf.Ui.Controls.UiPage
    {
        private readonly ISnackbarService _snackbarService;
        SnackbarService snackbarService = new SnackbarService();

        public void ShowSnackbar(string SnackarText = null, string SnackbarContent = null, SymbolRegular SnackbarSymbol = SymbolRegular.GlanceDefault12, ControlAppearance SnackbarControlAppearance = ControlAppearance.Dark)
        {
            _snackbarService.Show(SnackarText, SnackbarContent, SnackbarSymbol, SnackbarControlAppearance);
        }
        public PlayPage()
        {
            InitializeComponent();

            snackbarService.SetSnackbarControl(playSnackbar);
            _snackbarService = snackbarService;
            //<ShowSnackbar();

        }
    }
}

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

namespace Platinum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Wpf.Ui.Controls.Window.FluentWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Globals.GNavigation = MainWindowNavStore;
            Globals.MWindow = this;

            Wpf.Ui.Appearance.Watcher.Watch(this);
        }

        private void ThemeButtonNavigation_Click(object sender, RoutedEventArgs e)
        {
            var currentTheme = Wpf.Ui.Appearance.Theme.GetAppTheme();

            Wpf.Ui.Appearance.Theme.Apply(currentTheme == Wpf.Ui.Appearance.ThemeType.Light ? Wpf.Ui.Appearance.ThemeType.Dark : Wpf.Ui.Appearance.ThemeType.Light);
        }

        private void UiWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

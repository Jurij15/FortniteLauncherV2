using FortniteLauncherV2.Common.Launcher;
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

namespace FortniteLauncherV2.App.Pages
{
    /// <summary>
    /// Interaction logic for BuildsPage.xaml
    /// </summary>
    public partial class BuildsPage : Wpf.Ui.Controls.UiPage
    {
        public BuildsPage()
        {
            InitializeComponent();

            if (!Build.IsPathValid(Config.FortniteGameEnginePath))
            {
                InvalidPath.IsOpen = true;
            }
            if (Build.IsPathValid(Config.FortniteGameEnginePath))
            {
                PathBox.Text = Config.FortniteGameEnginePath;
            }
        }

        private void PathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Build.IsPathValid(PathBox.Text))
            {
                Config.FortniteGameEnginePath = PathBox.Text;
                InvalidPath.IsOpen = false;
            }
            else if (!Build.IsPathValid(PathBox.Text))
            {
                Config.FortniteGameEnginePath = string.Empty;
                InvalidPath.IsOpen = true;
            }
        }
    }
}

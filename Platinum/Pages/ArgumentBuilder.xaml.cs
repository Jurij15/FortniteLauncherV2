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

namespace Platinum.Pages
{
    /// <summary>
    /// Interaction logic for ArgumentBuilder.xaml
    /// </summary>
    public partial class ArgumentBuilder : Wpf.Ui.Controls.UiPage
    {
        public ArgumentBuilder()
        {
            InitializeComponent();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Globals.GFrame.Navigate(new PlayPage());
        }
    }
}

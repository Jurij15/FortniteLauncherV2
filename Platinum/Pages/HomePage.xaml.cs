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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        void AnimateAllElements()
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(BuildsPageCard, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 200);
            Wpf.Ui.Animations.Transitions.ApplyTransition(PlayCard, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 200);
        }

        public HomePage()
        {
            InitializeComponent();
            AnimateAllElements();
        }

        private void BuildsPageCard_Click(object sender, RoutedEventArgs e)
        {
            Globals.GNavigation.Navigate(typeof(BuildsPage));
        }

        private void RerollImageBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayCard_Click(object sender, RoutedEventArgs e)
        {
            Globals.GNavigation.Navigate(typeof(PlayPage));
        }

        private void ServerCard_Click(object sender, RoutedEventArgs e)
        {
            Globals.GNavigation.Navigate(typeof(ServerPage));
        }
    }
}

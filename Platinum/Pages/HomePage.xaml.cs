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
    public partial class HomePage : Wpf.Ui.Controls.UiPage
    {
        void AnimateAllElements()
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(TitleBlock, Wpf.Ui.Animations.TransitionType.FadeIn, 150);
            Wpf.Ui.Animations.Transitions.ApplyTransition(SubtitleBlock, Wpf.Ui.Animations.TransitionType.FadeIn, 150);
            Wpf.Ui.Animations.Transitions.ApplyTransition(BuildsPageCard, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 200);
            Wpf.Ui.Animations.Transitions.ApplyTransition(PlayCard, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 200);
        }

        public HomePage()
        {
            InitializeComponent();
            GradientBorder.Visibility = Visibility.Visible;
            AnimateAllElements();
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GradientBorder.IsVisible == true)
            {
                Wpf.Ui.Animations.Transitions.ApplyTransition(GradientBorder, Wpf.Ui.Animations.TransitionType.SlideBottom, 195);
                GradientBorder.Visibility = Visibility.Collapsed;
                MenuBtn.Content = "Open Menu";
            }
            else if (GradientBorder.IsVisible == false)
            {
                Wpf.Ui.Animations.Transitions.ApplyTransition(GradientBorder, Wpf.Ui.Animations.TransitionType.FadeIn, 195);
                GradientBorder.Visibility = Visibility.Visible;
                MenuBtn.Content = "Close Menu";
            }
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

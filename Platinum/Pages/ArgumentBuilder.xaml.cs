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

            CurrentLaunchArgumentsBox.Text = Settings.Settings.GetLaunchArguments();

            DocsBox.Document.Blocks.Add(new Paragraph(new Run("What should be here is basic explanation about every (common) argument")));
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Globals.CurrentLaunchArgunments = CurrentLaunchArgumentsBox.Text;
            Settings.Settings.SaveLaunchArguments(CurrentLaunchArgumentsBox.Text);
            Globals.GFrame.Navigate(new PlayPage());
        }

        private void CraniumArgsAction_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserNameBox.Text))
            {
                Globals.UserName = "Player";
            }
            else { Globals.UserName = UserNameBox.Text; }
            CurrentLaunchArgumentsBox.Text = FortniteStrings.GetNewLaunchArgumentsForCranium(Globals.UserName);
        }

        private void PreS5Args_Click(object sender, RoutedEventArgs e)
        {
            CurrentLaunchArgumentsBox.Text = FortniteStrings.GetPreSeason5LaunchArgs();
        }

        private void UserNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserNameBox.Text))
            {
                Globals.UserName = "Player";
            }
            else { Globals.UserName = UserNameBox.Text; }
            CurrentLaunchArgumentsBox.Text = FortniteStrings.GetNewLaunchArgumentsForCranium(Globals.UserName);
        }
    }
}

using FortniteLauncherV2.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Image = System.Windows.Controls.Image;

namespace FortniteLauncherV2.App.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Wpf.Ui.Controls.UiPage
    {
        static int rand; //this has to be static because of random (something with seeds?)

        void SetImage()
        {
            if (Config.ShowHomeScreenBackground)
            {
                if (!Directory.Exists(Strings.BackgroundsDirectory))
                {
                    return;
                }
                //get a random backgroud for home
                List<string> backgroundslist = new List<string>();
                string[] AllImagePaths = Directory.GetFiles(Strings.BackgroundsDirectory);
                foreach (var item in AllImagePaths)
                {
                    backgroundslist.Add(item); //we have all items in the array now, lets now pick a random one
                }
                Random random = new Random();
                rand = random.Next(backgroundslist.Count);
                string path = backgroundslist[rand];
                //MessageBox.Show(path);
                ImageBrush img = new ImageBrush();
                img.ImageSource = new BitmapImage(new Uri(path, UriKind.Relative));
                BaseBorder.Background = img;

                FileNameBlock.Text = System.IO.Path.GetFileName(path);

                backgroundslist.Clear();
                rand = 0;
            }
        }

        public Home()
        {
            InitializeComponent();
            SetImage();
        }

        private void RerollImageLink_Click(object sender, RoutedEventArgs e)
        {
            SetImage();
        }

        private void StartButtonClick_Click(object sender, RoutedEventArgs e)
        {
            //this will just redirect to launch
            Globals.NavStore.Navigate(typeof(PlayPage));
        }

        private void SelectBuildAction_Click(object sender, RoutedEventArgs e)
        {
            Globals.NavStore.Navigate(typeof(BuildsPage));
        }

        private void LaunchBuildAction_Click(object sender, RoutedEventArgs e)
        {
            Globals.NavStore.Navigate(typeof(PlayPage));
        }

        private void ServerAction_Click(object sender, RoutedEventArgs e)
        {
            Globals.NavStore.Navigate(typeof(ServerPage));
        }
    }
}

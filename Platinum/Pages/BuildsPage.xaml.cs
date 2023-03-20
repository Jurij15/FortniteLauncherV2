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
using System.IO;
using Platinum.Settings;
using Platinum.Helpers;
using System.Threading;

namespace Platinum.Pages
{
    /// <summary>
    /// Interaction logic for BuildsPage.xaml
    /// </summary>
    public partial class BuildsPage : Wpf.Ui.Controls.UiPage
    {
        public int AddedBuildsIndex = 0;
        public Wpf.Ui.Controls.ProgressRing ring = new Wpf.Ui.Controls.ProgressRing();
        public void AddNewBuildCard(string name, string path)
        {
            Wpf.Ui.Controls.CardAction NewCard = new Wpf.Ui.Controls.CardAction();
            StackPanel CardPanel = new StackPanel();
            Image img = new Image();
            TextBlock buildName = new TextBlock();

            string bmp = path + FortniteStrings.FortniteSplashImage;
            img.Source = new BitmapImage(new Uri(bmp));
            img.Height = 140;
            img.Width = 170;

            buildName.Text = name;
            buildName.VerticalAlignment = VerticalAlignment.Center;
            buildName.HorizontalAlignment = HorizontalAlignment.Center;
            buildName.FontWeight = FontWeights.Medium;
            buildName.FontSize = 18;

            CardPanel.Children.Add(img);
            CardPanel.Children.Add(buildName);

            NewCard.Content = CardPanel;
            NewCard.Click += OnBuildCard_Clicked;

            NewCard.Height = 200;
            NewCard.Width = 200;

            NewCard.Margin = new Thickness(2, 2, 2, 2);

            RootPanel.Children.Insert(AddedBuildsIndex, NewCard);
            AddedBuildsIndex++; 
        }

        public BuildsPage()
        {
            InitializeComponent();
            
            ring.IsIndeterminate = true;

            RootPanel.Children.Add(ring);

            //InitAllBuildsToRootPanel();
        }

        private void UiPage_Loaded(object sender, RoutedEventArgs e)
        {
            InitAllBuildsToRootPanel();
        }

        public void InitAllBuildsToRootPanel()
        {
            RootPanel.Children.Clear();
            AddedBuildsIndex = 0;
            foreach (var file in Directory.GetFiles(Locations.Directories.BuildsSavesDir))
            {
                string path = File.ReadAllText(file);
                if (BuildHelper.IsPathValid(path))
                {
                    //MessageBox.Show(path);
                    AddNewBuildCard(System.IO.Path.GetFileName(file), path);
                }
            }

            RootPanel.Children.Remove(ring);
        }

        private void OnBuildCard_Clicked(object sender, EventArgs e)
        {
            string FileName = string.Empty;
            string path = string.Empty;

            Wpf.Ui.Controls.CardAction card = (Wpf.Ui.Controls.CardAction)sender;
            StackPanel panel = card.Content as StackPanel;

            foreach (var item in panel.Children)
            {
                if (item.GetType() == typeof(TextBlock))
                {
                    TextBlock namebox = (TextBlock)item;
                    FileName = namebox.Text;
                }
            }
            
            if(string.IsNullOrEmpty(FileName)) { return; } // name not found

            path = File.ReadAllText(Locations.Directories.BuildsSavesDir + FileName);

            BuildNameBlock.Text = FileName;
            CurrentPathBox.Text = path;

            Globals.CurrentlySelectedBuildPath = path;
            Globals.CurrentlySelectedBuild = FileName;
        }

        private void CurrentPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!BuildHelper.IsPathValid(CurrentPathBox.Text))
            {
                BuildInvalid.IsOpen = true;
                BuildInvalid.IsClosable = false;

                BuildInvalidMark.Visibility = Visibility.Visible;
                BuildValidMark.Visibility = Visibility.Collapsed;

                Globals.CurrentlySelectedBuildPath = CurrentPathBox.Text;
            }
            else
            {
                BuildInvalid.IsOpen = false;
                BuildInvalid.IsClosable = false;

                BuildInvalidMark.Visibility = Visibility.Collapsed;
                BuildValidMark.Visibility = Visibility.Visible;

                Globals.CurrentlySelectedBuildPath = null;
            }
        }

        private void SaveNewBuildBtn_Click(object sender, RoutedEventArgs e)
        {
            string NewBuildPath = NewBuildPathBox.Text;
            string NewBuildName = NewBuildNameBox.Text;

            using (StreamWriter sw = File.CreateText(Locations.Directories.BuildsSavesDir + NewBuildName)) 
            { 
                sw.Write(NewBuildPath);
                sw.Close();
            }

            Globals.GFrame.Navigate(new PlayPage());
            Globals.GFrame.Navigate(new BuildsPage());
        }
    }
}

using FortniteLauncher.Json;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Controls
{
    public sealed partial class BuildCard : UserControl
    {
        public static readonly DependencyProperty BuildNameProperty =
            DependencyProperty.Register("BuildName", typeof(string), typeof(BuildCard), new PropertyMetadata(null));
        public string BuildName
        {
            get { return (string)GetValue(BuildNameProperty); }
            set { SetValue(BuildNameProperty, value); }
        }

        public static readonly DependencyProperty BuildSeasonProperty =
            DependencyProperty.Register("BuildSeason", typeof(string), typeof(BuildCard), new PropertyMetadata(null));
        public string BuildSeason
        {
            get { return (string)GetValue(BuildSeasonProperty); }
            set { SetValue(BuildSeasonProperty, value); }
        }

        public static readonly DependencyProperty BuildJsonProperty =
            DependencyProperty.Register("BuildJson", typeof(BuildJson), typeof(BuildCard), new PropertyMetadata(null));
        public BuildJson BuildJson
        {
            get { return (BuildJson)GetValue(BuildJsonProperty); }
            set { SetValue(BuildJsonProperty, value); }
        }

        public static readonly DependencyProperty BuildImageSourceProperty =
            DependencyProperty.Register("BuildImageSource", typeof(ImageSource), typeof(BuildCard), new PropertyMetadata(null));
        public ImageSource BuildImageSource
        {
            get { return (ImageSource)GetValue(BuildImageSourceProperty); }
            set { SetValue(BuildImageSourceProperty, value); }
        }

        public Image SeasonImage;

        public BuildCard()
        {
            this.InitializeComponent();

            SeasonImage = Img;
            this.DataContext = this;
        }

        private void SetPointerNormalState(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }

        private void SetPointerOverState(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "PointerOver", true);
        }

        private void SetPointerPressedState(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Pressed", true);
        }
    }
}

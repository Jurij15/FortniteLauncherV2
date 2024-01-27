using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PlatinumV2.Controls
{
    public sealed partial class SeasonControl : UserControl
    {
        public static readonly DependencyProperty SeasonBaseClassProperty =
            DependencyProperty.Register("SeasonBaseClass", typeof(FortniteSeasonBaseClass), typeof(SeasonControl), new PropertyMetadata(null));
        public FortniteSeasonBaseClass SeasonBaseClass
        {
            get { return (FortniteSeasonBaseClass)GetValue(SeasonBaseClassProperty); }
            set { SetValue(SeasonBaseClassProperty, value); }
        }

        public static readonly DependencyProperty SeasonCardBackgroundImageSourceProperty =
            DependencyProperty.Register("SeasonCardBackgroundImageSource", typeof(ImageSource), typeof(SeasonControl), new PropertyMetadata(null));
        public ImageSource SeasonCardBackgroundImageSource
        {
            get { return (ImageSource)GetValue(SeasonCardBackgroundImageSourceProperty); }
            set { SetValue(SeasonCardBackgroundImageSourceProperty, value); }
        }

        public static readonly DependencyProperty SeasonDisplayNameProperty =
            DependencyProperty.Register("SeasonDisplayName", typeof(string), typeof(SeasonControl), new PropertyMetadata(null));
        public string SeasonDisplayName
        {
            get { return (string)GetValue(SeasonDisplayNameProperty); }
            set { SetValue(SeasonDisplayNameProperty, value); }
        }

        public static readonly DependencyProperty ChapterDisplayNameProperty =
            DependencyProperty.Register("ChapterDisplayName", typeof(string), typeof(SeasonControl), new PropertyMetadata(null));
        public string ChapterDisplayName
        {
            get { return (string)GetValue(ChapterDisplayNameProperty); }
            set { SetValue(ChapterDisplayNameProperty, value); }
        }

        public SeasonControl()
        {
            this.InitializeComponent();
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

        public void PrepareToAnimate()
        {
            oSeasonInformation.Visibility = Visibility.Collapsed;
            oChapterInformation.Visibility = Visibility.Collapsed;
        }

        public void UndoAnimate()
        {
            oSeasonInformation.Visibility = Visibility.Visible;
            //oChapterInformation.Visibility = Visibility.Visible;
        }
    }
}

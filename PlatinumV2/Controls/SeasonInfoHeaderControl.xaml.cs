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

namespace PlatinumV2.Controls
{
    public sealed partial class SeasonInfoHeaderControl : UserControl
    {
        public new UIElement Content
        {
            get => (UIElement)GetValue(ContentProperty);
            set
            {
                SetValue(ContentProperty, value);
            }
        }
        public static new readonly DependencyProperty ContentProperty =
        DependencyProperty.Register("Content", typeof(UIElement), typeof(SeasonInfoHeaderControl), new PropertyMetadata(null));

        public new string Header
        {
            get => (string)GetValue(HeaderProperty);
            set
            {
                SetValue(HeaderProperty, value);
            }
        }

        public static new readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(string), typeof(SeasonInfoHeaderControl), new PropertyMetadata(null));

        public new UIElement HeaderContent
        {
            get => (UIElement)GetValue(HeaderContentProperty);
            set
            {
                SetValue(HeaderContentProperty, value);
            }
        }
        public static new readonly DependencyProperty HeaderContentProperty =
        DependencyProperty.Register("HeaderContent", typeof(UIElement), typeof(SeasonInfoHeaderControl), new PropertyMetadata(null));

        public new Thickness ContentMargin
        {
            get => new Thickness(12);
            set
            {
                SetValue(HeaderProperty, value);
            }
        }
        public static new readonly DependencyProperty ContentMarginProperty =
        DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(SeasonInfoHeaderControl), new PropertyMetadata(null));

        public SeasonInfoHeaderControl()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }
}

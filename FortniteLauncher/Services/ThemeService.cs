using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.WindowManagement;

namespace FortniteLauncher.Services
{
    public class ThemeService
    {
        public static void ChangeTheme(ElementTheme theme)
        {
            if (Globals.Objects.m_window.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = theme;
            }
        }

        //disable/enable the background layer
        public static void SetContentBackgrund(bool ShowBackground)
        {
            if (ShowBackground)
            {
                Globals.Objects.MainNavigation.Resources.Clear();
            }
            else if(!ShowBackground)
            {
                SolidColorBrush brush = Globals.Objects.MainNavigation.Resources["NavigationViewContentGridBorderBrush"] as SolidColorBrush;
                //SolidColorBrush brush = (SolidColorBrush)App.Current.Resources["NavigationViewContentGridBorderBrush"];

                Globals.Objects.MainNavigation.Resources.Add("NavigationViewContentGridBorderBrush", brush);
                Globals.Objects.MainNavigation.Resources.Add("NavigationViewContentBackground", brush);
            }

            Thickness t = new Thickness(56, 26, 0, 0);
            Globals.Objects.MainNavigation.Resources.Add("NavigationViewHeaderMargin", t);
        }

        public static bool IsContentLayerVisible()
        {
            bool retVal = false;

            try
            {
                if (Globals.Objects.MainNavigation.Resources["NavigationViewContentGridBorderBrush"] == Globals.Objects.MainNavigation.Resources["NavigationViewContentGridBorderBrush"] && Globals.Objects.MainNavigation.Resources["NavigationViewContentBackground"] == Globals.Objects.MainNavigation.Resources["NavigationViewContentGridBorderBrush"])
                {
                    retVal = false;
                }
                else
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
            }

            return retVal;
        }
    }
}

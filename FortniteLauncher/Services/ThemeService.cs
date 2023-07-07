using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
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
            if (!ShowBackground)
            {
                Globals.Objects.MainNavigation.Resources.Clear();
            }
            else
            {

            }
        }
    }
}

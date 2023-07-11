using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
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
        public static void ChangeNavigationBackgroundContentLayerVisibility(bool ShowContentBackgroundLayer)
        {
            try
            {
                if (ShowContentBackgroundLayer)
                {
                    //remove the border
                    Globals.Objects.MainNavigation.Resources.Remove("NavigationViewContentGridBorderBrush");
                    //remove the transparent background
                    Globals.Objects.MainNavigation.Resources.Remove("NavigationViewContentBackground");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("Operation failed! Error: " + ex.Message, "Fail!");
            }
        }

        public static bool IsContentBackgroundLayerVisible()
        {
            bool retVal = false;

            try
            {
                if (Globals.Objects.MainNavigation.Resources["NavigationViewContentGridBorderBrush"] != null)
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

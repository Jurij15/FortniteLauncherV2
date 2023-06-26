using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Services
{
    public class MainWindowHelper
    {
        public class NavigationResourceDictionary
        {
            // it does not work will figure it out later
            public static ResourceDictionary MainNavigationDictionary;
            public static ResourceDictionary MainNavigationDictionaryOriginal;
            public static void RemoveContentBackground() //make it look like windows settings
            {
                MainNavigationDictionary = MainNavigationDictionaryOriginal;
                Globals.Objects.MainNavigation.Resources = MainNavigationDictionary;
            }

            public static void AddContentBackground() //disable the windows settings look
            {
                Globals.Objects.MainNavigation.Resources.Clear();
            }
        }
    }
}

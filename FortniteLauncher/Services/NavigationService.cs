using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Services
{
    public enum NavigationPages
    {
        Home,
        Play, //select the build to play
        PrivateServer,//?
        About, //about the launcher //might just merge it into settings
        Settings, 
        //these are not displayed in navigation view, will be navigated to using breadcrumb
        SelectedBuildPage, //after build was selected, show launch options
        PlayerOptionsPage //idk, for stuff like username if using auth argument
    }
    public class NavigationService
    {
        public void Navigate()
        {

        }

        public static void ItemInvoked(NavigationView NavigationView, NavigationViewItemInvokedEventArgs args)
        {

        }
    }
}

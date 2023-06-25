using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinUIEx;

namespace FortniteLauncher
{
    public static class Globals
    {
        public static bool bIsFirstTimeRun = false;
        public static class Objects
        {
            public static WindowEx MainWindow;
            public static XamlRoot MainWindowXamlRoot;
            public static Frame MainFrame;
            public static BreadcrumbBar MainBreadcrumb;
            public static NavigationView MainNavigation;
        }

        public static string CurrentlySelectedBuildGUID { get; set; }

        public static HashSet<string> GalleryImages = new HashSet<string>();

        public static string PlayerUsername { get; set; }

        public static class FortniteStrings
        {
            public static string LocalAppData = Environment.GetEnvironmentVariable("LocalAppData");

            public static string Fortnite64ShippingExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping.exe";

            public static string Fortnite64ShippingBattleEyeExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_BE.exe";

            public static string Fortnite64ShippingEACExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_EAC.exe";

            public static string FortniteLauncherExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteLauncher.exe";

            public static string FortniteSplashImage = @"\FortniteGame\Content\Splash\Splash.bmp";

            public static string LaunchArguments = "-log -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -caldera={} -AUTH_LOGIN=player@projectreboot.dev -AUTH_PASSWORD=Rebooted -AUTH_TYPE=epic";

            public static string UserLaunchArguments = $"-skippatchcheck - epicportal -AUTH_TYPE=epic -epicapp=Fortnite -noeac -nobe -AUTH_LOGIN=HELLO@fortnite.com -AUTH_PASSWORD=unused -fltoken=7a848a93a74ba68876c36C1c -fromfl=none";
        }

        public static ObservableCollection<string> Breadcrumbs = new ObservableCollection<string>();
        public static void UpdateBreadcrumb()
        {
            Objects.MainBreadcrumb.ItemsSource = Breadcrumbs;
        }

        public static HashSet<string> SavedBuildsGuids = new HashSet<string>();
    }
}

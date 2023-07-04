using FortniteLauncher.Cores;
using FortniteLauncher.Managers;
using FortniteLauncher.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinUIEx;

namespace FortniteLauncher
{
    public static class Globals
    {
        public static int Theme { get; set; }
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

        public static string VersionString = "2.0-DEV";

        public static ElementSoundPlayerState SoundMode;

        public static string GetPlayerUsername()
        {
            return Config.PlayerAuthUsername;
        }
        public static string GetPlayerPassword()
        {
            return Config.PlayerAuthPassword;
        }

        public static void SetPlayerUsername(string NewUsername)
        {
            Config.PlayerAuthUsername = NewUsername;
        }

        public static void SetPlayerPassword(string NewPassword)
        {
            Config.PlayerAuthPassword = NewPassword;
        }

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

            public static string GetReadyLaunchArguments(string Username, string Password)
            {
                return "-log -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -caldera={}"+$" -AUTH_LOGIN={Username}@projectreboot.dev -AUTH_PASSWORD={Password} -AUTH_TYPE=epic";
            }
        }

        public static ObservableCollection<string> Breadcrumbs = new ObservableCollection<string>();
        public static void UpdateBreadcrumb()
        {
            Objects.MainBreadcrumb.ItemsSource = Breadcrumbs;
        }

        public static HashSet<string> SavedBuildsGuids = new HashSet<string>();

        public static async void ResetApp(bool bSendNotification)
        {
            Directory.Delete(Settings.RootSettingsDir, true);
            if (bSendNotification) { NotificationService.SendSimpleToast("FortniteLauncher was reset", "Restart was required to complete","", 1.9); }

            BuildsManager.ResetBuilds();

            RestartApp();
        }

        #region TemporaryVars
        #endregion

        public static async void RestartApp()
        {
            Process p = Process.Start("FortniteLauncher.exe");
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            Process.GetCurrentProcess().Kill();
        }
    }
}

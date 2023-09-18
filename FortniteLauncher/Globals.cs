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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinUIEx;

namespace FortniteLauncher
{
    public static class Globals
    {
        public static string RootSettingsDir = "Settings/";
        public static string RootBackupsDir = "Backups\\";
        public static string SettingsFile = RootSettingsDir + "settings.json";

        public static bool bIsFirstTimeRun = false;
        public static bool LEnableDebugConsole = false;

        public static SettingsJson Settings;

        public static class Objects
        {
            public static WindowEx MainWindow;
            public static XamlRoot MainWindowXamlRoot;
            public static Frame MainFrame;
            public static BreadcrumbBar MainBreadcrumb;
            public static NavigationView MainNavigation;

            public static Window m_window;
        }

        public static string CurrentlySelectedBuildGUID { get; set; }

        public static HashSet<string> GalleryImages = new HashSet<string>();

        public static string VersionString = "2.0-DEV";

        public static class FortniteStrings
        {
            public static string LocalAppData = Environment.GetEnvironmentVariable("LocalAppData");

            public static string Fortnite64ShippingExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping.exe";

            public static string Fortnite64ShippingBattleEyeExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_BE.exe";

            public static string Fortnite64ShippingEACExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_EAC.exe";

            public static string FortniteLauncherExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteLauncher.exe";

            public static string FortniteSplashImage = @"\FortniteGame\Content\Splash\Splash.bmp";

            public static string LaunchArguments = "-log -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -caldera={} -AUTH_LOGIN=player@epicgames.dev -AUTH_PASSWORD=Rebooted -AUTH_TYPE=epic";

            public static string UserLaunchArguments = $"-skippatchcheck - epicportal -AUTH_TYPE=epic -epicapp=Fortnite -noeac -nobe -AUTH_LOGIN=HELLO@fortnite.com -AUTH_PASSWORD=unused -fltoken=7a848a93a74ba68876c36C1c -fromfl=none";

            public static string GetReadyLaunchArguments(string Username, string Password)
            {
                return "-log -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -caldera={}"+$" -AUTH_LOGIN={Username}@epicgames.dev -AUTH_PASSWORD={Password} -AUTH_TYPE=epic";
            }
        }

        public static class DebugConsole
        {
            [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            private static extern int AllocConsole();

            public static void SetupConsole()
            {
                AllocConsole();
                Logger.Log(LogImportance.Info, LogSource.AppCore, "Console Enabled!");
            }

            [DllImport("kernel32.dll", EntryPoint = "FreeConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            private static extern int FreeConsole();

            public static void CloseConsole()
            {
                FreeConsole();
            }
        }

        public static HashSet<string> SavedBuildsGuids = new HashSet<string>();

        public static async void ResetApp(bool bSendNotification)
        {
            Directory.Delete(Globals.RootSettingsDir, true);
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

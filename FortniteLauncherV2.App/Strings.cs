using FortniteLauncherV2.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncherV2.Common
{
    public class Strings
    {
        public static string LocalAppData = Environment.GetEnvironmentVariable("LocalAppData");

        public static string GetConfigDirectory(bool isDebug)
        {
            if (isDebug)
            {
                return @"FortniteLauncherV2/";
            }
            else if (isDebug)
            {
                return LocalAppData + "/FortniteLauncherV2/";
            }

            return null;
        }

        public static string GetCraniumLocation(bool bDebug)
        {
            return GetConfigDirectory(bDebug) + "Cranium.dll";
        }

        public static string GetAuroraSSLLocation(bool bDebug)
        {
            return GetConfigDirectory(bDebug) + "AuroraNative.dll";
        }

        public static string Fortnite64ShippingExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping.exe";

        public static string Fortnite64ShippingBattleEyeExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_BE.exe";

        public static string Fortnite64ShippingEACExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_EAC.exe";

        public static string FortniteLauncherExecutablePath = @"\FortniteGame\Binaries\Win64\FortniteLauncher.exe";

        public static string FortniteSplashImage = @"\FortniteGame\Content\Splash\Splash.bmp";

        public static string BackgroundsDirectory = GetConfigDirectory(Config.bDebug) + @"Content\Images";

        public static string LaunchArguments = "-log -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -caldera={} -AUTH_LOGIN=player@projectreboot.dev -AUTH_PASSWORD=Rebooted -AUTH_TYPE=epic";

        public static string UserLaunchArguments = $"-skippatchcheck - epicportal -AUTH_TYPE=epic -epicapp=Fortnite -noeac -nobe -AUTH_LOGIN=HELLO@fortnite.com -AUTH_PASSWORD=unused -fltoken=7a848a93a74ba68876c36C1c -fromfl=none";

        public class FileLocations
        {
            public static string SavesFileLocations = GetConfigDirectory(true) + "Saves/BuildSaves";

            public static string ConfigSuspendBELocation = GetConfigDirectory(true) + "Config/suspendBE";

            public static string ConfigSuspendEACLocation = GetConfigDirectory(true) + "Config/suspendEAC";

            public static string ConfigUseCraniumLocation = GetConfigDirectory(true) + "Config/useCranium";

            public static string ConfigUsernameLocation = GetConfigDirectory(true) + "Config/username";
        }
    }
}

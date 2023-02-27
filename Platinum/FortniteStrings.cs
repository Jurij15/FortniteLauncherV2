﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum
{
    public class FortniteStrings
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
}

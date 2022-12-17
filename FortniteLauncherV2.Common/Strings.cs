using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncherV2.Common
{
    public class Strings
    {
        public static string Fortnite64ShippingExecutablePath = @"/FortniteGame/Binaries/Win64/FortniteClient-Win64-Shipping.exe";

        public static string Fortnite64ShippingBattleEyeExecutablePath = @"/FortniteGame/Binaries/Win64/FortniteClient-Win64-Shipping_BE.exe";

        public static string Fortnite64ShippingEACExecutablePath = @"/FortniteGame/Binaries/Win64/FortniteClient-Win64-Shipping_EAC.exe";

        public static string FortniteLauncherExecutablePath = @"/FortniteGame/Binaries/Win64/FortniteLauncher.exe";

        public static string FortniteSplashImage = "/FortniteGame/Content/Splash/Splash.bmp";

        public static string LaunchArguments = "-log -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -caldera={} -AUTH_LOGIN=player@projectreboot.dev -AUTH_PASSWORD=Rebooted -AUTH_TYPE=epic";

        public static string TestLaunchArguments = "-skippatchcheck -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=7ce411021b27b4343a44fdg8 -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQwN2IyZjQwZWJhYWQ4NTlhZDQiLCJnZW5lcmF0ZWQiOjE2Mzg3MTcyNzgsImNhbGRlcmFHdWlkIjoiMzgxMGI4NjMtMmE2NS00NDU3LTliNTgtNGRhYjNiNDgyYTg2IiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.VAWQB67RTxhiWOxx7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ \"-AUTH_LOGIN=[EPIC] ZenHost@projectreboot.dev\" -AUTH_PASSWORD=Rebooted -AUTH_TYPE=epic ";
    }
}

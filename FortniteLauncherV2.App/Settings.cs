using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FortniteLauncherV2.App
{
    public class Settings
    {
        public void Init()
        {

        }
    }

    public class SettingStrings
    {
        public static string LocalAppData = Environment.GetEnvironmentVariable("LocalAppData");

        public static string GetConfigDirectory()
        {
            if (Config.bDebug)
            {
                return "FortniteLauncherV2/";
            }
            else if (!Config.bDebug)
            {
                return LocalAppData + "/FortniteLauncherV2";
            }

            return null;
        }

        public static string CraniumString = GetConfigDirectory() + "craniumv2.dll";
    }
}

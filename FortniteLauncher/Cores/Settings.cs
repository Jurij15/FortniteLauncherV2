using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Capture;

namespace FortniteLauncher.Cores
{
    public class Settings
    {
        public static string RootSettingsDir = "Settings/";

        public static void GetSettings()
        {
            if (!Directory.Exists(RootSettingsDir))
            {
                CreateSettings();
                Globals.bIsFirstTimeRun = true;
            }
        }

        public static void CreateSettings()
        {
            Directory.CreateDirectory(RootSettingsDir);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Settings
{
    public class Locations
    {
        public static class Directories
        {
            public static string BaseDir = @"LauncherData/";
            public static string DLLDir = BaseDir + @"DLLs/";
            public static string SavesDir = BaseDir + @"Saves/";
            public static string BuildsSavesDir = SavesDir + @"Builds/";
        }
        public static string LaunchArgumentsSave = Directories.SavesDir + @"LaunchArguments";

        public static string AllSavedDLLsList = Directories.SavesDir + @"DLLSaves";

        public static string DefaultSSLBpassDLL = Directories.SavesDir + @"DefaultSSLBypassDLL";

        public static string SavedBuilds = Directories.SavesDir + @"SavedBuilds";
    }
}

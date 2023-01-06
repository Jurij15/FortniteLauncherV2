using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FortniteLauncherV2.App
{
    public class Config
    {
        public static bool bDebug = true;

        public static string FortniteGameEnginePath { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }

        public static bool bUseCranium = true;

        public static bool bSuspendBE = false;

        public static bool bSuspendEAC = false;

        public static bool ShowHomeScreenBackground = true; 
    }
}

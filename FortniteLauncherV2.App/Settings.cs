using FortniteLauncherV2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;

namespace FortniteLauncherV2.App
{
    public class Settings
    {
        public static void Init()
        {
            string suspendBe = File.ReadAllText(Strings.FileLocations.ConfigSuspendBELocation).ToString();
            string suspendEAC = File.ReadAllText(Strings.FileLocations.ConfigSuspendEACLocation).ToString();
            string useCranium = File.ReadAllText(Strings.FileLocations.ConfigUseCraniumLocation).ToString();
            string username = File.ReadAllText(Strings.FileLocations.ConfigUsernameLocation).ToString();

            if (suspendBe.Contains("true"))
            {
                Config.bSuspendBE = true;
            }
            if (suspendBe.Contains("false"))
            {
                Config.bSuspendBE = false;
            }
            if (suspendEAC.Contains("true"))
            {
                Config.bSuspendEAC = true;
            }
            if (suspendEAC.Contains("false"))
            {
                Config.bSuspendEAC = false;
            }
            if (useCranium.Contains("true"))
            {
                Config.bUseCranium = true;
            }
            if (useCranium.Contains("false"))
            {
                Config.bUseCranium = false;
            }
            if (username != null)
            {
                Config.Username = username;
            }
        }

        public static void suspendBE_ChangeSettingsValue(bool NewValue)
        {

        }
    }
}

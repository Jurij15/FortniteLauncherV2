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
            string username = File.ReadAllText(Strings.FileLocations.ConfigUsernameLocation).ToString();
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

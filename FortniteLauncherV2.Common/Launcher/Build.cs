using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncherV2.Common.Launcher
{
    public class Build
    {
        /// <summary>
        /// Checks if the path provided contains the required files for launching Fortnite
        /// </summary>
        /// <returns>True if it is valid, False if it is invalid</returns>
        public static bool IsPathValid(string Path)
        {
            if (File.Exists(Path+Strings.Fortnite64ShippingExecutablePath))
            {
                if (File.Exists(Path + Strings.Fortnite64ShippingBattleEyeExecutablePath))
                {
                    if (File.Exists(Path + Strings.Fortnite64ShippingEACExecutablePath))
                    {
                        if (File.Exists(Path + Strings.FortniteLauncherExecutablePath))
                        {
                            return true;
                        }
                        else if (!File.Exists(Path + Strings.FortniteLauncherExecutablePath))
                        {
                            return false;
                        }
                    }
                    else if (!File.Exists(Path + Strings.Fortnite64ShippingEACExecutablePath))
                    {
                        return false;
                    }
                }
                else if (!File.Exists(Path + Strings.Fortnite64ShippingBattleEyeExecutablePath))
                {
                    return false;
                }
            }
            else if (!File.Exists(Path + Strings.Fortnite64ShippingExecutablePath))
            {
                return false;
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FortniteLauncher.Helpers
{
    public class BuildsHelper
    {
        /// <summary>
        /// Checks if the path provided contains the required files for launching Fortnite
        /// </summary>
        /// <returns>True if it is valid, False if it is invalid</returns>
        /// 
        //THIS DOES NOT WORK IN SOME CASES, WILL FIX LATER 
        public static bool IsPathValid(string Path)
        {
            if (File.Exists(Path + Globals.FortniteStrings.Fortnite64ShippingExecutablePath))
            {
                if (File.Exists(Path + Globals.FortniteStrings.Fortnite64ShippingBattleEyeExecutablePath))
                {
                    if (File.Exists(Path + Globals.FortniteStrings.Fortnite64ShippingEACExecutablePath))
                    {
                        //the fortnite launcher isnt 100% needed, we can just ignore it (and it makes certain builds not get detected)
                        return true;
                    }
                    else if (!File.Exists(Path + Globals.FortniteStrings.Fortnite64ShippingEACExecutablePath))
                    {
                        return false;
                    }
                }
                else if (!File.Exists(Path + Globals.FortniteStrings.Fortnite64ShippingBattleEyeExecutablePath))
                {
                    return false;
                }
            }
            else if (!File.Exists(Path + Globals.FortniteStrings.Fortnite64ShippingExecutablePath))
            {
                return false;
            }

            return false;
        }

        public static string RemoveAllSpacesFromString(string ValidPath)
        {
            return ValidPath.Replace(" ", string.Empty);
        }
    }
}

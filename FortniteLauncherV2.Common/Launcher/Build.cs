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
                        //the fortnite launcher isnt 100% needed, we can just ignore it (and it makes certain builds not get detected)
                        return true;
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

        public static void SaveBuildToFile(string ValidPath)
        {
            List<string> Content = new List<string>(); //a list that will contain every current config
            foreach (var line in File.ReadAllLines(Strings.FileLocations.SavesFileLocations))
            {
                Content.Add(line); //add every existing line into the array
            }

            foreach(var line in Content) //check if the path already exists
            {
                if (line == ValidPath)
                {
                    return;
                }
                else if (line != ValidPath)
                {
                    continue;
                }
            }

            Content.Add(ValidPath); //add in a new path

            //there is probably an easier way to do this
            File.Delete(Strings.FileLocations.SavesFileLocations); //we just delete the existing file

            using (StreamWriter sw = File.CreateText(Strings.FileLocations.SavesFileLocations)) //now we write every path into the file we also create
            {
                foreach (var line in Content)
                {
                    sw.WriteLine(line);
                }
                sw.Close();
            }

            Content.Clear(); //do this incase if that arrray is gigantic
        }

        public static string[] GetAllSavedPaths()
        {
            return File.ReadAllLines(Strings.FileLocations.SavesFileLocations).ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Settings
{
    public class Settings
    {
        public static bool bIsSaveToRun()
        {
            bool RetValue = false;
            if (Directory.Exists(Locations.Directories.SavesDir))
            {
                RetValue = false;
            }
            else
            {
                RetValue= true;
            }

            return RetValue;
        }
        public static void SaveLaunchArguments(string Args)
        {
            File.Delete(Locations.LaunchArgumentsSave);
            using (StreamWriter sw = File.CreateText(Locations.LaunchArgumentsSave))
            {
                sw.WriteLine(Args);
                sw.Close();
            }
        }

        public static string GetLaunchArguments()
        {
            return File.ReadAllText(Locations.LaunchArgumentsSave);
        }
    }
}

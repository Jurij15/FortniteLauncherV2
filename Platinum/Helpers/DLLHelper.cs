using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Platinum.Helpers
{
    public class DLLHelper
    {
        public static List<string> SavedDLLsArray = new List<string>();

        public static void AddAllSavedDLLsToArray()
        {
            SavedDLLsArray.Clear();
            foreach (var dll in File.ReadAllLines(@"LauncherData/Saves/DLLSaves"))
            {
                SavedDLLsArray.Add(dll);
            }
        }

        public static void InitSavedDLLs()
        {
            File.Delete(@"LauncherData/Saves/DLLSaves");

            using (StreamWriter sw = File.CreateText(@"LauncherData/Saves/DLLSaves"))
            {
                foreach (var file in Directory.GetFiles(@"LauncherData/DLLs/"))
                {
                    sw.WriteLine(file);
                }

                sw.Close();
            }
        }

        public static void SetDefaultSSLBypassDLL(string FilePath)
        {
            File.Delete(@"LauncherData/Saves/DLLSaves");

            using (StreamWriter sw = File.CreateText(@"LauncherData/Saves/DefaultSSLBypassDLL"))
            {

                sw.WriteLine(FilePath);
                sw.Close();
            }
        }

        public static string GetDefaultSSLBypassDLL()
        {
            return File.ReadLines(@"LauncherData/Saves/DefaultSSLBypassDLL").First();
        }
    }
}

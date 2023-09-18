using FortniteLauncher.Cores;
using FortniteLauncher.Enums;
using FortniteLauncher.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Managers
{
    public class DLLibraryManager
    {
        public static string RootBuildsDir = Globals.RootSettingsDir + "DLLibrary";

        public string GetDLLNameConfig(string GUID)
        {
            return RootBuildsDir + "\\" + GUID + "\\DLLNameConfig";
        }

        public string GetDLLPathConfig(string GUID)
        {
            return RootBuildsDir + "\\" + GUID + "\\DLLPathConfig";
        }

        //saves a build
        public async Task<string> AddDllToLib(string Name, string Path)
        {
            if (!Directory.Exists(RootBuildsDir))
            {
                Directory.CreateDirectory(RootBuildsDir);
            }

            string guid = Guid.NewGuid().ToString();
            string dir = RootBuildsDir + "\\" + guid;
            Directory.CreateDirectory(dir);

            using (StreamWriter sw = File.CreateText(dir + "\\DLLNameConfig"))
            {
                await sw.WriteAsync(Name);
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText(dir + "\\DLLPathConfig"))
            {
                await sw.WriteAsync(Path);
                sw.Close();
            }

            return guid;
        }

        public HashSet<string> GetAllDLLGuids()
        {
            if (!Directory.Exists(RootBuildsDir)) //on first time run, create the dir if not existing yet
            {
                Directory.CreateDirectory(RootBuildsDir);
            }
            var list = new HashSet<string>();
            foreach (var item in Directory.GetDirectories(RootBuildsDir))
            {
                list.Add(Path.GetFileName(item));
            }

            return list;
        }

        //builds lists /*For search ONLY*/
        public List<string> GetAllDLLGUIDsThatNameContains(string ContainThisInName)
        {
            List<string> list = new List<string>();

            foreach (var item in GetAllDLLGuids())
            {
                string name = GetDLLNameByGUID(item);
                if (name.Contains(ContainThisInName, StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(item);
                }
            }

            return list;
        }
        //get build properties
        public string GetDLLNameByGUID(string GUID)
        {
            return File.ReadAllText(GetDLLNameConfig(GUID));
        }
        public string GetDLLPathByGUID(string GUID)
        {
            return File.ReadAllText(GetDLLPathConfig(GUID));
        }
        public void SaveNewDLLNameConfigToGuid(string guid, string NewBuildName)
        {
            File.Delete(GetDLLNameConfig(guid));
            using (StreamWriter sw = File.CreateText(GetDLLNameConfig(guid)))
            {
                sw.Write(NewBuildName);
                sw.Close();
            }
        }
        public void SaveNewDLLPathConfigToGuid(string guid, string NewBuildPath)
        {
            File.Delete(GetDLLPathConfig(guid));
            using (StreamWriter sw = File.CreateText(GetDLLPathConfig(guid)))
            {
                sw.Write(NewBuildPath);
                sw.Close();
            }
        }

        //delete builds
        public void DeleteDLL(string guid)
        {
            string dir = RootBuildsDir + "\\" + guid;
            Directory.Delete(dir, true);
        }

        public static void ResetDLLLibrary()
        {
            Directory.Delete(RootBuildsDir, true);
        }
    }
}

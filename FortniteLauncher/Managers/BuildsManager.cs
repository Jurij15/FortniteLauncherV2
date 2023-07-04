using FortniteLauncher.Cores;
using FortniteLauncher.Enums;
using FortniteLauncher.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Managers
{
    public class BuildsManager
    {
        public static string RootBuildsDir = Settings.RootSettingsDir + "Builds";

        public string GetBuildNameConfig(string GUID)
        {
            return RootBuildsDir + "\\" + GUID + "\\BuildNameConfig";
        }

        public static string STATIC_GetBuildNameConfig(string GUID)
        {
            return RootBuildsDir + "\\" + GUID + "\\BuildNameConfig";
        }

        public string GetBuildPathConfig(string GUID)
        {
            return RootBuildsDir + "\\" + GUID + "\\BuildPathConfig";
        }

        public string GetBuildSeasonConfig(string GUID)
        {
            return RootBuildsDir + "\\" + GUID + "\\BuildSeasonConfig";
        }

        //saves a build
        public async Task<string> CreateBuild(string Name, string Path, FortniteSeasons Season, bool bIgnoreInvalidPath)
        {
            if (!BuildsHelper.IsPathValid(Path) && !bIgnoreInvalidPath)
            {
                return null;
            }

            if (!Directory.Exists(RootBuildsDir))
            {
                Directory.CreateDirectory(RootBuildsDir);
            }

            string guid = Guid.NewGuid().ToString();
            string dir = RootBuildsDir + "\\" + guid;
            Directory.CreateDirectory(dir);

            using (StreamWriter sw = File.CreateText(dir+ "\\BuildNameConfig"))
            {
                await sw.WriteAsync(Name);
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText(dir + "\\BuildPathConfig"))
            {
                await sw.WriteAsync(Path);
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText(dir + "\\BuildSeasonConfig"))
            {
                var value = Season;
                await sw.WriteAsync(value.ToString());
                sw.Close();
            }

            return guid;
        }

        public HashSet<string > GetAllBuildGuids()
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
        public List<string> GetAllBuildGUIDsThatNameContains(string ContainThisInName)
        {
            List<string > list = new List<string>();

            foreach (var item in GetAllBuildGuids())
            {
                string name = GetBuildNameByGUID(item);
                if (name.Contains(ContainThisInName))
                {
                    list.Add(item);
                }
            }

            return list;
        }
        //get build properties
        public string GetBuildNameByGUID(string GUID)
        {
            return File.ReadAllText(GetBuildNameConfig(GUID));
        }
        public static string STATIC_GetBuildNameByGUID(string GUID)
        {
            return File.ReadAllText(STATIC_GetBuildNameConfig(GUID));
        }
        public string GetBuildPathByGUID(string GUID)
        {
            return File.ReadAllText(GetBuildPathConfig(GUID));
        }
        public string GetBuildSeasonByGUID(string GUID)
        {
            return File.ReadAllText(GetBuildSeasonConfig(GUID));
        }

        public FortniteSeasons GetBuildSeasonEnumByGUID(string GUID)
        {
            return (FortniteSeasons)Enum.Parse(typeof(FortniteSeasons), GetBuildSeasonByGUID(GUID));
        }

        //editing a build (will do later)
        public void SaveNewBuildNameConfigToGuid(string guid, string NewBuildName)
        {
            File.Delete(GetBuildNameConfig(guid));
            using (StreamWriter sw = File.CreateText(GetBuildNameConfig(guid)))
            {
                sw.Write(NewBuildName);
                sw.Close();
            }
        }
        public void SaveNewBuildPathConfigToGuid(string guid, string NewBuildPath)
        {
            File.Delete(GetBuildPathConfig(guid));
            using (StreamWriter sw = File.CreateText(GetBuildPathConfig(guid)))
            {
                sw.Write(NewBuildPath);
                sw.Close();
            }
        }
        public void SaveNewBuildSeasonConfigToGuid(string guid, string NewBuildSeason)
        {
            File.Delete(GetBuildSeasonConfig(guid));
            using (StreamWriter sw = File.CreateText(GetBuildSeasonConfig(guid)))
            {
                sw.Write(NewBuildSeason);
                sw.Close();
            }
        }

        //delete builds
        public void DeleteBuild(string guid)
        {
            string dir = RootBuildsDir + "\\" + guid;
            Directory.Delete(dir, true);
        }

        public static void ResetBuilds()
        {
            Directory.Delete(RootBuildsDir, true);
        }
    }
}

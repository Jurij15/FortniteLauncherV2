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
    public class BuildsManager
    {
        public static string RootBuildsDir = Settings.RootSettingsDir + "Builds/";

        public static string GetBuildNameConfig(string GUID)
        {
            return RootBuildsDir + "\\" + GUID + "\\BuildNameConfig";
        }

        public static string GetBuildPathConfig(string GUID)
        {
            return RootBuildsDir + "\\" + GUID + "\\BuildPathConfig";
        }

        public static string GetBuildSeasonConfig(string GUID)
        {
            return RootBuildsDir + "\\" + GUID + "\\BuildSeasonConfig";
        }

        //saves a build
        public async Task<string> CreateBuild(string Name, string Path, FortniteSeasons Season)
        {
            if (!BuildsHelper.IsPathValid(Path))
            {
                return null;
            }
            Directory.CreateDirectory(RootBuildsDir);

            string guid = Guid.NewGuid().ToString();
            string dir = RootBuildsDir + "/" + guid;
            Directory.CreateDirectory(dir);

            using (StreamWriter sw = File.CreateText(dir+ "BuildNameConfig"))
            {
                await sw.WriteAsync(Name);
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText(dir + "BuildPathConfig"))
            {
                await sw.WriteAsync(Path);
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText(dir + "BuildSeasonConfig"))
            {
                await sw.WriteAsync(Season.ToString());
                sw.Close();
            }

            return guid;
        }
    }
}

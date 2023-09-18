using FortniteLauncher.Json;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;
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
        public static string RootBuildsDir = Globals.RootSettingsDir + "Builds\\";

        private void CreateDirectory()
        {
            if (!Directory.Exists(RootBuildsDir))
            {
                Directory.CreateDirectory(RootBuildsDir);
            }
        }

        /// <summary>
        /// Saves a new build to disk
        /// </summary>
        /// <param name="Json">Json Class</param>
        /// <returns>Guid Of the build</returns>
        public string CreateBuildConfig(BuildJson Json)
        {
            string guid = Guid.NewGuid().ToString();

            Json.Guid = guid;
            var serializedjson = JsonConvert.SerializeObject(Json);

            using (var fileStream = new FileStream(Path.Combine(RootBuildsDir, guid + "json"), FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.Write(serializedjson);
                }
            }

            return guid;
        }

        /// <summary>
        /// Gets the BuildJson class for the requested build from disk
        /// </summary>
        /// <param name="GUID">GUID of the requested build</param>
        /// <returns>BuildJson Class with properties</returns>
        public BuildJson GetBuildJsonWithGuid(string GUID)
        {
            BuildJson Return = null;

            CreateDirectory();//create the directory just in case

            foreach (var file in Directory.GetFiles(RootBuildsDir))
            {
                if (Path.GetFileNameWithoutExtension(file) == GUID)
                {
                    Return = JsonConvert.DeserializeObject<BuildJson>(File.ReadAllText(file));
                    break;
                }
            }

            return Return;
        }

        /// <summary>
        /// Modifies a build saved on disk
        /// </summary>
        /// <param name="BuildJson">Modified BuildJson</param>
        /// <returns>True if sucessfull, false if failed</returns>
        public bool ModifyBuildJson(BuildJson BuildJson)
        {
            bool ReturnValue = false;

            try
            {
                string GUID = BuildJson.Guid;

                if (File.Exists(RootBuildsDir + GUID + ".json"))
                {
                    File.Delete(RootBuildsDir + GUID + ".json");
                }

                var serializedjson = JsonConvert.SerializeObject(BuildJson);

                using (var fileStream = new FileStream(Path.Combine(RootBuildsDir, GUID + "json"), FileMode.Create, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(fileStream))
                    {
                        writer.Write(serializedjson);
                        writer.Close();
                    }
                    fileStream.Close();
                    ReturnValue = true;
                }
            }
            catch (Exception ex)
            {
                return ReturnValue;
            }

            return ReturnValue;
        }

        public string[] GetAllGuids()
        {
            var guids = new List<string>();

            foreach (var file in Directory.GetFiles(RootBuildsDir))
            {
                guids.Add(Path.GetFileNameWithoutExtension(file));
            }

            return guids.ToArray();
        }

        public bool DeleteBuild(string GUID)
        {
            bool ReturnValue = false;

            try
            {
                File.Delete(Path.Combine(RootBuildsDir, GUID + "json"));
                ReturnValue = true;
            }
            catch (Exception ex)
            { 
            }

            return ReturnValue;
        }

        public static void ResetBuilds()
        {
            Directory.Delete(RootBuildsDir, true);
        }
    }
}

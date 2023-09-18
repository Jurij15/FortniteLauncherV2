using Microsoft.UI.Xaml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Cores
{
    public class SettingsJson
    {
        public ElementTheme Theme = ElementTheme.Default;
        public bool ShowContentBackgroundLayer = false;

        public string PlayerAuthUsername = "Player123456";
        public string PlayerAuthPassword = "NoPassword";
        public bool ShowIfAnotherInstanceRunningDialog = true;

        public string SSLBypassDLL = "Assets\\DLL\\cobalt.dll";
        public string ConsoleDLL = "Assets\\DLL\\console.dll";
        public string MemoryLeakFixDLL = "Assets\\DLL\\memoryleak.dll";
        public ElementSoundPlayerState SoundMode;

        //launch SettingsJsons
        public bool EnableSSLBypass = true;

        public bool SuspendBE = false;
        public bool SuspendEAC = false;
        public bool FixMemoryLeak = true;

        public string LaunchArguments= Globals.FortniteStrings.LaunchArguments;

        public static void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(Globals.Settings);

            if (!Directory.Exists(Globals.RootSettingsDir))
            {
                Globals.bIsFirstTimeRun = true;
                Directory.CreateDirectory(Globals.RootSettingsDir);
            }

            using (var fileStream = new FileStream(Globals.SettingsFile, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.Write(json);
                }
            }
        }

        public static void GetSettings()
        {
            try
            {
                if (File.Exists(Globals.SettingsFile))
                {
                    using (var fileStream = new FileStream(Globals.SettingsFile, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new StreamReader(fileStream))
                        {
                            string json = reader.ReadToEnd();
                            SettingsJson config = JsonConvert.DeserializeObject<SettingsJson>(json);
                            Globals.Settings = config;
                        }
                    }
                }
                else
                {
                    Globals.Settings = new SettingsJson();
                    SaveSettings();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

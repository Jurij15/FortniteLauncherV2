using FortniteLauncher.Cores;
using FortniteLauncher.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;

namespace FortniteLauncher.Helpers
{
    public class LawinServerHelper
    {
        private static int LawinServerProcessiD = 0;
        private static bool bStarted = false;

        public static string LawinServerDir = Settings.RootSettingsDir + "\\LawinServer";
        public static string LawinServerWorkingDir = LawinServerDir + "\\LawinServer-main";
        public static string LawinServerTemp = Settings.RootSettingsDir + "Temp";
        public static string LawinServerGitPath = "https://github.com/Lawin0129/LawinServer/archive/refs/heads/main.zip";
        public static bool IsLawinServerInstalled()
        {
            bool RetVal = false;

            if (Directory.Exists(LawinServerDir))
            {
                RetVal = true;
            }

            return RetVal;
        }

        public static bool IsLawinServerRunning()
        {
            bool RetVal = false;

            if (LawinServerProcessiD != 0)
            {
                RetVal = true;
            }   

            return RetVal;
        }

        public static void InstallLawinServer()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var s = client.GetStreamAsync(LawinServerGitPath))
                    {
                        using (var fs = new FileStream(LawinServerTemp, FileMode.OpenOrCreate))
                        {
                            s.Result.CopyTo(fs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("Unable to download update. Error message: " + ex.Message, "Download Failed");
                throw;
            }

            ZipFile.ExtractToDirectory(LawinServerTemp, LawinServerDir, true);
            System.IO.File.Delete(LawinServerTemp);
        }

        public static void UninstallLawinServer()
        {
            Directory.Delete(LawinServerDir, true);
        }

        private static void Start()
        {
            if (bStarted)
            {
                return;
            }
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Verb = "runas";
            p.StartInfo.Arguments = "/C cd Settings/LawinServer/LawinServer-main && start.bat";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();

            LawinServerProcessiD = p.Id;
            bStarted = true;

            DialogService.ShowSimpleDialog("lawinServer started minimized", "");
        }

        public static void Stop()
        {
            Process.GetProcessById(LawinServerProcessiD).Kill();
            bStarted = false; 
            LawinServerProcessiD = 0;
        }

        public static void TryInstallNodePackagesAndStart()
        {
            if (Directory.Exists(LawinServerWorkingDir + "\\node_modules"))
            {
                Start();
                return;//packages are already installed
            }
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Verb = "runas";
            p.StartInfo.Arguments = "/C cd Settings/LawinServer/LawinServer-main && install_packages.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            p.WaitForExit();

            Start();
        }
    }
}

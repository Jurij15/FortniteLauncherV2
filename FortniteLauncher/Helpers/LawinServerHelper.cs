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

        public static bool IsLawinServerRunning() //todo
        {
            //https://stackoverflow.com/questions/570098/in-c-how-to-check-if-a-tcp-port-is-available
            bool RetVal = false;

            int port = 456; //<--- This is your value

            // Evaluate current system tcp connections. This is the same information provided
            // by the netstat command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will set isAvailable to false.
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == port)
                {
                    RetVal = false;
                    break;
                }
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

        public static void Start()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Verb = "runas";
            p.StartInfo.Arguments = "/C cd Settings/LawinServer/LawinServer-main && start.bat";
            p.Start();
        }

        public static void Stop()
        {
            foreach (var item in Process.GetProcesses())
            {
                if (item.ProcessName.Contains("Node.js"))
                {
                    item.Kill();
                    break;
                }
            }
        }

        public static void TryInstallNodePackages()
        {
            if (Directory.Exists(LawinServerWorkingDir + "\\node_modules"))
            {
                return;//packages are already installed
            }
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Verb = "runas";
            p.StartInfo.Arguments = "/C cd Settings/LawinServer/LawinServer-main && install_packages.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
        }
    }
}

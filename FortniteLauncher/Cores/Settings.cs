using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.NumberFormatting;
using Windows.Graphics.Capture;

namespace FortniteLauncher.Cores
{
    public class Settings
    {
        public static string RootSettingsDir = "Settings/";

        public static string ThemeConfig = RootSettingsDir + "ThemeConfig";
        public static string AuthUsernameConfig = RootSettingsDir + "AuthUsername";
        public static string AuthPasswordConfig = RootSettingsDir + "AuthPassword";
        public static string ShowIfAnotherInstanceRunningDialogConfig = "ShowIfAnotherInstanceRunningDialog";

        public static string SSLBypassDLLConfig = RootSettingsDir + "SSLBypassDLLConfig";
        public static string ConsoleDLLConfig = RootSettingsDir + "ConsoleDLLConfig";
        public static string MemoryLeakDLLConfig = RootSettingsDir + "MemoryLeakDLLConfig";

        public static void GetSettings()
        {
            if (!Directory.Exists(RootSettingsDir))
            {
                CreateSettings();
                Globals.bIsFirstTimeRun = true;
            }

            string theme = File.ReadAllText(ThemeConfig);
            //Globals.Theme = Convert.ToInt32(theme);

            string username = File.ReadAllText(AuthUsernameConfig);
            Globals.SetPlayerUsername(username);

            string password = File.ReadAllText(AuthPasswordConfig);
            Globals.SetPlayerPassword(password);

            string instancedialogbool = File.ReadAllText(ShowIfAnotherInstanceRunningDialogConfig);
            Config.ShowIfAnotherInstanceRunningDialog = Convert.ToBoolean(Convert.ToInt32(instancedialogbool));


            string sslconfig = File.ReadAllText(SSLBypassDLLConfig);
            Config.SSLBypassDLL = sslconfig;

            string consoleconfig = File.ReadAllText(ConsoleDLLConfig);
            Config.ConsoleDLL = sslconfig;

            string memoryconfig = File.ReadAllText(MemoryLeakDLLConfig);
            Config.MemoryLeakFixDLL = memoryconfig;
        }

        public static void CreateSettings()
        {
            Directory.CreateDirectory(RootSettingsDir);

            //default values
            using (StreamWriter sw = File.CreateText(ThemeConfig))
            {
                sw.Write("0");
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText(AuthUsernameConfig))
            {
                sw.Write("ProGamer"+ new Random().Next(100).ToString());
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText(AuthPasswordConfig))
            {
                sw.Write("IdkWhatToPutHere");
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText(ShowIfAnotherInstanceRunningDialogConfig))
            {
                sw.Write("1");
                sw.Close();
            }

            using (StreamWriter sw = File.CreateText(SSLBypassDLLConfig))
            {
                sw.Write("Assets/DLL/cobalt.dll");
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText(ConsoleDLLConfig))
            {
                sw.Write("Assets/DLL/console.dll");
                sw.Close();
            }
            using (StreamWriter sw = File.CreateText(MemoryLeakDLLConfig))
            {
                sw.Write("Assets/DLL/memoryleak.dll");
                sw.Close();
            }
        }

        public static void SaveNewThemeConfig(int NewTheme)
        {
            File.Delete(ThemeConfig);
            using (StreamWriter sw = File.CreateText(ThemeConfig))
            {
                sw.Write(NewTheme.ToString());
                sw.Close();
            }
        }

        public static void SaveNewAuthUsernameConfig(string NewUsername)
        {
            File.Delete(AuthUsernameConfig);
            using (StreamWriter sw = File.CreateText(AuthUsernameConfig))
            {
                sw.Write(NewUsername);
                sw.Close();
            }
        }

        public static void SaveNewAuthPasswordConfig(string NewPassword)
        {
            File.Delete(AuthPasswordConfig);
            using (StreamWriter sw = File.CreateText(AuthPasswordConfig))
            {
                sw.Write(NewPassword);
                sw.Close();
            }
        }

        public static void SaveNewSSLBypassDLLConfigConfig(string NewPath)
        {
            File.Delete(SSLBypassDLLConfig);
            using (StreamWriter sw = File.CreateText(SSLBypassDLLConfig))
            {
                sw.Write(NewPath);
                sw.Close();
            }
        }

        public static void SaveNewConsoleDLLConfigConfig(string NewPath)
        {
            File.Delete(ConsoleDLLConfig);
            using (StreamWriter sw = File.CreateText(ConsoleDLLConfig))
            {
                sw.Write(NewPath);
                sw.Close();
            }
        }

        public static void SaveNewMemoryLeakFixDLLConfigConfig(string NewPath)
        {
            File.Delete(MemoryLeakDLLConfig);
            using (StreamWriter sw = File.CreateText(MemoryLeakDLLConfig))
            {
                sw.Write(NewPath);
                sw.Close();
            }
        }
    }
}

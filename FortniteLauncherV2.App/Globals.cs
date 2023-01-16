using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Mvvm.Contracts;

namespace FortniteLauncherV2.App
{
    public class Globals
    {
        public static Wpf.Ui.Controls.Dialog bRootDialog;
        public static Wpf.Ui.Controls.NavigationStore NavStore;
        public static Wpf.Ui.Controls.Snackbar RootSnackbar;

        public static Process ?FortniteProcess;

        public static List<string> LaunchedBuilds= new List<string>();

        public static void AddBuildToList(int PID)
        {
            LaunchedBuilds.Add("PID"+PID.ToString());
        }

        public static void RemoveBuildFromList(int PID)
        {
            List<string> TempList = new List<string>();

            TempList = LaunchedBuilds.ToList();

            foreach (string Temp in TempList)
            {
                if (Temp.Contains(PID.ToString()))
                {
                    LaunchedBuilds.Remove(Temp);
                }
                else
                {
                    continue;
                }
            }

            TempList.Clear();
        }

        public static void MakeBuildOnListGameServer(int PID)
        {
            List<string> TempList = new List<string>();

            TempList = LaunchedBuilds.ToList();

            foreach (string Temp in TempList)
            {
                if (Temp.Contains(PID.ToString()))
                {
                    string OldString = Temp;
                    string NewString = OldString + "[GAMESERVER]";
                    RemoveBuildFromList(PID);
                    LaunchedBuilds.Add(NewString);
                }
                else
                {
                    continue;
                }
            }

            TempList.Clear();
        }

        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        public static void SetupConsole()
        {
            AllocConsole();
        }
    }
}

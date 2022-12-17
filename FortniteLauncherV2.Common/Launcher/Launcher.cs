using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FortniteLauncherV2.Common.Launcher
{
    public class Launcher
    {
        /// <summary>
        /// Launches Fortnite Without any arguments
        /// </summary>
        /// <param name="ValidPath"></param>
        public static void LaunchFortnite(string ValidPath)
        {
            Process p = new Process();
            p.StartInfo.FileName = ValidPath + Strings.Fortnite64ShippingExecutablePath;
            p.Start();
        }

        public static void LaunchFortniteWithArguments(string ValidPath, string Arguments)
        {
            Process p = new Process();
            p.StartInfo.FileName = ValidPath + Strings.Fortnite64ShippingExecutablePath;
            p.StartInfo.Arguments= Arguments;
            p.Start();
        }

        public static void LaunchFortniteWithArugmentsAndCraniumSLLBypass(string ValidPath, string Arguments, string CraniumDLLPath)
        {
            Process p = new Process();
            p.StartInfo.FileName = ValidPath + Strings.Fortnite64ShippingExecutablePath;
            p.StartInfo.Arguments= Arguments;
            p.Start();

            Thread.Sleep(10000);
            Injector.InjectDll(p.Id, CraniumDLLPath);
        }
    }
}

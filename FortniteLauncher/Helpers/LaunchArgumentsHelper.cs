using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Helpers
{
    public class LaunchArgumentsHelper
    {
        public static List<string> AllCurrentArguments = new List<string>();

        public static void Parse()
        {
            string[] args = Environment.GetCommandLineArgs();

            foreach (var item in args)
            {
                AllCurrentArguments.Add(item);

                if (item.Contains("EnableDbgConsole"))
                {
                    Globals.LEnableDebugConsole = true;
                }
            }
        }
    }
}

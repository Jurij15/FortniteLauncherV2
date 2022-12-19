using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncherV2.Common.Launcher
{
    public class ProcessHelper
    {
        public static bool IsProcessRunning(int ProcessID)
        {
            if (ProcessID == 0) return false;
            foreach (var Process in Process.GetProcesses())
            {
                if (Process.Id == ProcessID)
                {
                    return true;
                }
                else if (Process.Id != ProcessID)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Helpers
{
    public class ProcessHelper
    {
        public static List<Process> GetAllFortniteProcessesPID()
        {
            List<Process> pids = new List<Process>();    
            foreach (var item in Process.GetProcesses())
            {
                if (item.MainWindowTitle.Contains("Fortnite"))
                {
                    pids.Add(item);
                }
            }

            return pids;
        }
    }
}

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
        public static List<int> GetAllFortniteProcessesPID()
        {
            List<int> pids = new List<int>();    
            foreach (var item in Process.GetProcesses())
            {
                if (item.MainWindowTitle.Contains("Fortnite") && item.MainWindowTitle != "FortniteLauncher")
                {
                    pids.Add(item.Id);
                }
            }

            return pids;
        }
    }
}

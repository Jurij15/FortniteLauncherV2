using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Cores
{
    public class PlayCore
    {
        private string LanuchErrors;
        public bool bLaunchStatus = false;
        public string GetLaunchErrorsIfAny()
        {
            return LanuchErrors;
        }

        public Process LaunchFortnite(string ShippingExePath ,string LaunchArguments, string BEExePath, string EACExePath, bool bSuspendBE, bool bSuspendEAC)
        {
            Process p = new Process();
            
            p.StartInfo.FileName = ShippingExePath;
            p.StartInfo.Arguments = LaunchArguments;

            try
            {
                p.Start();
                bLaunchStatus = true;
            }
            catch (Exception ex)
            {
                bLaunchStatus = false;
                LanuchErrors = ex.Message;
                throw;
            }

            return p;
        }
    }
}

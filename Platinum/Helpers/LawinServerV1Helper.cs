using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Platinum.Helpers
{
    public class LawinServerV1Helper
    {
        public static bool bIsLawinServerV1Installed()
        {
            bool RetValue = false;
            if (Directory.Exists("LauncherData/Server/LawinServerV1/"))
            {
                RetValue = true;
            }
            else
            {
                RetValue= false;
            }

            return RetValue;
        }

        public static bool bIsLawinServerV1CurrentlyRunning()
        {
            bool RetValue = false;

            //https://softwarebydefault.com/2013/02/22/port-in-use/
            IPGlobalProperties ipProps = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] iPEndPoints = ipProps.GetActiveTcpListeners();

            foreach (IPEndPoint endpoint in iPEndPoints)
            {
                if (endpoint.Port == 3551) //lawinserver v1 uses port 3551 by default
                {
                    RetValue = true ; 
                    break;
                }
            }

            return RetValue;
        }
    }
}

using Platinum.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum
{
    public class OnStartup
    {
        public static void DoOnStartup()
        {
            //DLLHelper.InitSavedDLLs();
            Settings.Settings.bIsSaveToRun();
        }
    }
}

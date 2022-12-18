using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncherV2.Common.Launcher
{
    public class Patcher
    {
        public static void PatchSSL(int FNProcessPID, string SSlybpassDLLLocation, Process FnProcess)
        {
            var SS = new SigScan(SigScan.OpenProcess(SigScan.PROCESS_ALL_ACCESS, false, FNProcessPID));
            var s69Addr = SS.FindPattern("41 39 28 0F 95 C0 88 83 50 04 00 00", out long Time); // it crashes right here, select module is null, will try fixing later
            var s910Addr = SS.FindPattern("41 39 28 0F 95 C0 88 83 50 04 00 00", out long TTime);
            if (s69Addr ==0)
            {
                //in this case, it probably isnt s6-9.10, just try 9-10
                if (s910Addr == 0)
                {
                    //this is probably something else, just inject the dll

                }
                else if (s910Addr != 0)
                {
                    //its probably 9.40 or sx, patch it
                }
            }
            else if (s69Addr != 0)
            {
                // it is something in between s6 - 9.10, try to patch it
                PatchSSL69(FnProcess);
            }
        }

        public static void PatchSSL69(Process FNProcess) //patch ssl for season 6 - 9 (9.10)
        {
            var SS = new SigScan(SigScan.OpenProcess(SigScan.PROCESS_ALL_ACCESS, false, FNProcess.Id));
            var Addr = SS.FindPattern("41 39 28 0F 95 C0 88 83 50 04 00 00", out long Time); //lets do it again
            SigScan.WriteProcessMemory(SigScan.OpenProcess(SigScan.PROCESS_ALL_ACCESS, false, FNProcess.Id), (IntPtr)Addr, Patches._verifyPeerPatched69, Patches._verifyPeerPatched69.Length, out IntPtr bytesWritten);
            //maybe log it here
            NewWin32.ResumeProcess(FNProcess.Id); //always resume process
        }

        public static void PatchSSL910(Process fnProcess) //patch ssl for season 9 maybe 10
        {

        }

        public static void InjectSSLBypass(Process fnProcess, string SSLBypassDllLocation) //we assume that this is either before s6 when no patching is needed (as much as i know) or its a build above s9/x, just inject the dll and hope for the best
        {
            Injector.InjectDll(fnProcess.Id, SSLBypassDllLocation);
            //NewWin32.ResumeProcess(fnProcess.Id);
        }
    }
}

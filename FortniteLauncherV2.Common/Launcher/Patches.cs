using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncherV2.Common.Launcher
{
    public class Patches
    {
        public static readonly byte[] _verifyPeerPatched69 = new byte[] //s6 - 9.10
        {
            65,
            57,
            40,
            176,
            0,
            144,
            136,
            131,
            80,
            4,
            0,
            0
        };
    }
}

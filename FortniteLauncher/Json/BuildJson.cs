using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.EnterpriseData;

namespace FortniteLauncher.Json
{
    public class BuildJson
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Season { get; set; }

        //do not modify this property
        public string Guid { get; set; }
    }
}

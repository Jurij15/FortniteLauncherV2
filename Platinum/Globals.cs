using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Navigation;

namespace Platinum
{
    public class Globals
    {
        public static MainWindow MWindow;
        public static NavigationView GNavigation;

        public static string CurrentlySelectedSSLBypassDLL { get ; set; }
        public static string CurrentlySelectedBuildPath { get ; set; }
        public static string UserName { get ; set; }
        public static string CurrentLaunchArgunments { get ; set; }

        public static string CurrentlySelectedBuild { get; set; }

        public static List<string> AllSavedBuilds = new List<string>();
    }
}

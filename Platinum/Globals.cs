using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace Platinum
{
    public class Globals
    {
        public static MainWindow MWindow;
        public static NavigationCompact GNavigation;
        public static Frame GFrame;

        public static string CurrentlySelectedSSLBypassDLL { get ; set; }
        public static string CurrentlySelectedBuildPath { get ; set; }
        public static string UserName { get ; set; }
        public static string CurrentLaunchArgunments { get ; set; }

        public static string CurrentlySelectedBuild { get; set; }

        public static List<string> AllSavedBuilds = new List<string>();
    }
}

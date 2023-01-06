using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Mvvm.Contracts;

namespace FortniteLauncherV2.App
{
    public class Globals
    {
        public static Wpf.Ui.Controls.Dialog bRootDialog;
        public static Wpf.Ui.Controls.NavigationStore NavStore;
        public static Wpf.Ui.Controls.Snackbar RootSnackbar;

        public static Process ?FortniteProcess;
    }
}

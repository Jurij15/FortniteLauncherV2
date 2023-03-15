using Platinum.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Platinum.Pages
{
    /// <summary>
    /// Interaction logic for ServerPage.xaml
    /// </summary>
    public partial class ServerPage : Wpf.Ui.Controls.UiPage
    {
        public ServerPage()
        {
            InitializeComponent();
            OnStartup();
        }

        void OnStartup()
        {
            bool bRunning = LawinServerV1Helper.bIsLawinServerV1CurrentlyRunning();
            bool bInstalled = LawinServerV1Helper.bIsLawinServerV1Installed();

            if (!bInstalled)
            {
                LawinServerStatusBadge.Appearance = Wpf.Ui.Common.ControlAppearance.Danger;
                LawinServerStatusBadge.Content = "Not Installed";
            }
            else if (bInstalled && !bRunning)
            {
                LawinServerStatusBadge.Appearance = Wpf.Ui.Common.ControlAppearance.Caution;
                LawinServerStatusBadge.Content = "Not Running";
            }
            else if (bRunning)
            {
                LawinServerStatusBadge.Appearance = Wpf.Ui.Common.ControlAppearance.Success;
                LawinServerStatusBadge.Content = "Running";
            }
        }
    }
}

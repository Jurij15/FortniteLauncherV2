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
using System.Windows.Shapes;

namespace FortniteLauncherV2.App.Windows
{
    /// <summary>
    /// Interaction logic for InstanceManager.xaml
    /// </summary>
    public partial class InstanceManager : Wpf.Ui.Controls.UiWindow
    {
        public InstanceManager()
        {
            InitializeComponent();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (BuildsList.Items.Count < 1)
            {
                return;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }
    }
}

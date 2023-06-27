using FortniteLauncher.Cores;
using FortniteLauncher.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InjectPage : Page
    {
        public InjectPage()
        {
            this.InitializeComponent();
        }

        private void InjectBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Injector.InjectDll(Convert.ToInt32(PIDBox.Value), DLLPathBox.Text);
            }
            catch (Exception ex)
            {
                DialogService.ShowSimpleDialog("An error occured while injecting. Error message: " + ex.Message, "Error");
                throw;
            }
        }
    }
}

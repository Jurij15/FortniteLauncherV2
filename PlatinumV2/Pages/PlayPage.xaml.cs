using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using PlatinumV2.Classes;
using PlatinumV2.Controls;
using PlatinumV2.Helpers;
using PlatinumV2.Interop;
using PlatinumV2.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUIEx.Messaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PlatinumV2.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayPage : Page
    {
        public static SeasonControl _cachedControl = null;
        SeasonDefinitionsManager seasonDefinitionsManager;

        private void LoadVersions()
        {
            SeasonsView.Items.Clear();
            List<FortniteSeasonBaseClass> seasonsSources = new List<FortniteSeasonBaseClass>();

            seasonsSources = seasonDefinitionsManager.GetAllSeasonDefinitions();

            foreach (var item in seasonsSources)
            {
                SeasonControl control = new SeasonControl();
                control.SeasonCardBackgroundImageSource = ImageHelper.GetImageSource(item.SplashImagePath);
                control.SeasonBaseClass = item;
                control.SeasonDisplayName = item.SeasonDisplayName;

                control.Width = 190;
                control.Height = 290;

                SeasonsView.Items.Add(control);
            }
            seasonsSources.Clear();
        }
        public PlayPage()
        {
            this.InitializeComponent();
            this.DataContext = this;

            seasonDefinitionsManager = new SeasonDefinitionsManager();
            LoadVersions();
        }

        private void GridView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_cachedControl != null)
            {

            }
            else
            {
                LoadVersions();
            }
        }
    }
}

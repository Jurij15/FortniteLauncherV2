using ABI.Windows.UI;
using CommunityToolkit.WinUI.UI.Animations;
using FortniteLauncher.Cores;
using FortniteLauncher.Services;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FortniteLauncher.Pages.SettingsPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonalizationPage : Page
    {
        bool InitFinished = false;
        public PersonalizationPage()
        {
            this.InitializeComponent();

            if (Globals.Theme == 0) //dark
            {
                ThemeBox.SelectedItem = DarkTheme;
            }
            else if (Globals.Theme == 1)
            {
                ThemeBox.SelectedItem = LightTheme;
            }

            if (Globals.SoundMode == ElementSoundPlayerState.On)
            {
                SoundToggle.IsOn = true;
            }
            else
            {
                SoundToggle.IsOn = false;
            }

            if (ThemeService.IsContentLayerVisible())
            {
                BackgroundLayerCardSwitch.IsOn = true;
            }

            InitFinished = true;
        }

        private void ThemeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!InitFinished)
            {
                return;
            }
            if (ThemeBox.SelectedItem == null) { return; }

            if (ThemeBox.SelectedItem == DarkTheme)
            {
                Settings.SaveNewThemeConfig(0);
                Globals.Theme = 0;

                ThemeService.ChangeTheme(ElementTheme.Dark);
            }
            else if (ThemeBox.SelectedItem == LightTheme)
            {
                Settings.SaveNewThemeConfig(1);
                Globals.Theme = 1;
                ThemeService.ChangeTheme(ElementTheme.Light);
            }

            AppRestartRequired.IsOpen = true;
        }

        private void SoundToggle_Toggled(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.IsOn)
            {
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
                Globals.SoundMode = ElementSoundPlayerState.On; 
            }
            else
            {
                ElementSoundPlayer.State = ElementSoundPlayerState.Off;
                Globals.SoundMode = ElementSoundPlayerState.Off;
            }
        }

        private void RestartAppBtn_Click(object sender, RoutedEventArgs e)
        {
            Globals.RestartApp();
        }

        private void BackdropComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (Globals.Objects.MainWindow.Content as Grid).Background = new SolidColorBrush(Colors.Transparent);
            ComboBoxItem selecteditem =(ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (selecteditem == MicaBackdrop)
            {
                Microsoft.UI.Xaml.Media.MicaBackdrop backdrop = new MicaBackdrop();
                backdrop.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base;

                Globals.Objects.MainWindow.SystemBackdrop = backdrop;
            }
            else if (selecteditem == MicaAltBackdrop)
            {
                Microsoft.UI.Xaml.Media.MicaBackdrop backdrop = new MicaBackdrop();
                backdrop.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt;

                Globals.Objects.MainWindow.SystemBackdrop = backdrop;
            }
            else if (selecteditem == AcrylicBackdrop)
            {
                Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop backdrop = new DesktopAcrylicBackdrop();

                Globals.Objects.MainWindow.SystemBackdrop = backdrop;
            }
            if (selecteditem == NoneBackdrop)
            {
                Globals.Objects.MainWindow.SystemBackdrop = null;
                (Globals.Objects.MainWindow.Content as Grid).Background = App.Current.Resources["ApplicationPageBackgroundThemeBrush"] as SolidColorBrush;
            }
        }

        private void BackgroundLayerCardSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (!InitFinished)
            {
                return;
            }

            if (BackgroundLayerCardSwitch.IsOn)
            {
                ThemeService.SetContentBackgrund(true);
            }
            else
            {
                ThemeService.SetContentBackgrund(false);
            }
        }
    }
}

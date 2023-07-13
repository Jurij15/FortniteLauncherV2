using FortniteLauncher.Cores;
using FortniteLauncher.Services;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher.Managers
{
    public class BackupManager
    {
        public static async void CreateBackup(bool bShowOpenDirectoryDialog)
        {
			try
			{
                ZipFile.CreateFromDirectory(Cores.Settings.RootSettingsDir, Cores.Settings.RootBackupsDir + "Backup" + DateTime.Now.ToString("dd-mm--yyyy--mm--hh-ss"));

                if (bShowOpenDirectoryDialog)
                {
                    ContentDialog dialog = DialogService.CreateContentDialog("Backup Created", "Open directory?");
                    dialog.PrimaryButtonClick += Dialog_PrimaryButtonClick;
                    dialog.PrimaryButtonText = "Open";
                    dialog.CloseButtonText = "Close";

                    dialog.DefaultButton = ContentDialogButton.Primary;

                    await dialog.ShowAsync();
                }
            }
			catch (Exception ex)
			{
                DialogService.ShowSimpleDialog("Error message " + ex.Message, "Fail");
				throw;
			}
        }

        private static void Dialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Process p = new Process();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.Arguments = Settings.RootSettingsDir;
            p.Start();
        }

        public static async void RestoreFromBackup(string BackupZipPath)
        {

        }
    }
}

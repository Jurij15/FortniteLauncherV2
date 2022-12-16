using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;
using System.Reflection;
using System.IO;

namespace FortniteLauncherV2.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        //got this from the demo wpfui app
        // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
        // https://docs.microsoft.com/dotnet/core/extensions/generic-host
        // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
        // https://docs.microsoft.com/dotnet/core/extensions/configuration
        // https://docs.microsoft.com/dotnet/core/extensions/logging
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IThemeService, ThemeService>();

                // Taskbar manipulation
                services.AddSingleton<ITaskBarService, TaskBarService>();

                // Snackbar service
                services.AddSingleton<ISnackbarService, SnackbarService>();

                // Dialog service
                services.AddSingleton<IDialogService, DialogService>();

            }).Build();

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            _host.StartAsync();
        }
    }
}

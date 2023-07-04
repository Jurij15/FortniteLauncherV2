using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher
{
    public enum LogImportance
    {
        Info,
        Warning,
        Error,
        Success
    }

    public enum LogSource
    {
        AppCore,
        Startup,
        Launcher,
        Navigation,
        DialogService,
        NotificationService,
        Helper,
        BackupManager,
        BuildsManager
    }
    public class Logger
    {
        public static void Log(LogImportance Importance ,LogSource Source, string Message)
        {
            //set color
            switch (Importance)
            {
                case LogImportance.Info:
                    Console.ResetColor();
                    break;
                case LogImportance.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogImportance.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogImportance.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }

            string source = Source.ToString();

            Console.WriteLine("["+source+"]"+Message);

            Console.ResetColor();
        }
    }
}

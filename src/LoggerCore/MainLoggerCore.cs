using System;
using System.Threading;


namespace LoggerCore

{
    static class MainLoggerCore
    {       
        /// <summary>
        /// Главная точка входа для приложения LoggerCore.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.Title = ("LoggerCore Console");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("LoggerCore: run .NET Framework 4.7.2");
            Console.WriteLine("GitHub: https://github.com/levshx/logger-mc-server");
            Console.WriteLine("Author: levshx");
            Console.WriteLine("ProductVersion : {0}",
                System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductVersion);
            Console.WriteLine();
            Console.ResetColor();

            Logger core = new Logger();
            core.Start();
            Thread.Sleep(1000);

            while (true)
            {
                LoggerCore.LogLines tmp_loglines = new LogLines();
                tmp_loglines = core.GetLines();
                tmp_loglines.ForEach(SendLog);
                void SendLog(LogLine tmp_line)
                {
                    Console.WriteLine(tmp_line.text);
                }
                Thread.Sleep(100);
            }

            //core.settings.currentLogList.ForEach(print);
            Console.ReadLine();
            
        }
    }
}

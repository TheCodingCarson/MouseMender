using System.Diagnostics;

namespace Mouse_Mender
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set "Below Normal" CPU Process Priority
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
            Thread.CurrentThread.IsBackground = true;

            // Initialize Application
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
using FileManager;
using System;
using System.Windows;

namespace NPCGenerator
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            ErrorHandler.collectError($"Unhandled exception: {ex.Message} \n" +
                $"Please submit the log file to the developer for adressing the issue!\n" +
                $"Saving data and exiting the app is advised.", ex);
            ErrorHandler.errorPopup();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ErrorHandler.collectError($"Unhandled exception: {e.Exception.Message} \n" +
                $"Please submit the log file to the developer for adressing the issue!\n" +
                $"Saving data and exiting the app is advised.", e.Exception);
            ErrorHandler.errorPopup();
            e.Handled = true;
        }
    }
}

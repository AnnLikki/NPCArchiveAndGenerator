using System.Windows;

namespace FileManager
{
    /// <summary>
    /// This static class is responsible for collecting and
    /// displaying errors. 
    /// </summary>
    /// <remarks>
    /// Instead of printing the errors in the console
    /// it collects them into the log variable and displays them
    /// when appropriate (ex. when launching the app - all the errors
    /// related to loading files, when saving - all the errors
    /// related to saving the files).
    /// </remarks>
    public static class ErrorHandler
    {
        private static string log = "";

        /// <summary>
        /// Adds the error message to the log variable.
        /// </summary>
        public static void collectError(string error)
        {
            log += "\n" + error;
        }

        /// <summary>
        /// Shows the logs in a popup message and returns true 
        /// if there were errors to show, else returns false.
        /// </summary>
        /// <returns>Returns true if log.Length > 0, else returns false.</returns>
        public static bool errorPopup()
        {
            if (log.Length > 0)
            {
                MessageBox.Show(log, "Errors!", MessageBoxButton.OK, MessageBoxImage.Warning);
                log = "";
                return true;
            }
            log = "";
            return false;
        }

    }
}

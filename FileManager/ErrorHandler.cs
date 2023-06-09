using System.Windows;

namespace FileManager
{
    // This static class is responsible for collecting and 
    // displaying errors. 
    // Instead of only printing the errors in the console
    // I collect them into the log variable and display them
    // when appropriate (ex. when launching the app - all the errors
    // related to loading files, when saving - all the errors
    // related to saving the files).
    public static class ErrorHandler
    {
        private static string log = "";

        public static void collectError(string error)
        {
            log += "\n" + error;
        }

        // Shows the logs in a popup message and returns true if
        // there were errors to show, else returns false.
        public static bool errorPopup()
        {
            if(log.Length > 0) { 
                MessageBox.Show(log, "Errors!", MessageBoxButton.OK, MessageBoxImage.Warning);
                log = "";
                return true;
            }
            log = "";
            return false;
        }

    }
}

using FileManager;
using Microsoft.Win32;
using System.Windows;

namespace NPCGenerator
{
    // Main Window class, standart implementation.
    public partial class MainWindow : Window
    {
        // NPCsArchiveUC and RacesArchiveUC inherit from UserCotrol
        // and are basically tabs at the central panel that can be
        // switched by clicking the correspoding buttons.
        public NPCsArchiveUC npcsArchiveUC;
        public RacesArchiveUC racesArchiveUC;
        public MainWindow()
        {
            InitializeComponent();

            // Loading the data from files using SnL class.
            SnL.loadRaceData(SnL.racesSavePath);
            SnL.loadNPCData(SnL.NPCsSavePath);
            // Displaying all the collected errors in one popup window.
            ErrorHandler.errorPopup();

            npcsArchiveUC = new NPCsArchiveUC();
            racesArchiveUC = new RacesArchiveUC();

            // Setting the center of the window to be the
            // NPC archive by default.
            centerContainer.Content = npcsArchiveUC;
        }

        // Buttons that switch panels.
        private void NPCsArchiveBtn_Click(object sender, RoutedEventArgs e)
        {
            centerContainer.Content = npcsArchiveUC;
        }

        private void RacesArchiveBtn_Click(object sender, RoutedEventArgs e)
        {
            centerContainer.Content = racesArchiveUC;
        }

        // Saving all the archives in the default directory,
        // collecting and displaying the errors.
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            SnL.saveRaceData(SnL.racesSavePath);
            SnL.saveNPCData(SnL.NPCsSavePath);
            ErrorHandler.errorPopup();
        }

        // Saving all the archives in a user-spesified location,
        // collecting and displaying the errors.
        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files(*.json)|*.json";
            if (saveFileDialog.ShowDialog() == false)
                return;

            string filename = saveFileDialog.FileName;
            if (centerContainer.Content == racesArchiveUC)
            {
                SnL.saveRaceData(filename);
                if (!ErrorHandler.errorPopup())
                    MessageBox.Show("Race Archive have been saved.");
            }
            else if (centerContainer.Content == npcsArchiveUC)
            {
                SnL.saveNPCData(filename);
                if (!ErrorHandler.errorPopup())
                    MessageBox.Show("NPC Archive have been saved.");
            }
        }

        // Import button allows to import either an NPC archive or
        // a race archive (depending on which tab is currently open)
        private void importBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == false)
                return;

            string filename = openFileDialog.FileName;
            if (centerContainer.Content == racesArchiveUC)
            {
                SnL.loadRaceData(filename);
                racesArchiveUC = new RacesArchiveUC();
                centerContainer.Content = racesArchiveUC;
                if (!ErrorHandler.errorPopup())
                    MessageBox.Show("Race Archive have been loaded.");

            }
            else if (centerContainer.Content == npcsArchiveUC)
            {
                SnL.loadNPCData(filename);
                npcsArchiveUC = new NPCsArchiveUC();
                centerContainer.Content = npcsArchiveUC;
                if (!ErrorHandler.errorPopup())
                    MessageBox.Show("NPC Archive have been loaded.");
            }
        }
    }
}

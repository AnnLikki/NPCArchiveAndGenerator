using Archives;
using FileManager;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NPCGenerator
{
    /// <summary>
    /// Main Window class, standart implementation.
    /// </summary>
    public partial class MainWindow : Window
    {
        // NPCsArchiveUC and RacesArchiveUC inherit from UserCotrol
        // and are basically tabs at the central panel that can be
        // switched by clicking the correspoding buttons.
        public NPCsArchiveUC npcsArchiveUC;
        public RacesArchiveUC racesArchiveUC;
        public static bool safeMode = true;
        public MainWindow()
        {
            InitializeComponent();

            npcsArchiveUC = new NPCsArchiveUC();
            racesArchiveUC = new RacesArchiveUC();

            // Setting the center of the window to be the
            // NPC archive by default.
            centerContainer.Content = npcsArchiveUC;
            updateFileName();
        }

        // Buttons that switch panels.
        private void NPCsArchiveBtn_Click(object sender, RoutedEventArgs e)
        {
            centerContainer.Content = npcsArchiveUC;
            updateFileName();
            npcsArchiveUC.updateFilterable();
        }

        private void RacesArchiveBtn_Click(object sender, RoutedEventArgs e)
        {
            centerContainer.Content = racesArchiveUC;
            updateFileName();
            racesArchiveUC.updateFilterable();
        }

        /// <summary>
        /// Updating the label that contatins current file name.
        /// </summary>
        private void updateFileName()
        {
            if (centerContainer.Content == npcsArchiveUC)
                if (SnL.NPCsSavePath != null)
                    fileNameLbl.Content = SnL.NPCsSavePath;
                else
                    fileNameLbl.Content = "Unsaved NPC Archive";
            else if (centerContainer.Content == racesArchiveUC)
                if (SnL.racesSavePath != null)
                    fileNameLbl.Content = SnL.racesSavePath;
                else
                    fileNameLbl.Content = "Unsaved Races Archive";
        }

        /// <summary>
        /// Saving all of the archives. For each one that wasn't saved yet, a saving dialog opens.  
        /// </summary>
        private void saveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SnL.NPCsSavePath == null)
                SnL.saveViaDialog(SnL.TYPE_NPC, "Save NPCs Archive");
            else
                SnL.saveArchive(SnL.TYPE_NPC, SnL.NPCsSavePath);
            updateFileName();
            if (SnL.racesSavePath == null)
                SnL.saveViaDialog(SnL.TYPE_RACE, "Save Races Archive");
            else
                SnL.saveArchive(SnL.TYPE_RACE, SnL.racesSavePath);
            updateFileName();

        }

        /// <summary>
        /// Saving currently open archive in a user-spesified location. 
        /// </summary>
        private void saveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (centerContainer.Content == npcsArchiveUC)
                SnL.saveViaDialog(SnL.TYPE_NPC, "Save NPCs Archive as...");
            else if (centerContainer.Content == racesArchiveUC)
                SnL.saveViaDialog(SnL.TYPE_RACE, "Save Races Archive as...");
            updateFileName();
        }

        /// <summary>
        /// Open button allows to open either an NPC archive or a race archive 
        /// (depending on which tab is currently open).
        /// </summary>
        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            if (centerContainer.Content == npcsArchiveUC)
            {
                SnL.openViaDialog(SnL.TYPE_NPC, "Open NPCs Archive");
                npcsArchiveUC = new NPCsArchiveUC();
                centerContainer.Content = npcsArchiveUC;
            }
            else if (centerContainer.Content == racesArchiveUC)
            {
                SnL.openViaDialog(SnL.TYPE_RACE, "Open Races Archive");
                racesArchiveUC = new RacesArchiveUC();
                centerContainer.Content = racesArchiveUC;
            }
            updateFileName();
        }

        private void TheWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (safeMode)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save everything before closing?", "Saving",
                   MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                    saveAllBtn_Click(null, null);
                else if (result == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void safeModeChkBx_Click(object sender, RoutedEventArgs e)
        {
            if (safeModeChkBx.IsChecked == true)
                safeMode = true;
            else
                safeMode = false;
        }
    }
}

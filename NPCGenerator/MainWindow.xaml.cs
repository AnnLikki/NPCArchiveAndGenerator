using Archives;
using FileManager;
using NPCArchiveAndGenerator;
using System.Windows;
using static Archives.Enums;
using static NPCArchiveAndGenerator.Controller;


namespace NPCGenerator
{
    /// <summary>
    /// Main Window class, standart implementation.
    /// </summary>
    public partial class MainWindow : Window
    {
        public NPCsArchiveUC npcsArchiveUC;
        public RacesArchiveUC racesArchiveUC;
        public BundlesArchivesUC bundlesArchivesUC;
        public ArchetypesArchivesUC archetypesArchivesUC;


        public MainWindow()
        {
            InitializeComponent();

            tryLoad();

            npcsArchiveUC = new NPCsArchiveUC();
            racesArchiveUC = new RacesArchiveUC();
            bundlesArchivesUC = new BundlesArchivesUC();
            archetypesArchivesUC = new ArchetypesArchivesUC();

            centerContainer.Content = npcsArchiveUC;

            Controller.npcsArchiveUC = npcsArchiveUC;
            Controller.racesArchiveUC = racesArchiveUC;
            Controller.bundlesArchivesUC = bundlesArchivesUC;
            Controller.archetypesArchivesUC = archetypesArchivesUC;


            updateFileName();
        }

        // Buttons that switch panels.
        private void NPCsArchiveBtn_Click(object sender, RoutedEventArgs e)
        {
            status = Status.NPC;
            centerContainer.Content = npcsArchiveUC;
            updateFileName();
            npcsArchiveUC.updateFilterable();
        }

        private void RacesArchiveBtn_Click(object sender, RoutedEventArgs e)
        {
            status = Status.Race;
            centerContainer.Content = racesArchiveUC;
            updateFileName();
            racesArchiveUC.updateFilterable();
        }
        private void BundlesArchivesBtn_Click(object sender, RoutedEventArgs e)
        {
            status = Status.Bundle;
            centerContainer.Content = bundlesArchivesUC;
            updateFileName();
        }
        private void ArchetypeArchivesBtn_Click(object sender, RoutedEventArgs e)
        {
            status = Status.Archetype;
            centerContainer.Content = archetypesArchivesUC;
            updateFileName();
        }


        private void tryLoad()
        {
            if (SnL.loadData(SnL.dataSavePath))
            {
                SnL.loadArchive(SnL.SaveType.NPC, SnL.NPCsSavePath);
                SnL.loadArchive(SnL.SaveType.Races, SnL.RacesSavePath);
                SnL.loadArchive(SnL.SaveType.Bundles, SnL.BundlesSavePath);
                SnL.loadArchive(SnL.SaveType.Archetypes, SnL.ArchetypesSavePath);
            }
            ErrorHandler.errorPopup();
        }


        /// <summary>
        /// Updating the label that contatins current file name.
        /// </summary>
        private void updateFileName()
        {
            Controller.UpdateFileName();
        }

        /// <summary>
        /// Saving all of the archives. For each one that wasn't saved yet, a saving dialog opens.  
        /// </summary>
        private void saveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SnL.NPCsSavePath == null)
                SnL.saveViaDialog(SnL.SaveType.NPC, "Save NPCs Archive", "NPCs Archive");
            else
                SnL.saveArchive(SnL.SaveType.NPC, SnL.NPCsSavePath);

            if (SnL.RacesSavePath == null)
                SnL.saveViaDialog(SnL.SaveType.Races, "Save Races Archive", "Races Archive");
            else
                SnL.saveArchive(SnL.SaveType.Races, SnL.RacesSavePath);

            if (SnL.BundlesSavePath == null)
                SnL.saveViaDialog(SnL.SaveType.Bundles, "Save Bundles Archive", "Bundles Archive");
            else
                SnL.saveArchive(SnL.SaveType.Bundles, SnL.BundlesSavePath);

            if (SnL.ArchetypesSavePath == null)
                SnL.saveViaDialog(SnL.SaveType.Archetypes, "Save Archetypes Archive", "Archetypes Archive");
            else
                SnL.saveArchive(SnL.SaveType.Archetypes, SnL.ArchetypesSavePath);

            updateFileName();

            if (!ErrorHandler.errorPopup())
                MessageBox.Show("Saved succefully!", "Saved",
                       MessageBoxButton.OK, MessageBoxImage.Information);

        }

        /// <summary>
        /// Saving currently open archive in a user-spesified location. 
        /// </summary>
        private void saveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (status == Status.NPC)
                SnL.saveViaDialog(SnL.SaveType.NPC, "Save NPCs Archive as...", "NPCs Archive");
            else if (status == Status.Race)
                SnL.saveViaDialog(SnL.SaveType.Races, "Save Races Archive as...", "Races Archive");
            else if (status == Status.Bundle)
                SnL.saveViaDialog(SnL.SaveType.Bundles, "Save Bundles Archive as...", "Bundles Archive");
            else if (status == Status.Archetype)
                SnL.saveViaDialog(SnL.SaveType.Archetypes, "Save Archetypes Archive as...", "Archetypes Archive");

            updateFileName();
        }

        /// <summary>
        /// Open button allows to open either an NPC archive or a race archive 
        /// (depending on which tab is currently open).
        /// </summary>
        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            if (status == Status.NPC)
            {
                SnL.openViaDialog(SnL.SaveType.NPC, "Open NPCs Archive");
                npcsArchiveUC = new NPCsArchiveUC();
                centerContainer.Content = npcsArchiveUC;
                Controller.npcsArchiveUC = npcsArchiveUC;
            }
            else if (status == Status.Race)
            {
                SnL.openViaDialog(SnL.SaveType.Races, "Open Races Archive");
                racesArchiveUC = new RacesArchiveUC();
                centerContainer.Content = racesArchiveUC;
                Controller.racesArchiveUC = racesArchiveUC;
            }
            else if (status == Status.Bundle)
            {
                SnL.openViaDialog(SnL.SaveType.Bundles, "Open Bundles Archive");
                bundlesArchivesUC = new BundlesArchivesUC();
                centerContainer.Content = bundlesArchivesUC;
                Controller.bundlesArchivesUC = bundlesArchivesUC;
            }
            else if (status == Status.Archetype)
            {
                SnL.openViaDialog(SnL.SaveType.Archetypes, "Open Archetype Archive");
                archetypesArchivesUC = new ArchetypesArchivesUC();
                centerContainer.Content = archetypesArchivesUC;
                Controller.archetypesArchivesUC = archetypesArchivesUC;
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
            if (e.Cancel == false)
            {
                SnL.saveData(SnL.dataSavePath);
                ErrorHandler.errorPopup();
                Application.Current.Shutdown();
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

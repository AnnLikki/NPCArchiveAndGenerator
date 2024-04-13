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
        public MainWindow()
        {
            InitializeComponent();

            tryLoad();
            updateFileName();

            npcsArchiveUC = new NPCsArchiveUC();
            racesArchiveUC = new RacesArchiveUC();
            bundlesArchivesUC = new BundlesArchivesUC();

            centerContainer.Content = npcsArchiveUC;
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
        private void LittleArchivesBtn_Click(object sender, RoutedEventArgs e)
        {
            status = Status.Bundle;
            centerContainer.Content = bundlesArchivesUC;
            updateFileName();
        }

        private void tryLoad()
        {
            if (SnL.loadData(SnL.dataSavePath))
            {
                SnL.loadArchive(SnL.SaveType.NPC, SnL.NPCsSavePath);
                SnL.loadArchive(SnL.SaveType.Races, SnL.RacesSavePath);
            }
            ErrorHandler.errorPopup();
        }


        /// <summary>
        /// Updating the label that contatins current file name.
        /// </summary>
        private void updateFileName()
        {
            fileNameLbl.Content = GetFileName();
        }

        /// <summary>
        /// Saving all of the archives. For each one that wasn't saved yet, a saving dialog opens.  
        /// </summary>
        private void saveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SnL.NPCsSavePath == null)
                SnL.saveViaDialog(SnL.SaveType.NPC, "Save NPCs Archive");
            else
                SnL.saveArchive(SnL.SaveType.NPC, SnL.NPCsSavePath);

            if (SnL.RacesSavePath == null)
                SnL.saveViaDialog(SnL.SaveType.Races, "Save Races Archive");
            else
                SnL.saveArchive(SnL.SaveType.Races, SnL.RacesSavePath);

            if (SnL.BundlesSavePath == null)
                SnL.saveViaDialog(SnL.SaveType.Bundles, "Save Bundles Archive");
            else
                SnL.saveArchive(SnL.SaveType.Bundles, SnL.BundlesSavePath);

            if (SnL.ArchetypesSavePath == null)
                SnL.saveViaDialog(SnL.SaveType.Archetypes, "Save Archetypes Archive");
            else
                SnL.saveArchive(SnL.SaveType.Archetypes, SnL.ArchetypesSavePath);

            updateFileName();

        }

        /// <summary>
        /// Saving currently open archive in a user-spesified location. 
        /// </summary>
        private void saveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (status == Status.NPC)
                SnL.saveViaDialog(SnL.SaveType.NPC, "Save NPCs Archive as...");
            else if (status == Status.Race)
                SnL.saveViaDialog(SnL.SaveType.Races, "Save Races Archive as...");
            else if (status == Status.Bundle)
                SnL.saveViaDialog(SnL.SaveType.Bundles, "Save Bundles Archive as...");
            else if (status == Status.Archetype)
                SnL.saveViaDialog(SnL.SaveType.Archetypes, "Save Archetypes Archive as...");

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
            }
            else if (status == Status.Race)
            {
                SnL.openViaDialog(SnL.SaveType.Races, "Open Races Archive");
                racesArchiveUC = new RacesArchiveUC();
                centerContainer.Content = racesArchiveUC;
            }
            else if (status == Status.Bundle)
            {
                SnL.openViaDialog(SnL.SaveType.Bundles, "Open Bundles Archive");
                bundlesArchivesUC = new BundlesArchivesUC();
                centerContainer.Content = bundlesArchivesUC;
            }
            else if (status == Status.Archetype)
            {
                //SnL.openViaDialog(SnL.SaveType.Archetypes, "Open Archetype Archive");
                //racesArchiveUC = new RacesArchiveUC();
                //centerContainer.Content = racesArchiveUC;
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
            SnL.saveData(SnL.dataSavePath);
            ErrorHandler.errorPopup();
        }

        private void safeModeChkBx_Click(object sender, RoutedEventArgs e)
        {
            if (safeModeChkBx.IsChecked == true)
                safeMode = true;
            else
                safeMode = false;
        }


        // TODO This is wrong, rethink

        ///// <summary>
        ///// Double clicking the archive path label "closes" the archive. Temporary solution.
        ///// </summary>
        private void closeArchive_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        //    if (safeMode)
        //    {
        //        MessageBoxResult result = MessageBox.Show("Do you want to save this archive before closing?", "Saving",
        //           MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        //        if (result == MessageBoxResult.Yes)
        //        {
        //            if (status == Status.ArchiveNPC)
        //            {
        //                if (SnL.NPCsSavePath != null)
        //                    SnL.saveArchive(SnL.SaveType.NPC, SnL.NPCsSavePath);
        //                else
        //                    SnL.saveViaDialog(SnL.SaveType.NPC, "Save NPCs Archive as...");
        //                SnL.NPCsSavePath = null;
        //            }
        //            else if (status == Status.ArchiveRace)
        //            {
        //                if (SnL.storageSavePath != null)
        //                    SnL.saveArchive(SnL.SaveType.STORAGE, SnL.storageSavePath);
        //                else
        //                    SnL.saveViaDialog(SnL.SaveType.STORAGE, "Save Storage as...");
        //                SnL.storageSavePath = null;
        //            }
        //        }
        //        else if (result == MessageBoxResult.Cancel)
        //            return;
        //    }

        //    if (status == Status.ArchiveNPC)
        //    {
        //        SnL.NPCsSavePath = null;
        //        ArchiveHandler.absoluteArchiveNPC.Clear();
        //    }
        //    else if (status == Status.ArchiveRace)
        //    {
        //        SnL.storageSavePath = null;
        //        ArchiveHandler.storage.Clear(ArchiveType.Race);
        //    }
        //    else if (status == Status.ArchiveBundles)
        //    {
        //        SnL.storageSavePath = null;
        //        ArchiveHandler.storage.Clear(ArchiveType.Race);
        //    }
        //    updateFileName();
        //    npcsArchiveUC.updateFilterable();
        //    racesArchiveUC.updateFilterable();

        //}

    }
}

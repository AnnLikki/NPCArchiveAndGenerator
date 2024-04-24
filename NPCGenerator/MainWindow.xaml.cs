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


        Archetype defaultArchetype = new Archetype("Default Archetype");

        Race human, elf;

        Bundle commonNames, elvenNames, ageNicknames;
        Bundle jobs;

        void racesSetup()
        {
            // Creating races and adding them to the dictionaries
            human = new Race("Human", "", 18, 80);
            human.SetGender(Gender.Female, 5);
            human.SetGender(Gender.Male, 5);
            human.SetGender(Gender.Neutral, 1);

            elf = new Race("Elf", "", 18, 750);
            elf.SetGender(Gender.Female, 5);
            elf.SetGender(Gender.Male, 5);
            elf.SetGender(Gender.Neutral, 2);

            ArchiveHandler.raceStorage.Add(human);
            ArchiveHandler.raceStorage.Add(elf);
            defaultArchetype.AddRace(human, 7);
            defaultArchetype.AddRace(elf, 3);

            defaultArchetype.SetGender(Gender.Female, 1);
            defaultArchetype.SetGender(Gender.Male, 1);
        }

        void commonNamesSetup()
        {
            // Common names bundle, contains regular names that are influenced by human culture
            commonNames = new Bundle("Common Names");
            // Layer 0 - male, female and gerder-neutral first names
            commonNames.InsertNewLayer(0, 1.0);
            commonNames.AddToLayer(0, "Arthur", 1, Gender.Male);
            commonNames.AddToLayer(0, "James", 1, Gender.Male);
            commonNames.AddToLayer(0, "Henry", 1, Gender.Male);
            commonNames.AddToLayer(0, "Alice", 1, Gender.Female);
            commonNames.AddToLayer(0, "Olivia", 1, Gender.Female);
            commonNames.AddToLayer(0, "Evelyn", 1, Gender.Female);
            commonNames.AddToLayer(0, "Alex");
            commonNames.AddToLayer(0, "Jamie");
            commonNames.AddToLayer(0, "Sam");
            // Layer 1 - male, female and gerder-neutral middle names, 30% chance of picking
            commonNames.InsertNewLayer(1, 0.3);
            commonNames.AddToLayer(1, " Arthur", 1, Gender.Male);
            commonNames.AddToLayer(1, " James", 1, Gender.Male);
            commonNames.AddToLayer(1, " Henry", 1, Gender.Male);
            commonNames.AddToLayer(1, " Alice", 1, Gender.Female);
            commonNames.AddToLayer(1, " Olivia", 1, Gender.Female);
            commonNames.AddToLayer(1, " Evelyn", 1, Gender.Female);
            commonNames.AddToLayer(1, " Alex");
            commonNames.AddToLayer(1, " Jamie");
            commonNames.AddToLayer(1, " Sam");
            // Layer 2 - gerder-neutral last names, 70% chance of picking
            commonNames.InsertNewLayer(2, 0.7);
            commonNames.AddToLayer(2, " Adams");
            commonNames.AddToLayer(2, " Allen");
            commonNames.AddToLayer(2, " Smith");

            human.CompatableBundles.AddBundle(BundleType.Name, commonNames);
            elf.CompatableBundles.AddBundle(BundleType.Name, commonNames);
            ArchiveHandler.bundleStorage.Add(BundleType.Name, commonNames);
        }

        void elvenNamesSetup()
        {
            // Elven names bundle, contains only elven names
            elvenNames = new Bundle("Elven Names");
            // Layer 0 - male, female and gerder-neutral first names
            elvenNames.InsertNewLayer(0, 1.0);
            elvenNames.AddToLayer(0, "Arin", 1, Gender.Male);
            elvenNames.AddToLayer(0, "Althaea", 1, Gender.Male);
            elvenNames.AddToLayer(0, "Calathes", 1, Gender.Male);
            elvenNames.AddToLayer(0, "Eira", 1, Gender.Female);
            elvenNames.AddToLayer(0, "Faerydae", 1, Gender.Female);
            elvenNames.AddToLayer(0, "Kaelen", 1, Gender.Female);
            elvenNames.AddToLayer(0, "Darindel");
            elvenNames.AddToLayer(0, "Ondine");
            elvenNames.AddToLayer(0, "Eira");

            // Layer 1 - gerder-neutral last names, 70% chance of picking
            elvenNames.InsertNewLayer(1, 0.7);
            elvenNames.AddToLayer(1, " Pharomir");
            elvenNames.AddToLayer(1, " Rivenneth");
            elvenNames.AddToLayer(1, " Silvethiel");

            elf.CompatableBundles.AddBundle(BundleType.Name, elvenNames);
            ArchiveHandler.bundleStorage.Add(BundleType.Name, elvenNames);
        }

        void ageNicknameSetup()
        {
            // Age nicknames bundle, contains nicknames that relate to gender
            ageNicknames = new Bundle("Age Nicknames");
            // Layer 0 - gerder-neutral nicknames for 0-5 years
            ageNicknames.InsertNewLayer(0, 1.0, "", Gender.Neutral, 0, 5);
            ageNicknames.AddToLayer(0, "Baby");
            ageNicknames.AddToLayer(0, "Little one");
            ageNicknames.AddToLayer(0, "Munchkin");
            // Layer 1 - gerder-neutral nicknames for 6-10 years
            ageNicknames.InsertNewLayer(1, 1.0, "", Gender.Neutral, 6, 10);
            ageNicknames.AddToLayer(1, "Kiddo");
            ageNicknames.AddToLayer(1, "Youngster");
            ageNicknames.AddToLayer(1, "Junior");
            // Layer 2 - gerder-neutral nicknames for 11-19 years
            ageNicknames.InsertNewLayer(2, 1.0, "", Gender.Neutral, 11, 19);
            ageNicknames.AddToLayer(2, "Teenie");
            ageNicknames.AddToLayer(2, "Teenster");
            ageNicknames.AddToLayer(2, "Youth");
            // Layer 3 - gerder-neutral nicknames for 20-25 years
            ageNicknames.InsertNewLayer(3, 1.0, "", Gender.Neutral, 20, 25);
            ageNicknames.AddToLayer(3, "Twentysomething");
            ageNicknames.AddToLayer(3, "Quarter-lifer");
            ageNicknames.AddToLayer(3, "Early bird");
            // Layer 4 - gerder-neutral nicknames for 26-35 years
            ageNicknames.InsertNewLayer(4, 1.0, "", Gender.Neutral, 26, 35);
            ageNicknames.AddToLayer(4, "Prime timer");
            ageNicknames.AddToLayer(4, "Bloomer");
            ageNicknames.AddToLayer(4, "Seasoned soul");
            // Layer 5 - gerder-neutral nicknames for 36-55 years
            ageNicknames.InsertNewLayer(5, 1.0, "", Gender.Neutral, 36, 55);
            ageNicknames.AddToLayer(5, "Mid-lifer");
            ageNicknames.AddToLayer(5, "Silver streak");
            ageNicknames.AddToLayer(5, "Golden Journeyer");
            // Layer 6 - gerder-neutral nicknames for 55-80 years
            ageNicknames.InsertNewLayer(6, 1.0, "", Gender.Neutral, 56, 80);
            ageNicknames.AddToLayer(6, "Senior sage");
            ageNicknames.AddToLayer(6, "Wise one");
            ageNicknames.AddToLayer(6, "Oldie");

            human.CompatableBundles.AddBundle(BundleType.Name, ageNicknames);
            elf.CompatableBundles.AddBundle(BundleType.Name, ageNicknames);
            ArchiveHandler.bundleStorage.Add(BundleType.Name, ageNicknames);
        }

        void ageDistributionsSetup()
        {
            // Filling archives of ages
            defaultArchetype.Ages.AddRange(10, 15, 0);
            defaultArchetype.Ages.AddRange(15, 35, 10);
            defaultArchetype.Ages.AddRange(35, 55, 5);

            human.Ages.AddRange(15, 30, 5);
            elf.Ages.AddRange(30, 40, 10);
        }

        void jobsSetup()
        {
            // Simple jobs bundle with age restrictions (only 20-50) and no genger restrictions
            jobs = new Bundle("Jobs", false, Gender.Neutral, 20, 50);
            // Layer 0 - job titles, 80% chance of picking, if fails - default value "Unemployed"
            // will get returned
            jobs.InsertNewLayer(0, 0.8, "Unemployed");
            jobs.AddToLayer(0, "Teacher");
            jobs.AddToLayer(0, "Doctor");
            jobs.AddToLayer(0, "Policeman", 1, Gender.Male);
            jobs.AddToLayer(0, "Policewoman", 1, Gender.Female);
            jobs.AddToLayer(0, "Judge");
            jobs.AddToLayer(0, "Vendor");

            ArchiveHandler.bundleStorage.Add(BundleType.Occupation, jobs);
            defaultArchetype.CompatableBundles.AddBundle(BundleType.Occupation, jobs);
            defaultArchetype.CompatableBundles[BundleType.Occupation].DefaultValue = "Can't work";
        }


        public MainWindow()
        {
            InitializeComponent();

            tryLoad();
            updateFileName();

            npcsArchiveUC = new NPCsArchiveUC();
            racesArchiveUC = new RacesArchiveUC();
            bundlesArchivesUC = new BundlesArchivesUC();
            archetypesArchivesUC = new ArchetypesArchivesUC();

            centerContainer.Content = npcsArchiveUC;



            //racesSetup();

            //// Creating name bundles
            //commonNamesSetup();
            //elvenNamesSetup();
            //ageNicknameSetup();

            //// Creating a job bundle
            //jobsSetup();

            //// Filling age distributions
            //ageDistributionsSetup();

            //ArchiveHandler.archetypeStorage.Add(defaultArchetype);

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
                SnL.openViaDialog(SnL.SaveType.Archetypes, "Open Archetype Archive");
                archetypesArchivesUC = new ArchetypesArchivesUC();
                centerContainer.Content = archetypesArchivesUC;
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

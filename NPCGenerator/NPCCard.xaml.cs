using Archives;
using NPCArchiveAndGenerator;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static Archives.Enums;

namespace NPCGenerator
{
    /// <summary>
    /// NPC Card is a User Control that is placed next to the NPC archive when sertain NPC is seleted. Allows to view and change NPC's data.
    /// </summary>
    public partial class NPCCard : UserControl
    {
        NPC npc;
        DataGrid grid;
        bool changesSaved = true;

        public NPCCard(NPC npc, DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;

            //ComboBox with race options taken from storage.
            raceCmb.ItemsSource = ArchiveHandler.raceStorage;

            archetypeCmb.ItemsSource = ArchiveHandler.archetypeStorage.ToList();
            ArchiveHandler.archetypeStorage.Items.CollectionChanged += Archetypes_CollectionChanged;

            if (ArchiveHandler.archetypeStorage.DefaultArchetype != null)
                archetypeCmb.SelectedItem = ArchiveHandler.archetypeStorage.DefaultArchetype;

            genderCmb.ItemsSource = Enum.GetValues(typeof(Gender));


            // Filling all the fields with NPC's data.
            this.npc = npc;

            nameTB.Text = npc.Name;

            // Using FindMatching function to set the ComboBox to the right
            // option if found the exact copy of NPC's race in the race archive.
            if (ArchiveHandler.raceStorage.FindRace(npc.RaceID) != null)
                raceCmb.SelectedItem = ArchiveHandler.raceStorage.FindRace(npc.RaceID);
            else
                raceCmb.SelectedItem = null;

            genderCmb.SelectedItem = npc.Gender;
            ageChronoTB.Text = npc.AgeChrono.ToString();
            ageBioTB.Text = npc.AgeBio.ToString();
            occupationTB.Text = npc.Occupation;
            placeTB.Text = npc.Place;
            characterTB.Text = npc.Character;
            backstoryTB.Text = npc.Backstory;
            heightTB.Text = npc.Height;
            physiqueTB.Text = npc.Physique;
            skincolourTB.Text = npc.Skin;
            hairTB.Text = npc.Hair;
            faceTB.Text = npc.Face;
            eyesTB.Text = npc.Eyes;
            clothesTB.Text = npc.Clothes;
            featuresTB.Text = npc.Features;
            strTB.Text = npc.Str.ToString();
            dexTB.Text = npc.Dex.ToString();
            conTB.Text = npc.Con.ToString();
            intTB.Text = npc.Int.ToString();
            wisTB.Text = npc.Wis.ToString();
            chaTB.Text = npc.Cha.ToString();

            // Calculating modifiers of NPC's stats and placing them
            // in the corresponding labels.
            // Adding a "+" to the number if it is positive.
            strModLbl.Content = (NPC.calcMod(npc.Str) >= 0) ? "+" + NPC.calcMod(npc.Str).ToString() : NPC.calcMod(npc.Str).ToString();
            dexModLbl.Content = (NPC.calcMod(npc.Dex) >= 0) ? "+" + NPC.calcMod(npc.Dex).ToString() : NPC.calcMod(npc.Dex).ToString();
            conModLbl.Content = (NPC.calcMod(npc.Con) >= 0) ? "+" + NPC.calcMod(npc.Con).ToString() : NPC.calcMod(npc.Con).ToString();
            intModLbl.Content = (NPC.calcMod(npc.Int) >= 0) ? "+" + NPC.calcMod(npc.Int).ToString() : NPC.calcMod(npc.Int).ToString();
            wisModLbl.Content = (NPC.calcMod(npc.Wis) >= 0) ? "+" + NPC.calcMod(npc.Wis).ToString() : NPC.calcMod(npc.Wis).ToString();
            chaModLbl.Content = (NPC.calcMod(npc.Cha) >= 0) ? "+" + NPC.calcMod(npc.Cha).ToString() : NPC.calcMod(npc.Cha).ToString();

            notesTB.Text = npc.Notes;

            unhighlightBecauseSaved();
        }

        private void Archetypes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            archetypeCmb.ItemsSource = ArchiveHandler.archetypeStorage.ToList();
        }

        /// <summary>
        /// Save button updates the NPC's data and notifies the DataGrid to update. Also recalculates the modifiers.
        /// </summary>
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            npc.updateInfoNotifyably(
            nameTB.Text, (Race)raceCmb.SelectedValue, (Gender)genderCmb.SelectedItem, ParseCarefully(ageChronoTB.Text), ParseCarefully(ageBioTB.Text),
            occupationTB.Text, placeTB.Text, characterTB.Text, backstoryTB.Text, heightTB.Text,
            physiqueTB.Text, skincolourTB.Text, hairTB.Text, faceTB.Text, eyesTB.Text, clothesTB.Text, featuresTB.Text,
            ParseCarefully(strTB.Text), ParseCarefully(dexTB.Text), ParseCarefully(conTB.Text), ParseCarefully(intTB.Text), ParseCarefully(wisTB.Text), ParseCarefully(chaTB.Text),
            notesTB.Text);
            strModLbl.Content = (NPC.calcMod(npc.Str) >= 0) ? "+" + NPC.calcMod(npc.Str).ToString() : NPC.calcMod(npc.Str).ToString();
            dexModLbl.Content = (NPC.calcMod(npc.Dex) >= 0) ? "+" + NPC.calcMod(npc.Dex).ToString() : NPC.calcMod(npc.Dex).ToString();
            conModLbl.Content = (NPC.calcMod(npc.Con) >= 0) ? "+" + NPC.calcMod(npc.Con).ToString() : NPC.calcMod(npc.Con).ToString();
            intModLbl.Content = (NPC.calcMod(npc.Int) >= 0) ? "+" + NPC.calcMod(npc.Int).ToString() : NPC.calcMod(npc.Int).ToString();
            wisModLbl.Content = (NPC.calcMod(npc.Wis) >= 0) ? "+" + NPC.calcMod(npc.Wis).ToString() : NPC.calcMod(npc.Wis).ToString();
            chaModLbl.Content = (NPC.calcMod(npc.Cha) >= 0) ? "+" + NPC.calcMod(npc.Cha).ToString() : NPC.calcMod(npc.Cha).ToString();
            unhighlightBecauseSaved();
        }

        private void field_TextChanged(object sender, TextChangedEventArgs e)
        {
            highlightNotSaved();
        }

        private void field_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            highlightNotSaved();
        }

        void highlightNotSaved()
        {
            changesSaved = false;
            saveBtn.Background = Brushes.Orange;
        }
        void unhighlightBecauseSaved()
        {
            changesSaved = true;
            saveBtn.Background = Brushes.LightGray;
        }


        /// <summary>
        /// Delete button deletes the NPC from the global archive,
        /// consequently changing DataGrid's selection and destroying
        /// this NPC Card.
        /// </summary>
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.safeMode)
            {
                MessageBoxResult confirmResult =
                    MessageBox.Show("Are you sure you want to delete it?", "Confirm Delete",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmResult == MessageBoxResult.Yes)
                    ArchiveHandler.absoluteArchiveNPC.Remove(npc);
            }
            else ArchiveHandler.absoluteArchiveNPC.Remove(npc);
        }

        /// <summary>
        /// Closing without saving.
        /// </summary>
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.safeMode && !changesSaved)
            {
                MessageBoxResult confirmResult =
                MessageBox.Show("Close without saving?", "Confirm Closing",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmResult == MessageBoxResult.Yes)
                    grid.SelectedItem = null;
            }
            else grid.SelectedItem = null;
        }

        private void ageBioTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ageBioTB.IsFocused)
                updateAgeOnBio();
            highlightNotSaved();
        }

        private void updateAgeOnBio()
        {
            if (((Race)raceCmb.SelectedValue) != null)
                if (ageBioTB.Text.Length == 0)
                    ageChronoTB.Text = "0";
                else
                    ageChronoTB.Text = ((Race)raceCmb.SelectedValue).calculateChronoAge(ParseCarefully(ageBioTB.Text)).ToString();
        }

        private void ageChronoTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ageChronoTB.IsFocused)
                updateAgeOnChrono();
            highlightNotSaved();
        }

        private void updateAgeOnChrono()
        {
            if (((Race)raceCmb.SelectedValue) != null)
                if (ageChronoTB.Text.Length == 0)
                    ageBioTB.Text = "0";
                else
                    ageBioTB.Text = ((Race)raceCmb.SelectedValue).calculateBioAge(ParseCarefully(ageChronoTB.Text)).ToString();
        }


        /// <summary>
        /// These methods check input text to Age TextBoxes so the user can 
        /// not input nothig except numbers in them.
        /// </summary>
        private void numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumericInput(e.Text))
                e.Handled = true;
        }
        /// <summary>
        /// These methods check input text to Age TextBoxes so the user can 
        /// not input nothig except numbers in them.
        /// </summary>
        private bool IsNumericInput(string input)
        {
            return int.TryParse(input, out _);
        }
        /// <summary>
        /// These methods check input text to Age TextBoxes so the user can 
        /// not input nothig except numbers in them.
        /// </summary>
        private int ParseCarefully(string s)
        {
            int result;
            int.TryParse(s, out result);
            return result;
        }


        /// <summary>
        /// Randomizes all fields. Doesn't randomize locked fields.
        /// </summary>
        private void randAllBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race;
                if (lockRaceBtn.IsChecked == false)
                {
                    race = archetype.GetRandomRace();
                    raceCmb.SelectedItem = race;
                }
                else
                    race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender;
                    if (lockGenderBtn.IsChecked == false)
                    {
                        gender = archetype.GetRandomGender(race);
                        genderCmb.SelectedItem = gender;
                    }
                    else
                        gender = (Gender)genderCmb.SelectedItem;

                    int ageChrono;
                    if (lockAgeBtn.IsChecked == false)
                    {
                        ageChrono = archetype.GetRandomAgeChrono(race);
                        ageChronoTB.Text = ageChrono.ToString();
                        updateAgeOnChrono();
                    }
                    else
                        ageChrono = ParseCarefully(ageChronoTB.Text);

                    int ageBio = race.calculateBioAge(ageChrono);


                    if (lockNameBtn.IsChecked == false)
                        nameTB.Text = archetype.GetRandomFromBundle(BundleType.Name, race, gender, ageBio);
                    if (lockOccupationBtn.IsChecked == false)
                        occupationTB.Text = archetype.GetRandomFromBundle(BundleType.Occupation, race, gender, ageBio);
                    if (lockCharacterBtn.IsChecked == false)
                        characterTB.Text = archetype.GetRandomFromBundle(BundleType.Character, race, gender, ageBio);
                    if (lockHeightBtn.IsChecked == false)
                        heightTB.Text = archetype.GetRandomFromBundle(BundleType.Height, race, gender, ageBio);
                    if (lockPhysiqueBtn.IsChecked == false)
                        physiqueTB.Text = archetype.GetRandomFromBundle(BundleType.Physique, race, gender, ageBio);
                    if (lockSkincolourBtn.IsChecked == false)
                        skincolourTB.Text = archetype.GetRandomFromBundle(BundleType.Skin, race, gender, ageBio);
                    if (lockHairBtn.IsChecked == false)
                        hairTB.Text = archetype.GetRandomFromBundle(BundleType.Hair, race, gender, ageBio);
                    if (lockFaceBtn.IsChecked == false)
                        faceTB.Text = archetype.GetRandomFromBundle(BundleType.Face, race, gender, ageBio);
                    if (lockEyesBtn.IsChecked == false)
                        eyesTB.Text = archetype.GetRandomFromBundle(BundleType.Eyes, race, gender, ageBio);
                    if (lockClothesBtn.IsChecked == false)
                        clothesTB.Text = archetype.GetRandomFromBundle(BundleType.Clothes, race, gender, ageBio);
                    if (lockFeaturesBtn.IsChecked == false)
                        featuresTB.Text = archetype.GetRandomFromBundle(BundleType.Features, race, gender, ageBio);
                }
            }
        }
        private void randNameBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    nameTB.Text = archetype.GetRandomFromBundle(BundleType.Name, race, gender, ageBio);

                }
            }
        }

        private void randRaceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = archetype.GetRandomRace();
                raceCmb.SelectedItem = race;
            }
        }

        private void randGenderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender;
                    gender = archetype.GetRandomGender(race);
                    genderCmb.SelectedItem = gender;
                }
            }
        }

        private void randAgeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    int ageChrono = archetype.GetRandomAgeChrono(race);
                    ageChronoTB.Text = ageChrono.ToString();
                    updateAgeOnChrono();
                }
            }
        }

        private void randOccupationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    occupationTB.Text = archetype.GetRandomFromBundle(BundleType.Occupation, race, gender, ageBio);
                }
            }
        }

        private void randCharacterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    characterTB.Text = archetype.GetRandomFromBundle(BundleType.Character, race, gender, ageBio);
                }
            }
        }

        private void randHeightBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    heightTB.Text = archetype.GetRandomFromBundle(BundleType.Height, race, gender, ageBio);
                }
            }
        }

        private void randPhysiqueBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    physiqueTB.Text = archetype.GetRandomFromBundle(BundleType.Physique, race, gender, ageBio);
                }
            }
        }

        private void randSkinColourBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    skincolourTB.Text = archetype.GetRandomFromBundle(BundleType.Skin, race, gender, ageBio);
                }
            }
        }

        private void randHairBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    hairTB.Text = archetype.GetRandomFromBundle(BundleType.Hair, race, gender, ageBio);
                }
            }
        }

        private void randFaceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    faceTB.Text = archetype.GetRandomFromBundle(BundleType.Face, race, gender, ageBio);
                }
            }
        }

        private void randEyesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    eyesTB.Text = archetype.GetRandomFromBundle(BundleType.Eyes, race, gender, ageBio);
                }
            }
        }

        private void randClothesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    clothesTB.Text = archetype.GetRandomFromBundle(BundleType.Clothes, race, gender, ageBio);
                }
            }
        }

        private void randFeaturesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (archetypeCmb.SelectedItem is Archetype archetype && archetype != null)
            {
                Race race = (Race)raceCmb.SelectedItem;

                if (race != null)
                {
                    Gender gender = (Gender)genderCmb.SelectedItem;
                    int ageChrono = ParseCarefully(ageChronoTB.Text);
                    int ageBio = race.calculateBioAge(ageChrono);

                    featuresTB.Text = archetype.GetRandomFromBundle(BundleType.Features, race, gender, ageBio);
                }
            }
        }
    }
}
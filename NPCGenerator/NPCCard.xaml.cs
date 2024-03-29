﻿using Archives;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NPCGenerator
{
    /// <summary>
    /// NPC Card is a User Control that is placed next to the NPC archive when sertain NPC is seleted. Allows to view and change NPC's data.
    /// </summary>
    public partial class NPCCard : UserControl
    {
        NPC npc;
        DataGrid grid;

        public NPCCard(NPC npc, DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;

            //ComboBox with race options taken from global race archive.
            raceCmb.ItemsSource = ArchiveHandler.absoluteArchiveRace;

            // Filling all the fields with NPC's data.
            this.npc = npc;

            nameTB.Text = npc.Name;

            // Using FindMatching function to set the ComboBox to the right
            // option if found the exact copy of NPC's race in the race archive.
            if (ArchiveHandler.absoluteArchiveRace.FindMatching(npc.Race) != null)
                raceCmb.SelectedItem = ArchiveHandler.absoluteArchiveRace.FindMatching(npc.Race);
            else
                raceCmb.SelectedItem = npc.Race;

            genderTB.Text = npc.Gender;
            ageChronoTB.Text = npc.AgeChrono.ToString();
            ageBioTB.Text = npc.AgeBio.ToString();
            occupationTB.Text = npc.Occupation;
            placeTB.Text = npc.Place;
            charaterTB.Text = npc.Character;
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
        }

        /// <summary>
        /// Save button updates the NPC's data and notifies the DataGrid to update. Also recalculates the modifiers.
        /// </summary>
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            npc.updateInfoNotifyably(
            nameTB.Text, (Race)raceCmb.SelectedValue, genderTB.Text, ParseCarefully(ageChronoTB.Text), ParseCarefully(ageBioTB.Text),
            occupationTB.Text, placeTB.Text, charaterTB.Text, backstoryTB.Text, heightTB.Text,
            physiqueTB.Text, skincolourTB.Text, hairTB.Text, faceTB.Text, eyesTB.Text, clothesTB.Text, featuresTB.Text,
            ParseCarefully(strTB.Text), ParseCarefully(dexTB.Text), ParseCarefully(conTB.Text), ParseCarefully(intTB.Text), ParseCarefully(wisTB.Text), ParseCarefully(chaTB.Text),
            notesTB.Text);
            strModLbl.Content = (NPC.calcMod(npc.Str) >= 0) ? "+" + NPC.calcMod(npc.Str).ToString() : NPC.calcMod(npc.Str).ToString();
            dexModLbl.Content = (NPC.calcMod(npc.Dex) >= 0) ? "+" + NPC.calcMod(npc.Dex).ToString() : NPC.calcMod(npc.Dex).ToString();
            conModLbl.Content = (NPC.calcMod(npc.Con) >= 0) ? "+" + NPC.calcMod(npc.Con).ToString() : NPC.calcMod(npc.Con).ToString();
            intModLbl.Content = (NPC.calcMod(npc.Int) >= 0) ? "+" + NPC.calcMod(npc.Int).ToString() : NPC.calcMod(npc.Int).ToString();
            wisModLbl.Content = (NPC.calcMod(npc.Wis) >= 0) ? "+" + NPC.calcMod(npc.Wis).ToString() : NPC.calcMod(npc.Wis).ToString();
            chaModLbl.Content = (NPC.calcMod(npc.Cha) >= 0) ? "+" + NPC.calcMod(npc.Cha).ToString() : NPC.calcMod(npc.Cha).ToString();

        }

        /// <summary>
        /// Delete button deletes the NPC from the global archive,
        /// consequently changing DataGrid's selection and destroying
        /// this NPC Card.
        /// </summary>
        // TODO Create custom dialog window with turning off safe delete
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.safeMode)
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
        /// Opens an extra window with the card.
        /// </summary>
        private void openExternallyBtn_Click(object sender, RoutedEventArgs e)
        {
            npc.updateInfoNotifyably(
            nameTB.Text, (Race)raceCmb.SelectedValue, genderTB.Text, ParseCarefully(ageChronoTB.Text), ParseCarefully(ageBioTB.Text),
            occupationTB.Text, placeTB.Text, charaterTB.Text, backstoryTB.Text, heightTB.Text,
            physiqueTB.Text, skincolourTB.Text, hairTB.Text, faceTB.Text, eyesTB.Text, clothesTB.Text, featuresTB.Text,
            ParseCarefully(strTB.Text), ParseCarefully(dexTB.Text), ParseCarefully(conTB.Text), ParseCarefully(intTB.Text), ParseCarefully(wisTB.Text), ParseCarefully(chaTB.Text),
            notesTB.Text);

            NPCCardWindow npcCardWindow = new NPCCardWindow(npc);
            npcCardWindow.Show();

            grid.SelectedItem = null;
        }

        /// <summary>
        /// Closing without saving.
        /// </summary>
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.safeMode)
            {
                MessageBoxResult confirmResult =
                MessageBox.Show("Close without saving?", "Confirm Closing",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmResult == MessageBoxResult.Yes)
                grid.SelectedItem = null;
            }
            else grid.SelectedItem = null;
        }

        // Two next methods are tied to chronological and biological (human)
        // ages of the NPC. Depending on which TextBox is being changed,
        // it recalculates the number in the other.
        // The logic is simple but difficult to explain. It depends on 
        // linear progression of the age in two parts of life - 
        // before and after the age of maturity and calculates a proportion
        // based on npc race and human race variables.
        
        // TODO Should I move the logic away to somewhere, maybe?
        private void ageBioTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ageBioTB.IsFocused)
                if ((Race)raceCmb.SelectedValue != null)
                    if (ageBioTB.Text.Length == 0 || ParseCarefully(ageBioTB.Text) <= 0)
                        ageChronoTB.Text = "0";
                    else
                        if (ParseCarefully(ageBioTB.Text) <= ArchiveRace.baseRace.AgeMaturity)
                        ageChronoTB.Text = ((int)Math.Round((double)(ParseCarefully(ageBioTB.Text) * ((Race)raceCmb.SelectedValue).AgeMaturity) / ArchiveRace.baseRace.AgeMaturity)).ToString();
                    else
                        ageChronoTB.Text = ((int)Math.Round((double)((ParseCarefully(ageBioTB.Text) - ArchiveRace.baseRace.AgeMaturity) * (((Race)raceCmb.SelectedValue).LifeExpectancy - ((Race)raceCmb.SelectedValue).AgeMaturity)) / (ArchiveRace.baseRace.LifeExpectancy - ArchiveRace.baseRace.AgeMaturity)) + ((Race)raceCmb.SelectedValue).AgeMaturity).ToString();
        }

        private void ageChronoTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ageChronoTB.IsFocused)
                if ((Race)raceCmb.SelectedValue != null)
                    if (ageChronoTB.Text.Length == 0 || ParseCarefully(ageChronoTB.Text) <= 0)
                        ageBioTB.Text = "0";
                    else
                        if (ParseCarefully(ageChronoTB.Text) <= ((Race)raceCmb.SelectedValue).AgeMaturity)
                        ageBioTB.Text = ((int)Math.Round((double)(ParseCarefully(ageChronoTB.Text) * ArchiveRace.baseRace.AgeMaturity) / ((Race)raceCmb.SelectedValue).AgeMaturity)).ToString();
                    else
                        ageBioTB.Text = ((int)Math.Round((double)((ParseCarefully(ageChronoTB.Text) - ((Race)raceCmb.SelectedValue).AgeMaturity) * (ArchiveRace.baseRace.LifeExpectancy - ArchiveRace.baseRace.AgeMaturity)) / (((Race)raceCmb.SelectedValue).LifeExpectancy - ((Race)raceCmb.SelectedValue).AgeMaturity)) + ArchiveRace.baseRace.AgeMaturity).ToString();
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

    }
}

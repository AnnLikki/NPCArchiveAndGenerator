using Archives;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NPCGenerator
{

    public partial class NPCCardWindow : Window
    {
        NPC npc;

        public NPCCardWindow(NPC npc)
        {
            InitializeComponent();

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

        // Save button updates the NPC's data and notifies the DataGrid to update.
        // Also recalculates the modifiers.
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            npc.updateInfoNotifyably(
            nameTB.Text, (Race)raceCmb.SelectedValue, genderTB.Text, int.Parse(ageChronoTB.Text), int.Parse(ageBioTB.Text),
            occupationTB.Text, placeTB.Text, charaterTB.Text, backstoryTB.Text, heightTB.Text,
            physiqueTB.Text, skincolourTB.Text, hairTB.Text, faceTB.Text, eyesTB.Text, clothesTB.Text, featuresTB.Text,
            int.Parse(strTB.Text), int.Parse(dexTB.Text), int.Parse(conTB.Text), int.Parse(intTB.Text), int.Parse(wisTB.Text), int.Parse(chaTB.Text),
            notesTB.Text);
            strModLbl.Content = (NPC.calcMod(npc.Str) >= 0) ? "+" + NPC.calcMod(npc.Str).ToString() : NPC.calcMod(npc.Str).ToString();
            dexModLbl.Content = (NPC.calcMod(npc.Dex) >= 0) ? "+" + NPC.calcMod(npc.Dex).ToString() : NPC.calcMod(npc.Dex).ToString();
            conModLbl.Content = (NPC.calcMod(npc.Con) >= 0) ? "+" + NPC.calcMod(npc.Con).ToString() : NPC.calcMod(npc.Con).ToString();
            intModLbl.Content = (NPC.calcMod(npc.Int) >= 0) ? "+" + NPC.calcMod(npc.Int).ToString() : NPC.calcMod(npc.Int).ToString();
            wisModLbl.Content = (NPC.calcMod(npc.Wis) >= 0) ? "+" + NPC.calcMod(npc.Wis).ToString() : NPC.calcMod(npc.Wis).ToString();
            chaModLbl.Content = (NPC.calcMod(npc.Cha) >= 0) ? "+" + NPC.calcMod(npc.Cha).ToString() : NPC.calcMod(npc.Cha).ToString();

        }

        // Delete button deletes the NPC from the global archive,
        // consequently changing DataGrid's selection and destroying
        // this NPC Card.
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            ArchiveHandler.absoluteArchiveNPC.Remove(npc);
            Close();
        }

        // Two next methods are tied to chronological and biological (human)
        // ages of the NPC. Depending on which TextBox is being changed,
        // it recalculates the number in the other.
        // The logic is simple but difficult to explain. It depends on 
        // linear progression of the age in two parts of life - 
        // before and after the age of maturity and calculates a proportion
        // based on npc race and human race variables.
        private void ageBioTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ageBioTB.IsFocused)
                if ((Race)raceCmb.SelectedValue != null)
                    if (ageBioTB.Text.Length == 0 || int.Parse(ageBioTB.Text) <= 0)
                        ageChronoTB.Text = "0";
                    else
                        if (int.Parse(ageBioTB.Text) <= ArchiveRace.baseRace.AgeMaturity)
                        ageChronoTB.Text = ((int)Math.Round((double)(int.Parse(ageBioTB.Text) * ((Race)raceCmb.SelectedValue).AgeMaturity) / ArchiveRace.baseRace.AgeMaturity)).ToString();
                    else
                        ageChronoTB.Text = ((int)Math.Round((double)((int.Parse(ageBioTB.Text) - ArchiveRace.baseRace.AgeMaturity) * (((Race)raceCmb.SelectedValue).LifeExpectancy - ((Race)raceCmb.SelectedValue).AgeMaturity)) / (ArchiveRace.baseRace.LifeExpectancy - ArchiveRace.baseRace.AgeMaturity)) + ((Race)raceCmb.SelectedValue).AgeMaturity).ToString();
        }

        private void ageChronoTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ageChronoTB.IsFocused)
                if ((Race)raceCmb.SelectedValue != null)
                    if (ageChronoTB.Text.Length == 0 || int.Parse(ageChronoTB.Text) <= 0)
                        ageBioTB.Text = "0";
                    else
                        if (int.Parse(ageChronoTB.Text) <= ((Race)raceCmb.SelectedValue).AgeMaturity)
                        ageBioTB.Text = ((int)Math.Round((double)(int.Parse(ageChronoTB.Text) * ArchiveRace.baseRace.AgeMaturity) / ((Race)raceCmb.SelectedValue).AgeMaturity)).ToString();
                    else
                        ageBioTB.Text = ((int)Math.Round((double)((int.Parse(ageChronoTB.Text) - ((Race)raceCmb.SelectedValue).AgeMaturity) * (ArchiveRace.baseRace.LifeExpectancy - ArchiveRace.baseRace.AgeMaturity)) / (((Race)raceCmb.SelectedValue).LifeExpectancy - ((Race)raceCmb.SelectedValue).AgeMaturity)) + ArchiveRace.baseRace.AgeMaturity).ToString();
        }

        // These methods check input text to Age TextBoxes so the user
        // can not input nothig except numbers in them.
        private void numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumericInput(e.Text))
                e.Handled = true;
        }
        private bool IsNumericInput(string input)
        {
            return int.TryParse(input, out _);
        }


    }

}

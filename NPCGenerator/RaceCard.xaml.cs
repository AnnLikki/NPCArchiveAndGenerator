using Archives;
using NPCArchiveAndGenerator;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NPCGenerator
{
    /// <summary>
    /// Race Card is a User Control that is placed next to the race archive 
    /// when sertain race is seleted. Allows to view and change race's data.
    /// </summary>
    public partial class RaceCard : UserControl
    {
        Race race;
        DataGrid grid;
        public RaceCard(Race race, DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;
            this.race = race;

            // Filling all the fields with race's data.
            nameTB.Text = race.Name;
            descTB.Text = race.Description;
            maturityTB.Text = race.MaturityAge.ToString();
            expectancyTB.Text = race.LifeExpectancy.ToString();
        }

        /// <summary>
        /// Save button updates the race's data and notifies the DataGrid to update.
        /// </summary>
        private void saveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            race.updateInfoNotifyably(nameTB.Text, descTB.Text, ParseCarefully(maturityTB.Text), ParseCarefully(expectancyTB.Text));

        }

        /// <summary>
        /// Delete button deletes the race from the global archive, consequently 
        /// changing DataGrid's selection and destroying this Race Card.
        /// </summary>
        private void deleteBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Controller.safeMode)
            {
                MessageBoxResult confirmResult =
                    MessageBox.Show("Are you sure you want to delete it?", "Confirm Delete",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmResult == MessageBoxResult.Yes)
                    ArchiveHandler.raceStorage.Remove(race);
            }
            else ArchiveHandler.raceStorage.Remove(race);
        }

        private void openExternallyBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO Implement opening a race in a window 
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.safeMode)
            {
                MessageBoxResult confirmResult =
                MessageBox.Show("Close without saving?", "Confirm Closing",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmResult == MessageBoxResult.Yes)
                    grid.SelectedItem = null;
            }
            else grid.SelectedItem = null;
        }

        /// <summary>
        /// These methods check input text to Age TextBoxes so the user can not input nothig except numbers in them.
        /// </summary>
        private void numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumericInput(e.Text))
                e.Handled = true;
        }
        /// <summary>
        /// These methods check input text to Age TextBoxes so the user can not input nothig except numbers in them.
        /// </summary>
        private bool IsNumericInput(string input)
        {
            return int.TryParse(input, out _);
        }
        /// <summary>
        /// These methods check input text to Age TextBoxes so the user can not input nothig except numbers in them.
        /// </summary>
        private int ParseCarefully(string s)
        {
            int result;
            int.TryParse(s, out result);
            return result;
        }

    }
}

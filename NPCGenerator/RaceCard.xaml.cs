using Archives;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NPCGenerator
{
    // Race Card is a User Control that is placed next to the race archive
    // when sertain race is seleted. Allows to view and change race's data.
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
            maturityTB.Text = race.AgeMaturity.ToString();
            expectancyTB.Text = race.LifeExpectancy.ToString();
        }

        // Save button updates the race's data and notifies the DataGrid to update.
        private void saveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            race.updateInfoNotifyably(nameTB.Text, descTB.Text, ParseCarefully(maturityTB.Text), ParseCarefully(expectancyTB.Text));

        }

        // Delete button deletes the race from the global archive,
        // consequently changing DataGrid's selection and destroying
        // this Race Card.
        private void deleteBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.safeMode)
            {
                MessageBoxResult confirmResult =
                    MessageBox.Show("Are you sure you want to delete it?", "Confirm Delete",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmResult == MessageBoxResult.Yes)
                    ArchiveHandler.absoluteArchiveRace.Remove(race);
            }
            else ArchiveHandler.absoluteArchiveRace.Remove(race);
        }

        private void openExternallyBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO Implement opening a race in a window 
        }

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
        private int ParseCarefully(string s)
        {
            int result;
            int.TryParse(s, out result);
            return result;
        }

    }
}

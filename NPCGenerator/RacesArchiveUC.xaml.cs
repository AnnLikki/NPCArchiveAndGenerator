using Archives;
using System.Windows;
using System.Windows.Controls;

namespace NPCGenerator
{
    // The race arcive inherits from UserControl,
    // so it can be placed in the center of the window interchangebly
    // with NPC archive.
    // There the global race archive is displayed via DataGrid, and
    // when its item is selected a Race Card opens to the side and
    // displayes race's data.
    public partial class RacesArchiveUC : UserControl
    {
        public RacesArchiveUC()
        {
            InitializeComponent();
            // Binding the DataGrid the global race archive.
            RaceDataGrid.ItemsSource = ArchiveHandler.archiveRace;
        }

        // A new race is created, added to the archive and selected,
        // so RaceDataGrid_SelectionChanged is triggered.
        private void addRaceBtn_Click(object sender, RoutedEventArgs e)
        {
            Race newRace = new Race();
            ArchiveHandler.archiveRace.Add(newRace);
            RaceDataGrid.SelectedItem = newRace;

        }

        // When a race is selected, a Race Card is shown with all race's data.
        // If nothing is selected, the card isn't showing.
        private void RaceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RaceDataGrid.SelectedItem is Race selectedRace)
            {
                RaceCard card = new RaceCard(selectedRace);
                RaceView.Content = card;
            }
            else
            {
                RaceView.Content = null;
            }
        }

        // Filters and searching are yet to be implemented.
        // I plan to implement this feature in the future.


    }

}

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
        ArchiveRace displayedArchiveRace;
        public RacesArchiveUC()
        {
            InitializeComponent();

            // Binding the DataGrid the filterable races archive.
            updateFilterable();

            ArchiveHandler.archiveRace.CollectionChanged += ArchiveRace_CollectionChanged;

        }

        private void ArchiveRace_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            updateFilterable();
        }

        void updateFilterable()
        {
            displayedArchiveRace = ArchiveHandler.archiveRace.filterByKey(filterTB.Text);
            RaceDataGrid.ItemsSource = displayedArchiveRace;
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

        private void filterTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateFilterable();
        }

        private void clearFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            filterTB.Clear();
        }

    }

}

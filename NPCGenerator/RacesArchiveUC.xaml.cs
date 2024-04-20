using Archives;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace NPCGenerator
{
    /// <summary>
    /// The race arcive inherits from UserControl,
    /// so it can be placed in the center of the window interchangebly
    /// with NPC archive.
    /// There the global race archive is displayed via DataGrid, and
    /// when its item is selected a Race Card opens to the side and
    /// displayes race's data.
    /// </summary>
    public partial class RacesArchiveUC : UserControl
    {
        Collection<Race> displayedArchiveRace;
        public RacesArchiveUC()
        {
            InitializeComponent();

            // Binding the DataGrid the filterable races archive.
            updateFilterable();

            ArchiveHandler.raceStorage.CollectionChanged += ArchiveRace_CollectionChanged;

        }

        private void ArchiveRace_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            updateFilterable();
        }

        public void updateFilterable()
        {
            displayedArchiveRace = ArchiveHandler.raceStorage.filterByKey(filterTB.Text);
            RaceDataGrid.ItemsSource = displayedArchiveRace;
        }
        private void UpdateFilterable(object sender, RoutedEventArgs e)
        {
            updateFilterable();
        }

        /// <summary>
        /// A new race is created, added to the archive and selected, so RaceDataGrid_SelectionChanged is triggered.
        /// </summary>
        private void addRaceBtn_Click(object sender, RoutedEventArgs e)
        {
            Race newRace = new Race("New Race");
            ArchiveHandler.raceStorage.Add(newRace);
        }

        /// <summary>
        /// When a race is selected, a Race Card is shown with all race's data. If nothing is selected, the card isn't showing.
        /// </summary>
        private void RaceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RaceDataGrid.SelectedItem is Race selectedRace)
            {
                RaceCard card = new RaceCard(selectedRace, RaceDataGrid);
                RaceView.Content = card;
                card.closeBtn.Click += UpdateFilterable;
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

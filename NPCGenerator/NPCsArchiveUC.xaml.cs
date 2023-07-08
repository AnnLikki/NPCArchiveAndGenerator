using Archives;
using System;
using System.Windows.Controls;

namespace NPCGenerator
{
    // The NPC arcive inherits from UserControl,
    // so it can be placed in the center of the window interchangebly
    // with races archive.
    // There the global NPC archive is displayed via DataGrid, and
    // when its item is selected an NPC Card opens to the side and
    // displayes NPC's data.
    public partial class NPCsArchiveUC : UserControl
    {
        ArchiveNPC displayedArchiveNPC;
        public NPCsArchiveUC()
        {
            InitializeComponent();

            // Binding the DataGrid the filterable NPC archive.
            updateFilterable();

            ArchiveHandler.absoluteArchiveNPC.CollectionChanged += AbsoluteArchiveNPC_CollectionChanged;

        }

        private void AbsoluteArchiveNPC_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            updateFilterable();
        }

        void updateFilterable()
        {
            displayedArchiveNPC = ArchiveHandler.absoluteArchiveNPC.filterByKey(filterTB.Text);
            NPCDataGrid.ItemsSource = displayedArchiveNPC;
        }

        // A new NPC is created, added to the archive and selected,
        // so NPCDataGrid_SelectionChanged is triggered.
        private void addNPCBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NPC newNPC = new NPC();
            ArchiveHandler.absoluteArchiveNPC.Add(newNPC);
            NPCDataGrid.SelectedItem = newNPC;
        }

        // When NPC is selected, an NPC Card is shown with all NPC's data.
        // If nothing is selected, the card isn't showing.
        private void NPCDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NPCDataGrid.SelectedItem is NPC selectedNPC)
            {
                NPCCard card = new NPCCard(selectedNPC);
                NPCView.Content = card;
            }
            else
            {
                NPCView.Content = null;
            }

        }

       
        private void filterTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateFilterable();
        }

        private void clearFilterBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            filterTB.Clear();
        }
    }
}

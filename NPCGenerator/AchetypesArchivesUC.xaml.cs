﻿using Archives;
using FileManager;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NPCArchiveAndGenerator
{
    public partial class ArchetypesArchivesUC : UserControl
    {
        List<Archetype> displayedArchetypes;

        public ArchetypesArchivesUC()
        {
            InitializeComponent();

            ArchiveHandler.archetypeStorage.Items.CollectionChanged += Archetypes_CollectionChanged;

            updateFilterable();
        }

        private void Archetypes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            updateFilterable();
        }
        private void Archetypes_CollectionChanged(object sender, RoutedEventArgs e)
        {
            updateFilterable();
        }
        private void Archetype_Closed(object sender, RoutedEventArgs e)
        {
            ArchetypeView.Content = null;
            ArchetypesDG.SelectedItem = null;
            DefaultArchetypeBtn.IsChecked = false;
            updateFilterable();
        }

        public void updateFilterable()
        {
            displayedArchetypes = ArchiveHandler.archetypeStorage.filterByKey(SearchTB.Text);
            ArchetypesDG.ItemsSource = displayedArchetypes;

            if (ArchiveHandler.archetypeStorage.DefaultArchetype != null)
                DefaultArchetypeBtn.Content = ArchiveHandler.archetypeStorage.DefaultArchetype.Name;
            else
                DefaultArchetypeBtn.Content = "Default Archetype isn't set";
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateFilterable();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ArchiveHandler.archetypeStorage.Add(new Archetype("New Archetype"));
        }

        private void ArchetypesDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ArchetypesDG.SelectedItem is Archetype selectedArchetype)
            {
                ArchetypeCard card = new ArchetypeCard(selectedArchetype, false, ArchetypesDG);
                ArchetypeView.Content = card;
                card.MakeDefaultBtn.Click += Archetypes_CollectionChanged;
                card.closeBtn.Click += Archetype_Closed;
                DefaultArchetypeBtn.IsChecked = false;
            }
            else if (DefaultArchetypeBtn.IsChecked == false)
            {
                ArchetypeView.Content = null;
            }
            updateFilterable();
        }

        private void DefaultArchiveBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (ArchiveHandler.archetypeStorage.DefaultArchetype != null)
            {
                ArchetypeCard card = new ArchetypeCard(ArchiveHandler.archetypeStorage.DefaultArchetype, true, ArchetypesDG);
                ArchetypeView.Content = card;
                card.closeBtn.Click += Archetype_Closed;
                ArchetypesDG.SelectedItem = null;

                updateFilterable();
            }
            else
                DefaultArchetypeBtn.IsChecked = false;
        }

        private void clearFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchTB.Text = "";
        }
        private void closeArchviveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.safeMode)
            {
                MessageBoxResult result = MessageBox.Show("Close without saving?", "Saving",
                   MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    SnL.ArchetypesSavePath = null;
                    ArchiveHandler.archetypeStorage.Clear();
                }
            }
            updateFilterable();
            Controller.UpdateFileName();
        }
    }
}

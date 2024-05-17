using Archives;
using FileManager;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static Archives.Enums;

namespace NPCArchiveAndGenerator
{
    public partial class BundlesArchivesUC : UserControl
    {

        BundleType state;
        List<Bundle> displayedBundles;

        public BundlesArchivesUC()
        {
            InitializeComponent();

            foreach (BundleType type in (BundleType[])Enum.GetValues(typeof(BundleType)))
                ArchiveHandler.bundleStorage[type].CollectionChanged += Bundles_CollectionChanged;

            NamesBtn.IsChecked = true;
            state = BundleType.Name;
            updateFilterable();

        }

        private void Bundles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            updateFilterable();
        }

        public void updateFilterable()
        {
            displayedBundles = ArchiveHandler.bundleStorage.filterByKey(state, BundleSearchTB.Text);
            BundlesDG.ItemsSource = displayedBundles;
            updateCatgoriesCount();
        }

        private void updateCatgoriesCount()
        {
            NamesBtn.Content = "Names (" + ArchiveHandler.bundleStorage[BundleType.Name].Count + ")";
            OccupationsBtn.Content = "Occupations (" + ArchiveHandler.bundleStorage[BundleType.Occupation].Count + ")";
            CharactersBtn.Content = "Characters (" + ArchiveHandler.bundleStorage[BundleType.Character].Count + ")";
            HeightsBtn.Content = "Heights (" + ArchiveHandler.bundleStorage[BundleType.Height].Count + ")";
            PhysiquesBtn.Content = "Physiques (" + ArchiveHandler.bundleStorage[BundleType.Physique].Count + ")";
            SkinsBtn.Content = "Skins (" + ArchiveHandler.bundleStorage[BundleType.Skin].Count + ")";
            HairsBtn.Content = "Hairs (" + ArchiveHandler.bundleStorage[BundleType.Hair].Count + ")";
            FacesBtn.Content = "Faces (" + ArchiveHandler.bundleStorage[BundleType.Face].Count + ")";
            EyesBtn.Content = "Eyes (" + ArchiveHandler.bundleStorage[BundleType.Eyes].Count + ")";
            ClothesBtn.Content = "Clothes (" + ArchiveHandler.bundleStorage[BundleType.Clothes].Count + ")";
            FeaturesBtn.Content = "Features (" + ArchiveHandler.bundleStorage[BundleType.Features].Count + ")";
        }

        private void CategoryBtn_Pressed(object sender, RoutedEventArgs e)
        {
            RadioButton clickedButton = sender as RadioButton;
            if (clickedButton != null)
            {
                switch (clickedButton.Name)
                {
                    case "NamesBtn":
                        state = BundleType.Name;
                        break;
                    case "OccupationsBtn":
                        state = BundleType.Occupation;
                        break;
                    case "CharactersBtn":
                        state = BundleType.Character;
                        break;
                    case "HeightsBtn":
                        state = BundleType.Height;
                        break;
                    case "PhysiquesBtn":
                        state = BundleType.Physique;
                        break;
                    case "SkinsBtn":
                        state = BundleType.Skin;
                        break;
                    case "HairsBtn":
                        state = BundleType.Hair;
                        break;
                    case "FacesBtn":
                        state = BundleType.Face;
                        break;
                    case "EyesBtn":
                        state = BundleType.Eyes;
                        break;
                    case "ClothesBtn":
                        state = BundleType.Clothes;
                        break;
                    case "FeaturesBtn":
                        state = BundleType.Features;
                        break;
                }
                updateFilterable();

            }
        }

        private void BundleSearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateFilterable();
        }
        private void AddBundleBtn_Click(object sender, RoutedEventArgs e)
        {
            ArchiveHandler.bundleStorage.Add(state, new Bundle("New Bundle (" + state.ToString() + ")"));
        }

        private void BundlesDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BundlesDG.SelectedItem is Bundle selectedBundle)
            {
                BundleCard card = new BundleCard(state, selectedBundle, BundlesDG);
                BundleView.Content = card;
            }
            else
            {
                BundleView.Content = null;
            }
            updateFilterable();
        }

        private void clearFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            BundleSearchTB.Text = "";
        }

        private void closeArchviveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Controller.safeMode)
            {
                MessageBoxResult result = MessageBox.Show("Close without saving?", "Saving",
                   MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    SnL.BundlesSavePath = null;
                    ArchiveHandler.bundleStorage.ClearAllBundles();
                }
            }
            Controller.UpdateFileName();
        }
    }
}

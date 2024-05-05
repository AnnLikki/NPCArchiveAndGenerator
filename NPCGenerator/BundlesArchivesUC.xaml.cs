using Archives;
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
    }
}

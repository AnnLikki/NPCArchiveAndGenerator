using Archives;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using static Archives.Enums;

namespace NPCArchiveAndGenerator
{
    public partial class ArchetypeCard : UserControl
    {
        Archetype archetype;
        bool isDefault;

        public ArchetypeCard(Archetype archetype1, bool isDefault1, DataGrid grid1)
        {

            archetype = archetype1;
            isDefault = isDefault1;

            AgesPercentageConverterForArchetype converter1 = new AgesPercentageConverterForArchetype();
            converter1.Archetype = archetype;
            Resources.Add("AgesPercentageConverter", converter1);

            BundlesPercentageConverterForArchetype converter2 = new BundlesPercentageConverterForArchetype();
            converter2.Archetype = archetype;
            Resources.Add("BundlesPercentageConverter", converter2);

            RacesPercentageConverterForArchetype converter3 = new RacesPercentageConverterForArchetype();
            converter3.Archetype = archetype;
            Resources.Add("RacesPercentageConverter", converter3);

            InitializeComponent();

            NameTB.Text = archetype.Name;
            MaleWeightTB.Text = archetype.Genders.GetWeight(Gender.Male).ToString();
            FemaleWeightTB.Text = archetype.Genders.GetWeight(Gender.Female).ToString();
            NeutralWeightTB.Text = archetype.Genders.GetWeight(Gender.Neutral).ToString();

            recalcPercent();

            if (isDefault)
            {
                MakeDefaultBtn.Visibility = Visibility.Collapsed;
                deleteBtn.Visibility = Visibility.Hidden;
            }

            //archetype.Ages.CollectionChanged += AgeRanges_CollectionChanged;
            updateAgeRanges();
            updateBundles();
            updateRaces();
        }

        public void recalcPercent()
        {
            MalePercentLbl.Content = archetype.Genders.GetPercentage(Gender.Male) + "%";
            FemalePercentLbl.Content = archetype.Genders.GetPercentage(Gender.Female) + "%";
            NeutralPercentLbl.Content = archetype.Genders.GetPercentage(Gender.Neutral) + "%";
        }
        private void cloneBtn_Click(object sender, RoutedEventArgs e)
        {
            ArchiveHandler.archetypeStorage.Duplicate(archetype);
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e) { }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            ArchiveHandler.archetypeStorage.Remove(archetype);
        }

        private void NameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            archetype.Name = NameTB.Text;
        }

        private void MakeDefaultRtn_Click(object sender, RoutedEventArgs e)
        {
            ArchiveHandler.archetypeStorage.SetDefaultArchetype(archetype);
        }

        private void AddRangeBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveRangesBtn_Click(null, null);
            archetype.Ages.AddRange(archetype.Ages.MaxAge() + 1, archetype.Ages.MaxAge() + 2, archetype.Ages.MaxWeight() + 1);
            updateAgeRanges();
        }

        private void DeleteRangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is WeightedElement el)
            {
                var container = AgesIC.ItemContainerGenerator.ContainerFromItem(el) as FrameworkElement;
                if (container != null)
                {
                    var fromTB = FindChild<TextBox>(container, "FromTB");
                    var toTB = FindChild<TextBox>(container, "ToTB");

                    if (fromTB != null && toTB != null &&
                        int.TryParse(fromTB.Text, out int fromValue) &&
                        int.TryParse(toTB.Text, out int toValue))
                    {
                        archetype.Ages.RemoveRange(fromValue, toValue);
                    }
                }
                updateAgeRanges();
            }
        }

        private void SaveRangesBtn_Click(object sender, RoutedEventArgs e)
        {
            archetype.Ages.Clear();
            foreach (var item in AgesIC.Items)
            {
                // Get the container for the item
                var container = AgesIC.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                if (container != null)
                {
                    // Find the TextBox controls inside the container
                    var fromTB = FindChild<TextBox>(container, "FromTB");
                    var toTB = FindChild<TextBox>(container, "ToTB");
                    var weightTB = FindChild<TextBox>(container, "RangeWeightTB");

                    // Check if TextBoxes were found and extract their values
                    if (fromTB != null && toTB != null && weightTB != null &&
                        int.TryParse(fromTB.Text, out int fromValue) &&
                        int.TryParse(toTB.Text, out int toValue) &&
                        int.TryParse(weightTB.Text, out int weight))
                    {
                        archetype.Ages.AddRange(fromValue, toValue, weight);
                    }
                }
            }
            updateAgeRanges();
        }

        private void updateAgeRanges()
        {
            AgesIC.ItemsSource = null;
            AgesIC.ItemsSource = archetype.Ages.GetRanges();
            EmptyAgeRangesImg.Visibility = (archetype.Ages.Count == 0) ? Visibility.Visible : Visibility.Collapsed;
            unhighlightAgeRangeBecauseSaved();
        }

        private void agerange_TextChanged(object sender, TextChangedEventArgs e)
        {
            highlightAgeRangeNotSaved();
        }

        void highlightAgeRangeNotSaved()
        {
            SaveRangesBtn.Background = Brushes.Orange;
        }
        void unhighlightAgeRangeBecauseSaved()
        {
            SaveRangesBtn.Background = Brushes.LightGray;
        }



        private void AddRaceBtn_Click(object sender, RoutedEventArgs e)
        {
            RaceSelectionWin selectionWin = new RaceSelectionWin(archetype);
            selectionWin.Show();
            selectionWin.Closed += Update_Races;
        }

        private void UpdateRacesBtn_Click(object sender, RoutedEventArgs e)
        {
            updateRaces();
        }

        private void DeleteRaceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is WeightedElement el)
            {
                archetype.Races.Remove(el);
                updateRaces();
            }
        }

        private void updateRaces()
        {
            RacesIC.ItemsSource = null;
            RacesIC.ItemsSource = archetype.Races;
            EmptyRacesImg.Visibility = (archetype.Races.Count == 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Update_Races(object sender, EventArgs e)
        {
            updateRaces();
        }


        private void AddBundleBtn_Click(object sender, RoutedEventArgs e)
        {
            BundleType? type = null;
            if (sender is Button button)
            {
                if (button.Name == "AddNamesBundleBtn")
                    type = BundleType.Name;
                if (button.Name == "AddOccupationsBundleBtn")
                    type = BundleType.Occupation;
                if (button.Name == "AddCharactersBundleBtn")
                    type = BundleType.Character;
                if (button.Name == "AddHeightsBundleBtn")
                    type = BundleType.Height;
                if (button.Name == "AddPhysiquesBundleBtn")
                    type = BundleType.Physique;
                if (button.Name == "AddSkinsBundleBtn")
                    type = BundleType.Skin;
                if (button.Name == "AddHairsBundleBtn")
                    type = BundleType.Hair;
                if (button.Name == "AddFacesBundleBtn")
                    type = BundleType.Face;
                if (button.Name == "AddEyesBundleBtn")
                    type = BundleType.Eyes;
                if (button.Name == "AddClothesBundleBtn")
                    type = BundleType.Clothes;
                if (button.Name == "AddFeaturesBundleBtn")
                    type = BundleType.Features;
            }
            if (type != null)
            {
                BundleSelectionWin selectionWin = new BundleSelectionWin((BundleType)type, archetype);
                selectionWin.Show();
                selectionWin.Closed += Update_Bundles;
            }
        }

        private void updateBundles(BundleType? type = null)
        {
            switch (type)
            {
                case BundleType.Name:
                    NamesBundlesIC.ItemsSource = null;
                    NamesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Name];
                    break;
                case BundleType.Occupation:
                    OccupationsBundlesIC.ItemsSource = null;
                    OccupationsBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Occupation];
                    break;
                case BundleType.Character:
                    CharactersBundlesIC.ItemsSource = null;
                    CharactersBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Character];
                    break;
                case BundleType.Height:
                    HeightsBundlesIC.ItemsSource = null;
                    HeightsBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Height];
                    break;
                case BundleType.Physique:
                    PhysiquesBundlesIC.ItemsSource = null;
                    PhysiquesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Physique];
                    break;
                case BundleType.Skin:
                    SkinsBundlesIC.ItemsSource = null;
                    SkinsBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Skin];
                    break;
                case BundleType.Hair:
                    HairsBundlesIC.ItemsSource = null;
                    HairsBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Hair];
                    break;
                case BundleType.Face:
                    FacesBundlesIC.ItemsSource = null;
                    FacesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Face];
                    break;
                case BundleType.Eyes:
                    EyesBundlesIC.ItemsSource = null;
                    EyesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Eyes];
                    break;
                case BundleType.Clothes:
                    ClothesBundlesIC.ItemsSource = null;
                    ClothesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Clothes];
                    break;
                case BundleType.Features:
                    FeaturesBundlesIC.ItemsSource = null;
                    FeaturesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Features];
                    break;
                case null:
                    NamesBundlesIC.ItemsSource = null;
                    NamesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Name];
                    OccupationsBundlesIC.ItemsSource = null;
                    OccupationsBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Occupation];
                    CharactersBundlesIC.ItemsSource = null;
                    CharactersBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Character];
                    HeightsBundlesIC.ItemsSource = null;
                    HeightsBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Height];
                    PhysiquesBundlesIC.ItemsSource = null;
                    PhysiquesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Physique];
                    SkinsBundlesIC.ItemsSource = null;
                    SkinsBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Skin];
                    HairsBundlesIC.ItemsSource = null;
                    HairsBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Hair];
                    FacesBundlesIC.ItemsSource = null;
                    FacesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Face];
                    EyesBundlesIC.ItemsSource = null;
                    EyesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Eyes];
                    ClothesBundlesIC.ItemsSource = null;
                    ClothesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Clothes];
                    FeaturesBundlesIC.ItemsSource = null;
                    FeaturesBundlesIC.ItemsSource = archetype.CompatableBundles[BundleType.Features];
                    break;
            }

        }
        private void UpdateBundlesBtn_Click(object sender, RoutedEventArgs e)
        {
            updateBundles();
        }

        private void Update_Bundles(object sender, EventArgs e)
        {
            updateBundles();
        }

        private void DeleteBundleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is WeightedElement el)
            {
                switch (button.Name)
                {
                    case "DeleteNamesBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Name, (Guid)el.Value);
                        updateBundles(BundleType.Name);
                        break;
                    case "DeleteOccupationsBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Occupation, (Guid)el.Value);
                        updateBundles(BundleType.Occupation);
                        break;
                    case "DeleteCharactersBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Character, (Guid)el.Value);
                        updateBundles(BundleType.Character);
                        break;
                    case "DeleteHeightsBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Height, (Guid)el.Value);
                        updateBundles(BundleType.Height);
                        break;
                    case "DeletePhysiquesBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Physique, (Guid)el.Value);
                        updateBundles(BundleType.Physique);
                        break;
                    case "DeleteSkinsBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Skin, (Guid)el.Value);
                        updateBundles(BundleType.Skin);
                        break;
                    case "DeleteHairsBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Hair, (Guid)el.Value);
                        updateBundles(BundleType.Hair);
                        break;
                    case "DeleteFacesBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Face, (Guid)el.Value);
                        updateBundles(BundleType.Face);
                        break;
                    case "DeleteEyesBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Eyes, (Guid)el.Value);
                        updateBundles(BundleType.Eyes);
                        break;
                    case "DeleteClothesBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Clothes, (Guid)el.Value);
                        updateBundles(BundleType.Clothes);
                        break;
                    case "DeleteFeaturesBundleBtn":
                        archetype.CompatableBundles.RemoveBundle(BundleType.Features, (Guid)el.Value);
                        updateBundles(BundleType.Features);
                        break;
                    default:
                        throw new Exception("INVESTIGATE");
                }

            }

        }



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

        private void MaleWeightTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            archetype.SetGender(Gender.Male, ParseCarefully(MaleWeightTB.Text));
            recalcPercent();
        }

        private void FemaleWeightTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            archetype.SetGender(Gender.Female, ParseCarefully(FemaleWeightTB.Text));
            recalcPercent();
        }

        private void NeutralWeightTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            archetype.SetGender(Gender.Neutral, ParseCarefully(NeutralWeightTB.Text));
            recalcPercent();
        }




        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

    }

    public class AgesPercentageConverterForArchetype : IValueConverter
    {
        public Archetype Archetype { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WeightedElement item && Archetype != null)
            {
                string percent;
                percent = Archetype.Ages.GetRangePercentage(((Tuple<int, int>)item.Value).Item1, ((Tuple<int, int>)item.Value).Item2) + "%";
                return percent;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RaceIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WeightedElement item)
            {
                Race race = ArchiveHandler.raceStorage.FindRace(Guid.Parse(item.Value.ToString()));
                string name = null;
                if (race != null)
                    name = race.Name;
                if (name != null)
                    return name;
            }
            return "Error";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class RacesPercentageConverterForArchetype : IValueConverter
    {
        public Archetype Archetype { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WeightedElement item && Archetype != null)
            {
                string percent;
                percent = Archetype.Races.GetPercentage(item.Value) + "%";
                return percent;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BundleIdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WeightedElement item && parameter is string typeNumber)
            {
                BundleType type = (BundleType)int.Parse(typeNumber);
                Bundle bundle = ArchiveHandler.bundleStorage.FindBundle(type, (Guid)item.Value);
                string name = null;
                if (bundle != null)
                    name = bundle.Name;
                if (name != null)
                    return name;
            }
            return "Error";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class BundlesPercentageConverterForArchetype : IValueConverter
    {
        public Archetype Archetype { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WeightedElement item && Archetype != null && parameter is string category)
            {
                BundleType type = (BundleType)int.Parse(category);
                string percent;
                percent = Archetype.CompatableBundles[type].GetPercentage(item.Value) + "%";
                return percent;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

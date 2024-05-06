using Archives;
using NPCArchiveAndGenerator;
using System;
using System.Globalization;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using static Archives.Enums;

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
            this.grid = grid;
            this.race = race;

            AgesPercentageConverterForRace converter1 = new AgesPercentageConverterForRace();
            converter1.Race = this.race;
            Resources.Add("AgesPercentageConverter", converter1);

            BundlesPercentageConverterForRace converter2 = new BundlesPercentageConverterForRace();
            converter2.Race = this.race;
            Resources.Add("BundlesPercentageConverter", converter2);

            InitializeComponent();

            nameTB.Text = race.Name;
            descTB.Text = race.Description;
            maturityTB.Text = race.MaturityAge.ToString();
            expectancyTB.Text = race.LifeExpectancy.ToString();

            MaleWeightTB.Text = race.Genders.GetWeight(Gender.Male).ToString();
            FemaleWeightTB.Text = race.Genders.GetWeight(Gender.Female).ToString();
            NeutralWeightTB.Text = race.Genders.GetWeight(Gender.Neutral).ToString();

            recalcPercent();
            updateAgeRanges();
            updateBundles();
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
        private void cloneBtn_Click(object sender, RoutedEventArgs e)
        {
            ArchiveHandler.raceStorage.Duplicate(race);
        }

        public void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            grid.SelectedItem = null;
        }

        private void nameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            race.Name = nameTB.Text;
        }

        private void descTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            race.Description = descTB.Text;
        }

        private void maturityTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            race.MaturityAge = ParseCarefully(maturityTB.Text);
        }

        private void expectancyTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            race.LifeExpectancy = ParseCarefully(expectancyTB.Text);
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



        private void MaleWeightTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            race.SetGender(Gender.Male, ParseCarefully(MaleWeightTB.Text));
            recalcPercent();
        }

        private void FemaleWeightTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            race.SetGender(Gender.Female, ParseCarefully(FemaleWeightTB.Text));
            recalcPercent();
        }

        private void NeutralWeightTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            race.SetGender(Gender.Neutral, ParseCarefully(NeutralWeightTB.Text));
            recalcPercent();
        }

        public void recalcPercent()
        {
            MalePercentLbl.Content = race.Genders.GetPercentage(Gender.Male) + "%";
            FemalePercentLbl.Content = race.Genders.GetPercentage(Gender.Female) + "%";
            NeutralPercentLbl.Content = race.Genders.GetPercentage(Gender.Neutral) + "%";
        }


        private void AddRangeBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveRangesBtn_Click(null, null);
            race.Ages.AddRange(race.Ages.MaxAge() + 1, race.Ages.MaxAge() + 2, race.Ages.MaxWeight() + 1);
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
                        race.Ages.RemoveRange(fromValue, toValue);
                    }
                }
                updateAgeRanges();
            }
        }

        private void SaveRangesBtn_Click(object sender, RoutedEventArgs e)
        {
            race.Ages.Clear();
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
                        race.Ages.AddRange(fromValue, toValue, weight);
                    }
                }
            }
            updateAgeRanges();
        }

        private void updateAgeRanges()
        {
            AgesIC.ItemsSource = null;
            AgesIC.ItemsSource = race.Ages.GetRanges();
            EmptyAgeRangesImg.Visibility = (race.Ages.Count == 0) ? Visibility.Visible : Visibility.Collapsed;
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


        private void AddBundleBtn_Click(object sender, RoutedEventArgs e)
        {
            BundleType? type = null;
            if (sender is Button button)
            {
                if (button.Name == "AddNamesBundleBtn")
                    type = BundleType.Name;
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
                if (button.Name == "AddFeaturesBundleBtn")
                    type = BundleType.Features;
            }
            if (type != null)
            {
                BundleSelectionWin selectionWin = new BundleSelectionWin((BundleType)type, race);
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
                    NamesBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Name];
                    break;
                case BundleType.Character:
                    CharactersBundlesIC.ItemsSource = null;
                    CharactersBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Character];
                    break;
                case BundleType.Height:
                    HeightsBundlesIC.ItemsSource = null;
                    HeightsBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Height];
                    break;
                case BundleType.Physique:
                    PhysiquesBundlesIC.ItemsSource = null;
                    PhysiquesBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Physique];
                    break;
                case BundleType.Skin:
                    SkinsBundlesIC.ItemsSource = null;
                    SkinsBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Skin];
                    break;
                case BundleType.Hair:
                    HairsBundlesIC.ItemsSource = null;
                    HairsBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Hair];
                    break;
                case BundleType.Face:
                    FacesBundlesIC.ItemsSource = null;
                    FacesBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Face];
                    break;
                case BundleType.Eyes:
                    EyesBundlesIC.ItemsSource = null;
                    EyesBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Eyes];
                    break;
                case BundleType.Features:
                    FeaturesBundlesIC.ItemsSource = null;
                    FeaturesBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Features];
                    break;
                case null:
                    NamesBundlesIC.ItemsSource = null;
                    NamesBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Name];
                    CharactersBundlesIC.ItemsSource = null;
                    CharactersBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Character];
                    HeightsBundlesIC.ItemsSource = null;
                    HeightsBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Height];
                    PhysiquesBundlesIC.ItemsSource = null;
                    PhysiquesBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Physique];
                    SkinsBundlesIC.ItemsSource = null;
                    SkinsBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Skin];
                    HairsBundlesIC.ItemsSource = null;
                    HairsBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Hair];
                    FacesBundlesIC.ItemsSource = null;
                    FacesBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Face];
                    EyesBundlesIC.ItemsSource = null;
                    EyesBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Eyes];
                    FeaturesBundlesIC.ItemsSource = null;
                    FeaturesBundlesIC.ItemsSource = race.CompatableBundles[BundleType.Features];
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
                        race.CompatableBundles.RemoveBundle(BundleType.Name, (Guid)el.Value);
                        updateBundles(BundleType.Name);
                        break;
                    case "DeleteCharactersBundleBtn":
                        race.CompatableBundles.RemoveBundle(BundleType.Character, (Guid)el.Value);
                        updateBundles(BundleType.Character);
                        break;
                    case "DeleteHeightsBundleBtn":
                        race.CompatableBundles.RemoveBundle(BundleType.Height, (Guid)el.Value);
                        updateBundles(BundleType.Height);
                        break;
                    case "DeletePhysiquesBundleBtn":
                        race.CompatableBundles.RemoveBundle(BundleType.Physique, (Guid)el.Value);
                        updateBundles(BundleType.Physique);
                        break;
                    case "DeleteSkinsBundleBtn":
                        race.CompatableBundles.RemoveBundle(BundleType.Skin, (Guid)el.Value);
                        updateBundles(BundleType.Skin);
                        break;
                    case "DeleteHairsBundleBtn":
                        race.CompatableBundles.RemoveBundle(BundleType.Hair, (Guid)el.Value);
                        updateBundles(BundleType.Hair);
                        break;
                    case "DeleteFacesBundleBtn":
                        race.CompatableBundles.RemoveBundle(BundleType.Face, (Guid)el.Value);
                        updateBundles(BundleType.Face);
                        break;
                    case "DeleteEyesBundleBtn":
                        race.CompatableBundles.RemoveBundle(BundleType.Eyes, (Guid)el.Value);
                        updateBundles(BundleType.Eyes);
                        break;
                    case "DeleteFeaturesBundleBtn":
                        race.CompatableBundles.RemoveBundle(BundleType.Features, (Guid)el.Value);
                        updateBundles(BundleType.Features);
                        break;
                    default:
                        throw new Exception("INVESTIGATE");
                }

            }

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


    public class AgesPercentageConverterForRace : IValueConverter
    {
        public Race Race { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WeightedElement item && Race != null)
            {
                string percent;
                percent = Race.Ages.GetRangePercentage(((Tuple<int, int>)item.Value).Item1, ((Tuple<int, int>)item.Value).Item2) + "%";
                return percent;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BundlesPercentageConverterForRace : IValueConverter
    {
        public Race Race { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WeightedElement item && Race != null && parameter is string category)
            {
                BundleType type = (BundleType)int.Parse(category);
                string percent;
                percent = Race.CompatableBundles[type].GetPercentage(item.Value) + "%";
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

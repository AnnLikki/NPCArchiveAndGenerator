using Archives;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static Archives.Enums;

namespace NPCArchiveAndGenerator
{
    public partial class BundleCard : UserControl
    {
        Bundle bundle;
        BundleType type;
        DataGrid grid;

        public BundleCard(BundleType type1, Bundle bundle1, DataGrid grid1)
        {
            bundle = bundle1;
            type = type1;
            grid = grid1;

            InitializeComponent();

            BundleNameTB.Text = bundle.Name;

            CategoryCmb.ItemsSource = Enum.GetValues(typeof(BundleType));
            CategoryCmb.SelectedItem = type;

            IndependentLayersChb.IsChecked = bundle.IndependentLayers;

            if (bundle.Gender == Gender.Male)
                MaleBtn.IsChecked = true;
            else if (bundle.Gender == Gender.Female)
                FemaleBtn.IsChecked = true;
            else
                NeutralBtn.IsChecked = true;

            if (bundle.LowerAgeLimit > 0)
                AgeFromTB.Text = bundle.LowerAgeLimit.ToString();
            if (bundle.UpperAgeLimit < int.MaxValue)
                AgeToTB.Text = bundle.UpperAgeLimit.ToString();

            updateLayers();


        }

        private void cloneBtn_Click(object sender, RoutedEventArgs e)
        {
            ArchiveHandler.bundleStorage.Duplicate(type, bundle);
        }
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            grid.SelectedItem = null;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            ArchiveHandler.bundleStorage.Remove(type, bundle);
        }


        public void updateLayers()
        {
            LayersIC.ItemsSource = null;
            LayersIC.ItemsSource = bundle.Layers;
        }

        private void BundleNameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            bundle.Name = BundleNameTB.Text;
        }


        private void MoveToCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            ArchiveHandler.bundleStorage.Add((BundleType)CategoryCmb.SelectedValue, bundle);
            ArchiveHandler.bundleStorage.Remove(type, bundle);
        }


        private void MaleBtn_Checked(object sender, RoutedEventArgs e)
        {
            bundle.Gender = Gender.Male;
        }

        private void FemaleBtn_Checked(object sender, RoutedEventArgs e)
        {
            bundle.Gender = Gender.Female;
        }

        private void NeutralBtn_Checked(object sender, RoutedEventArgs e)
        {
            bundle.Gender = Gender.Neutral;
        }

        private void AgeFromTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ParseCarefully(AgeFromTB.Text) > 0 && ParseCarefully(AgeFromTB.Text) < int.MaxValue)
                bundle.LowerAgeLimit = ParseCarefully(AgeFromTB.Text);
            else
                bundle.LowerAgeLimit = 0;
        }

        private void AgeToTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ParseCarefully(AgeToTB.Text) < int.MaxValue && ParseCarefully(AgeToTB.Text) > 0)
                bundle.UpperAgeLimit = ParseCarefully(AgeToTB.Text);
            else
                bundle.UpperAgeLimit = int.MaxValue;
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


        private void AddLayerBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bundle.InsertNewLayer(bundle.Count);
            updateLayers();
        }

        private void MoveUpBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Layer layer)
            {
                if (bundle.MoveLayerUp(layer))
                    updateLayers();
            }
        }

        private void MoveDownBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Layer layer)
            {
                if (bundle.MoveLayerDown(layer))
                    updateLayers();
            }
        }


        private void DuplicateLayerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Layer layer)
            {
                bundle.Duplicate(layer);
                updateLayers();
            }
        }

        private void EditLayerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Layer layer)
            {
                LayerEditWin layerEdit = new LayerEditWin(layer);
                layerEdit.Closed += LayerEdit_Closed;
                layerEdit.Show();
            }
        }

        private void LayerEdit_Closed(object sender, EventArgs e)
        {
            updateLayers();
        }

        private void DeleteLayerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Layer layer)
            {
                bundle.RemoveLayer(layer);
                updateLayers();
            }
        }

        private void IndependentLayersChb_Changed(object sender, RoutedEventArgs e)
        {
            bundle.IndependentLayers = IndependentLayersChb.IsChecked == true ? true : false;
        }

    }

    public class EnumToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Gender gender)
            {
                switch (gender)
                {
                    case Gender.Male:
                        return new BitmapImage(new Uri("/Images/male.png", UriKind.Relative));
                    case Gender.Female:
                        return new BitmapImage(new Uri("/Images/female.png", UriKind.Relative));
                    case Gender.Neutral:
                        return new BitmapImage(new Uri("/Images/neuter.png", UriKind.Relative));
                    default:
                        return null;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AgeLimitVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int ageLimit && ageLimit > 0 && ageLimit < int.MaxValue)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringLengthVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && !string.IsNullOrEmpty(str))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ElemsPercentageConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 2 && values[0] is Layer layer && values[1] is WeightedElement element && parameter is string gender)
            {
                string percent;
                switch (gender)
                {
                    case "Male":
                        percent = layer.GetPercentage(element, Gender.Male) + "%";
                        break;
                    case "Female":
                        percent = layer.GetPercentage(element, Gender.Female) + "%";
                        break;
                    case "Neutral":
                        percent = layer.GetPercentage(element, Gender.Neutral) + "%";
                        break;
                    default:
                        percent = "N/A%";
                        break;
                }
                return percent;
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }



}
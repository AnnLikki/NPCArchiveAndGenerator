using Archives;
using FileManager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using static Archives.Enums;

namespace NPCArchiveAndGenerator
{
    public partial class LayerEditWin : Window
    {
        Layer layer;

        public LayerEditWin(Layer layer1)
        {

            layer = layer1;

            LayerElemsPercentageConverter converter = new LayerElemsPercentageConverter();
            converter.Layer = layer;
            Resources.Add("LayerElemsPercentageConverter", converter);

            InitializeComponent();

            ChanceTB.Text = layer.Chance.ToString();
            DefaultTB.Text = layer.DefaultValue;

            if (layer.Gender == Gender.Male)
                MaleBtn.IsChecked = true;
            else if (layer.Gender == Gender.Female)
                FemaleBtn.IsChecked = true;
            else
                NeutralBtn.IsChecked = true;

            if (layer.LowerAgeLimit > 0)
                AgeFromTB.Text = layer.LowerAgeLimit.ToString();
            if (layer.UpperAgeLimit < int.MaxValue)
                AgeToTB.Text = layer.UpperAgeLimit.ToString();

            updateElems();

        }

        private void ChanceIncrement_Click(object sender, RoutedEventArgs e)
        {
            if (layer.Chance < 1)
                layer.Chance = Math.Round(layer.Chance + 0.05, 2);
            if (layer.Chance > 1)
                layer.Chance = 1;
            ChanceTB.Text = layer.Chance.ToString();
        }

        private void ChanceDecrement_Click(object sender, RoutedEventArgs e)
        {
            if (layer.Chance > 0)
                layer.Chance = Math.Round(layer.Chance - 0.05, 2);
            if (layer.Chance < 0)
                layer.Chance = 0;
            ChanceTB.Text = layer.Chance.ToString();
        }

        private void DefaultTB_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            layer.DefaultValue = DefaultTB.Text;
        }


        private void MaleBtn_Checked(object sender, RoutedEventArgs e)
        {
            layer.Gender = Gender.Male;
        }

        private void FemaleBtn_Checked(object sender, RoutedEventArgs e)
        {
            layer.Gender = Gender.Female;
        }

        private void NeutralBtn_Checked(object sender, RoutedEventArgs e)
        {
            layer.Gender = Gender.Neutral;
        }


        private void AgeFromTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ParseCarefully(AgeFromTB.Text) > 0 && ParseCarefully(AgeFromTB.Text) < int.MaxValue)
                layer.LowerAgeLimit = ParseCarefully(AgeFromTB.Text);
            else
                layer.LowerAgeLimit = 0;
        }

        private void AgeToTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ParseCarefully(AgeToTB.Text) < int.MaxValue && ParseCarefully(AgeToTB.Text) > 0)
                layer.UpperAgeLimit = ParseCarefully(AgeToTB.Text);
            else
                layer.UpperAgeLimit = int.MaxValue;
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




        public void updateElems()
        {
            ElemsDG.ItemsSource = null;
            ElemsDG.ItemsSource = layer.Elements;
        }

        private void AddElementBtn_Click(object sender, RoutedEventArgs e)
        {
            layer.AddToStart("");
            updateElems();
        }

        private void ImportElementsBtn_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<WeightedElement> data;
            if (DataPorter.importViaDialog("Import Layer from a text file...", out data))
            {
                layer.AddAll(data);
                updateElems();
            }
        }

        private void ExportElementsBtn_Click(object sender, RoutedEventArgs e)
        {
            DataPorter.exportViaDialog(layer.Elements, "Export Layer to file...", "Layer Data");
        }


        private void DeleteElemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is WeightedElement element)
            {
                layer.Remove(element);
                updateElems();
            }
        }

        private void ValueTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox valueTB && valueTB.DataContext is WeightedElement element)
            {
                element.Value = valueTB.Text;
            }
        }

        private void MaleBtn_Checked_1(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton button && button.DataContext is WeightedElement element)
            {
                element.Gender = Gender.Male;
            }
        }

        private void FemaleBtn_Checked_1(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton button && button.DataContext is WeightedElement element)
            {
                element.Gender = Gender.Female;
            }
        }

        private void NeutralBtn_Checked_1(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton button && button.DataContext is WeightedElement element)
            {
                element.Gender = Gender.Neutral;
            }
        }

        private void WeightTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox weightTB && weightTB.DataContext is WeightedElement element)
            {
                element.Weight = ParseCarefully(weightTB.Text);
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            updateElems();
        }

    }

    public class GenderToCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i = int.Parse(parameter.ToString());

            if (value is Gender gender && i is int target)
            {
                switch (target)
                {
                    case 0:
                        return gender == Gender.Male;
                    case 1:
                        return gender == Gender.Female;
                    case 2:
                        return gender == Gender.Neutral;
                    default:
                        return false;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class LayerElemsPercentageConverter : IValueConverter
    {
        public Layer Layer { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WeightedElement item && Layer != null)
            {
                string percent;
                percent = Layer.GetPercentage(item.Value) + "%";
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

using Archives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NPCArchiveAndGenerator
{
    public partial class RaceSelectionWin : Window
    {
        Archetype archetype;
        public RaceSelectionWin(Archetype archetype1)
        {
            archetype = archetype1;

            RaceCheckedConverter converter = new RaceCheckedConverter();
            converter.raceIds = archetype.Races.ToList();
            Resources.Add("RaceCheckedConverter", converter);


            InitializeComponent();

            NameLbl.Content = "Races";

            RacesIC.ItemsSource = ArchiveHandler.raceStorage;

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is Race race)
            {
                archetype.Races.AddElement(race.Id);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is Race race)
            {
                archetype.Races.Remove(archetype.Races.FirstOrDefault(el => el.Value.Equals(race.Id)));
            }
        }
    }

    public class RaceCheckedConverter : IValueConverter
    {
        public List<object> raceIds { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Race race)
            {
                if (raceIds.Any(e => e.Equals(race.Id)))
                    return "True";
            }
            return "False";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}

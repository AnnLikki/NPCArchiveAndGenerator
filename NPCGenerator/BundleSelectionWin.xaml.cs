using Archives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static Archives.Enums;

namespace NPCArchiveAndGenerator
{
    public partial class BundleSelectionWin : Window
    {
        Archetype archetype;
        public BundleType type;
        public BundleSelectionWin(BundleType type1, Archetype archetype1)
        {
            archetype = archetype1;
            type = type1;

            BundleCheckedConverter converter = new BundleCheckedConverter();
            converter.bundleIds = archetype.CompatableBundles[type].ToList();
            Resources.Add("BundleCheckedConverter", converter);


            InitializeComponent();

            CategoryNameLbl.Content = type + " Bundles";

            BundlesIC.ItemsSource = ArchiveHandler.bundleStorage[type];

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is Bundle bundle)
            {
                archetype.CompatableBundles.AddBundle(type, bundle);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is Bundle bundle)
            {
                archetype.CompatableBundles.RemoveBundle(type, bundle);
            }
        }
    }

    public class BundleCheckedConverter : IValueConverter
    {
        public List<object> bundleIds { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Bundle bundle)
            {
                if (bundleIds.Any(e => e.Equals(bundle.Id)))
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

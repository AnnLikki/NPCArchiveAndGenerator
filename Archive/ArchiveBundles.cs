using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


/// <summary>
/// A collection of Little Archives. For example, it allows to group them all by type to
/// place into a dictionary.
/// </summary>
namespace Archives
{
    public class ArchiveBundles : ObservableCollection<Bundle>
    {

        public string defaultValue = "";

        Random random = new Random();

        public Bundle getBundle(int index)
        {
            return this[index];
        }

        public string getRandomFromAnyOrDefault(string defaultValue)
        {
            List<Bundle> combinedBundles = new List<Bundle>();
            foreach (Bundle bundle in this)
            {
                combinedBundles.Add(bundle);
            }

            if (combinedBundles.Count == 0)
                return defaultValue;

            int index = random.Next(combinedBundles.Count);
            //Console.WriteLine(index);

            return combinedBundles[index].getRandom();
        }

        public string getRandomFromAnyOrDefault()
        {
            List<Bundle> combinedBundles = new List<Bundle>();
            foreach (Bundle bundle in this)
            {
                combinedBundles.Add(bundle);
            }

            if (combinedBundles.Count == 0)
                return this.defaultValue;

            int index = random.Next(combinedBundles.Count);
            //Console.WriteLine(index);

            return combinedBundles[index].getRandom();
        }

        public override string ToString()
        {
            string output = "";
            foreach (Bundle bundle in this)
            {
                output += bundle.ToString() + "\n";
            }
            return output;
        }

    }
}

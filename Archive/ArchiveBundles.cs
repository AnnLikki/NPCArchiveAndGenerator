using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// A collection of Little Archives. For example, it allows to group them all by type to
/// place into a dictionary.
/// </summary>
namespace Archives
{
    public class ArchiveBundles : ObservableCollection<Bundle>
    {
        Random random = new Random();

        public Bundle getBundle(int index)
        {
            return this[index];
        }

        public string getRandomFromAny()
        {
            List<Bundle> combinedBundles = new List<Bundle>();
            foreach (Bundle bundle in this)
            {
                combinedBundles.Add(bundle);
            }

            int index = random.Next(combinedBundles.Count);
            //Console.WriteLine(index);

            return combinedBundles[index].getRandom();
        }
    }
}

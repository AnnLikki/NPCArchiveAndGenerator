using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Archives.Enums;

namespace Archives
{
    /// <summary>
    /// Stores actual instances of objects. Doesn't partisipate in generation.
    /// </summary>
    public class BundleStorage : Dictionary<BundleType, ObservableCollection<Bundle>>
    {

        public BundleStorage()
        {
            foreach (BundleType type in (BundleType[])Enum.GetValues(typeof(BundleType)))
                if (!ContainsKey(type))
                    Add(type, new ObservableCollection<Bundle>());
        }

        /// <summary>
        /// Add a bundle to storage.
        /// </summary>
        public void Add(BundleType type, Bundle bundle)
        {
            if (!ContainsKey(type))
                Add(type, new ObservableCollection<Bundle>());
            this[type].Add(bundle);
        }

        /// <summary>
        /// Removes an element at index from collection corresponding with specified key.
        /// </summary>
        public void RemoveAt(BundleType type, int index)
        {
            this[type].RemoveAt(index);
        }

        /// <summary>
        /// Removes an element from collection corresponding with specified key.
        /// </summary>
        public void Remove(BundleType type, Bundle bundle)
        {
            this[type].Remove(bundle);
        }

        public void Clear(BundleType type)
        {
            if (ContainsKey(type))
                this[type].Clear();
        }

        public void ClearAllBundles()
        {
            foreach(BundleType type in (BundleType[])Enum.GetValues(typeof(BundleType)))
                if (ContainsKey(type))
                    this[type].Clear();
        }

        public Bundle FindBundle(BundleType archiveType, Guid ID)
        {
            if (!ContainsKey(archiveType))
                return null;
            return this[archiveType].FirstOrDefault(r => (r).Id.Equals(ID));
        }

        public override string ToString()
        {
            string res = "";
            foreach (BundleType type in (BundleType[])Enum.GetValues(typeof(BundleType)))
            {
                res += type + ":\n";
                foreach (var e in this[type])
                    res += e+"\n";
                res += "\n";
            }
            return res;
        }
    }
}

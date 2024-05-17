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

        public void Duplicate(BundleType type, Bundle bundle)
        {
            Bundle bundle1 = new Bundle(bundle.Name, bundle.IndependentLayers, bundle.Gender, bundle.LowerAgeLimit, bundle.UpperAgeLimit, bundle.DefaultValue, bundle.Layers);
            this[type].Insert(this[type].IndexOf(bundle), bundle1);
        }

        public void Clear(BundleType type)
        {
            if (ContainsKey(type))
                this[type].Clear();
        }

        public void ClearAllBundles()
        {
            foreach (BundleType type in (BundleType[])Enum.GetValues(typeof(BundleType)))
                if (ContainsKey(type))
                    this[type].Clear();
        }

        public Bundle FindBundle(BundleType bundleType, Guid ID)
        {
            if (!ContainsKey(bundleType))
                return null;
            return this[bundleType].FirstOrDefault(r => (r).Id.Equals(ID));
        }

        public List<Bundle> filterByKey(BundleType bundleType, string keyword)
        {
            if (keyword.Count() == 0)
            {
                List<Bundle> fin1 = new List<Bundle>();
                foreach (Bundle i in this[bundleType]) fin1.Add(i);
                return fin1;
            }
            List<Bundle> name = new List<Bundle>();
            List<Bundle> gender = new List<Bundle>();
            foreach (Bundle bundle in this[bundleType])
            {
                if (bundle.Name.ToLower().Contains(keyword.ToLower()) || keyword.ToLower().Contains(bundle.Name.ToLower()))
                {
                    name.Add(bundle);
                }
                else if (keyword.ToLower().Contains(bundle.Gender.ToString().ToLower()))
                {
                    gender.Add(bundle);
                }

            }

            List<Bundle> fin = new List<Bundle>();
            foreach (Bundle i in name) fin.Add(i);
            foreach (Bundle i in gender) fin.Add(i);

            return fin;

        }

        public override string ToString()
        {
            string res = "";
            foreach (BundleType type in (BundleType[])Enum.GetValues(typeof(BundleType)))
            {
                res += type + ":\n";
                foreach (var e in this[type])
                    res += e + "\n";
                res += "\n";
            }
            return res;
        }
    }
}

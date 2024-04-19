using System;
using System.Collections.Generic;
using System.Linq;
using static Archives.Enums;

namespace Archives
{
    /// <summary>
    /// Stores IDs of objects. Archetypes can't be added to kits.
    /// </summary>
    public class Kit : Dictionary<BundleType, WeightedArchive>
    {

        public Kit()
        {

            foreach (BundleType type in (BundleType[])Enum.GetValues(typeof(BundleType)))
                Add(type, new WeightedArchive());

        }

        /// <summary>
        /// Add a bundle to a kit as an ID.
        /// </summary>
        public void AddBundle(BundleType type, Bundle bundle, int weight = 1, Gender gender = Gender.Neutral)
        {
            if (!ContainsKey(type))
                Add(type, new WeightedArchive());
            if (!this[type].Any(e => e.Value.Equals(bundle.Id)))
                this[type].AddElement(bundle.Id, weight, gender);
        }

        /// <summary>
        /// Add a bundle to a kit as an ID.
        /// </summary>
        public void AddBundle(BundleType type, Guid bundleID, int weight = 1, Gender gender = Gender.Neutral)
        {
            if (!ContainsKey(type))
                Add(type, new WeightedArchive());
            if (!this[type].Any(e => e.Value.Equals(bundleID)))
                this[type].AddElement(bundleID, weight, gender);
        }

        public void RemoveBundle(BundleType type, Bundle bundle)
        {
            if (ContainsKey(type))
                if (this[type].Any(e => e.Value.Equals(bundle.Id)))
                    this[type].Remove(this[type].First(e => e.Value.Equals(bundle.Id)));
        }
        public void RemoveBundle(BundleType type, Guid bundleId)
        {
            if (ContainsKey(type))
                if (this[type].Any(e => e.Value.Equals(bundleId)))
                    this[type].Remove(this[type].First(e => e.Value.Equals(bundleId)));
        }


        public string GetRandomFromBundle(BundleStorage storage, BundleType type, Gender gender = Gender.Neutral, int ageBio = -1)
        {
            if (!ContainsKey(type))
                return null;
            return this[type].GetRandomFromBundle(storage, type, gender, ageBio);
        }


    }
}
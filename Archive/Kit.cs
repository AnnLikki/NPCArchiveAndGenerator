using System;
using System.Collections.Generic;
using static Archives.Enums;

namespace Archives
{
    /// <summary>
    /// Stores IDs of objects. Archetypes can't be added to kits.
    /// </summary>
    public class Kit : Dictionary<BundleType, WeightedArchive>
    {


        /// <summary>
        /// Add a bundle to a kit as an ID.
        /// </summary>
        public void AddBundle(BundleType type, Bundle bundle, int weight = 1, Gender gender = Gender.Neutral)
        {
            if (!ContainsKey(type))
                Add(type, new WeightedArchive());
            this[type].AddElement(bundle.Id, weight, gender);
        }

        /// <summary>
        /// Add a bundle to a kit as an ID.
        /// </summary>
        public void AddBundle(BundleType type, Guid bundleID, int weight = 1, Gender gender = Gender.Neutral)
        {
            if (!ContainsKey(type))
                Add(type, new WeightedArchive());
            this[type].AddElement(bundleID, weight, gender);
        }



        public string GetRandomFromBundle(BundleStorage storage, BundleType type, Gender gender = Gender.Neutral, int ageBio = -1)
        {
            if (!ContainsKey(type))
                return null;
            return this[type].GetRandomFromBundle(storage, type, gender, ageBio);
        }


    }
}
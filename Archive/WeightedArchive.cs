using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Archives.Enums;

namespace Archives
{
    /// <summary>
    /// Stores weighted elements.
    /// </summary>
    public class WeightedArchive : ObservableCollection<WeightedElement>
    {
        /// <summary>
        /// Value that is returned during randomization if the archive is empty
        /// </summary>
        public object DefaultValue = "";

        Random random = new Random();

        public int TotalWeight
        {
            get
            {
                int totalSum = 0;
                foreach (WeightedElement we in this)
                    totalSum += we.Weight;
                return totalSum;
            }
        }

        /// <summary>
        /// Add an instance of any element to archive.
        /// </summary>
        public void AddElement(object value, int weight = 1, Gender gender = Gender.Neutral)
        {
            if (!this.Any(e => e.Value.Equals(value)))
                Add(new WeightedElement(value, weight, gender));
        }

        public int GetWeight(object value)
        {
            if (this.Any(e => e.Value.Equals(value))) return this.First(e => e.Value.Equals(value)).Weight;
            else return 0;
        }

        public int GetPercentage(object value)
        {
            if (this.Any(e => e.Value.Equals(value)) && TotalWeight != 0) return GetWeight(value) * 100 / TotalWeight;
            else return 0;
        }

        /// <summary>
        /// Returns a new WeightedArchive that contains WeightedElements with Values that are
        /// present in both Weighted Archives. Weight is multiplied. Genders are "multipled". See <see cref="Multiply"/>.
        /// The caller of the method is given priority in terms of gender multiplication. 
        /// </summary>
        public WeightedArchive GetIntersection(WeightedArchive archive)
        {
            WeightedArchive result = new WeightedArchive();
            foreach (WeightedElement we1 in Items)
            {
                WeightedElement we2 = archive.FirstOrDefault(e => e.Value.Equals(we1.Value));
                if (we2 != null)
                    result.AddElement(we1.Value, we1.Weight * we2.Weight, Multiply(we1.Gender, we2.Gender));
            }
            return result;
        }


        /// <summary>
        /// Get weighted randomized result without any restrictions and conditions.
        /// </summary>
        public object GetRandomUnrestricted()
        {

            if (Count == 0) return DefaultValue;

            int totalSum = 0;

            foreach (WeightedElement we in Items)
                totalSum += we.Weight;

            int r = random.Next(1, totalSum + 1);
            int sum = 0;

            foreach (WeightedElement we in Items)
            {
                sum += we.Weight;
                if (sum >= r)
                    return we.Value;
            }
            // If there are no elements in this Archive
            return DefaultValue;
        }

        public string GetRandomFromBundle(BundleStorage storage, BundleType type, Gender gender = Gender.Neutral, int ageBio = -1)
        {
            // If Archive is empty
            if (Count == 0) return DefaultValue.ToString();

            // Searching for bundles in storage and checking compatability 
            // WEIGHTED ELEMENT'S GENDER IS PRIORITIZED
            List<WeightedElement> compatableBundleIDs = new List<WeightedElement>();
            foreach (WeightedElement we in Items)
            {
                Bundle found = storage.FindBundle(type, (Guid)we.Value);
                if (found != null)
                    if (we.Gender == Gender.Neutral || we.Gender == gender)
                    {
                        if (ageBio == -1 || (found.LowerAgeLimit <= ageBio && found.UpperAgeLimit >= ageBio))
                            compatableBundleIDs.Add(we);
                    }
            }

            int totalSum = 0;

            foreach (WeightedElement we in compatableBundleIDs)
                totalSum += we.Weight;

            int r = random.Next(1, totalSum + 1);
            int sum = 0;

            foreach (WeightedElement we in compatableBundleIDs)
            {
                sum += we.Weight;
                if (sum >= r)
                    return storage.FindBundle(type, (Guid)we.Value).GetRandom(gender, ageBio);
            }
            // If there are no compatable elements in this Archive or they all have 0 weight
            return DefaultValue.ToString();
        }

        public string GetRandomFromBundle(BundleType type, Gender gender = Gender.Neutral, int ageBio = -1)
        {
            // If Archive is empty
            if (Count == 0) return DefaultValue.ToString();

            // Searching for bundles in storage and checking compatability 
            // WEIGHTED ELEMENT'S GENDER IS PRIORITIZED
            List<WeightedElement> compatableBundleIDs = new List<WeightedElement>();
            foreach (WeightedElement we in Items)
            {
                Bundle found = ArchiveHandler.bundleStorage.FindBundle(type, (Guid)we.Value);
                if (found != null)
                    if (we.Gender == Gender.Neutral || we.Gender == gender)
                    {
                        if (ageBio == -1 || (found.LowerAgeLimit <= ageBio && found.UpperAgeLimit >= ageBio))
                            compatableBundleIDs.Add(we);
                    }
            }

            int totalSum = 0;

            foreach (WeightedElement we in compatableBundleIDs)
                totalSum += we.Weight;

            int r = random.Next(1, totalSum + 1);
            int sum = 0;

            foreach (WeightedElement we in compatableBundleIDs)
            {
                sum += we.Weight;
                if (sum >= r)
                    return ArchiveHandler.bundleStorage.FindBundle(type, (Guid)we.Value).GetRandom(gender, ageBio);
            }
            // If there are no compatable elements in this Archive or they all have 0 weight
            return DefaultValue.ToString();
        }

        public List<object> ToList()
        {
            List<object> l = new List<object>();
            foreach (WeightedElement we in this)
            {
                l.Add(we.Value);
            }
            return l;
        }

    }
}
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using static Archives.Enums;

namespace Archives
{
    /// <summary>
    /// Stores weighted elements.
    /// </summary>
    public class WeightedArchive : ObservableCollection<WeightedElement>
    {
        Random random = new Random();

        /// <summary>
        /// Add an instance of any element to archive.
        /// </summary>
        public void AddElement(object value, int weight = 1, Gender gender = Gender.Neutral)
        {
            Add(new WeightedElement(value, weight, gender));
        }

        public object GetRandomUnrestricted()
        {
            int totalSum = 0;
           
            foreach (WeightedElement we in Items)
                totalSum += we.Weight;

            int r = random.Next(totalSum + 1);
            int sum = 0;

            foreach (WeightedElement we in Items)
            {
                sum += we.Weight;
                if (sum >= r)
                    return we.Value;
            }
            // If there are no elements in this Archive
            return null;
        }

        public WeightedArchive Combine(WeightedArchive archive)
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

        public string GetRandomFromBundle(ArchiveStorage storage, ArchiveType type, Gender gender = Gender.Neutral, int ageBio = -1)
        {
            int totalSum = 0;
            foreach (WeightedElement we in Items.Where(
                e =>
                (storage.FindBundle(type, (Guid)e.Value).Gender == gender || storage.FindBundle(type, (Guid)e.Value).Gender == Gender.Neutral)
                &&
                (ageBio == -1 || (storage.FindBundle(type, (Guid)e.Value).LowerAgeLimit <= ageBio && storage.FindBundle(type, (Guid)e.Value).UpperAgeLimit >= ageBio))))
                totalSum += we.Weight;

            int r = random.Next(totalSum + 1);
            int sum = 0;

            foreach (WeightedElement we in Items)
            {
                sum += we.Weight;
                if (sum >= r)
                    return storage.FindBundle(type, (Guid)we.Value).GetRandom(gender, ageBio);
            }
            // If there are no elements in this Archive
            return null;
        }
    }
}

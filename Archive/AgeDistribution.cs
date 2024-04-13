
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Archives
{
    public class AgeDistribution : ObservableCollection<WeightedElement>
    {
        Random random = new Random();

        public void AddAge(int age, int weight = 1)
        {
            if (Items.Any(el => (int)el.Value == age))
                Items.First(element => element.Value.Equals(age)).Weight = weight;
            else
                Add(new WeightedElement(age, weight));
        }

        public void AddRange(int from, int to, int weight = 1)
        {
            if (from > to)
                throw new ArgumentException("Parameter \"to\" (" + to + ") shouldn't be lower than \"from\" (" + from + ").");
            for (int i = from; i <= to; i++)
                AddAge(i, weight);
        }

        public AgeDistribution GetIntersection(AgeDistribution distribution)
        {
            AgeDistribution result = new AgeDistribution();
            foreach (WeightedElement we1 in Items)
            {
                WeightedElement we2 = distribution.FirstOrDefault(e => e.Value.Equals(we1.Value));
                if (we2 != null)
                    result.Add(new WeightedElement((int)we1.Value, we1.Weight * we2.Weight));
            }
            return result;
        }

        public int GetRandom()
        {
            int totalSum = 0;
            foreach (WeightedElement we in Items)
                totalSum += we.Weight;

            int r = random.Next(1, totalSum + 1);
            int sum = 0;

            foreach (WeightedElement we in Items)
            {
                sum += we.Weight;
                if (sum >= r)
                    return (int) we.Value;
            }
            // If there are no elements on this layer
            return -1;
            
        }
    }
}
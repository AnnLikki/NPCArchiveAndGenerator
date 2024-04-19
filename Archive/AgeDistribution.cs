
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Archives
{
    public class AgeDistribution : ObservableCollection<WeightedElement>
    {
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
        public void AddAge(int age, int weight = 1)
        {
            if (Items.Any(el => el.Value.Equals(age)))
                Items.First(element => element.Value.Equals(age)).Weight = weight;
            else
                Add(new WeightedElement(age, weight));
        }

        private void NumberFlipFlop(int x, int y, out int from, out int to)
        {
            if (x > y)
                (x, y) = (y, x);
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            from = x;
            to = y;
        }

        public void AddRange(int from, int to, int weight = 1)
        {
            NumberFlipFlop(from, to, out from, out to);
            if (weight < 0) weight = 0;
            for (int i = from; i <= to; i++)
                AddAge(i, weight);
        }

        public void RemoveRange(int from, int to)
        {
            NumberFlipFlop(from, to, out from, out to);
            for (int i = 0; i < Count; i++)
            {
                WeightedElement we = this[i];
                if (int.Parse(we.Value.ToString()) >= from && int.Parse(we.Value.ToString()) <= to)
                {
                    Remove(we);
                    i--;
                }
            }
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
                    return int.Parse(we.Value.ToString());
            }
            // If there are no elements on this layer
            return -1;

        }

        public List<WeightedElement> GetRanges()
        {
            if (Count == 0)
                return new List<WeightedElement>();
            else
            {

                List<WeightedElement> orig = this.ToList();
                orig.Sort(delegate (WeightedElement x, WeightedElement y) { return (int.Parse(x.Value.ToString())).CompareTo(int.Parse(y.Value.ToString())); });

                int previousNumber = int.Parse(orig.First().Value.ToString());
                int previousWeight = int.Parse(orig.First().Weight.ToString());
                List<WeightedElement> list = new List<WeightedElement>();

                int from = previousNumber, to = previousNumber;
                int weight = previousWeight;

                foreach (WeightedElement we in orig)
                {
                    if (int.Parse(we.Value.ToString()) - previousNumber <= 1 && we.Weight == previousWeight)
                    {
                        to = int.Parse(we.Value.ToString());
                        weight = we.Weight;
                    }
                    else
                    {
                        list.Add(new WeightedElement(new Tuple<int, int>(from, to), weight));
                        from = int.Parse(we.Value.ToString());
                        to = int.Parse(we.Value.ToString());
                        weight = we.Weight;
                    }

                    previousNumber = int.Parse(we.Value.ToString());
                    previousWeight = we.Weight;
                }
                list.Add(new WeightedElement(new Tuple<int, int>(from, to), weight));

                return list;
            }
        }

        public int GetRangePercentage(int from, int to)
        {
            NumberFlipFlop(from, to, out from, out to);

            int rangeSum = 0;

            foreach (WeightedElement we in this)
                if (int.Parse(we.Value.ToString()) >= from && int.Parse(we.Value.ToString()) <= to)
                    rangeSum += we.Weight;

            return rangeSum * 100 / TotalWeight;
        }

        public int MaxAge()
        {
            if (Count == 0)
                return 0;
            return this.Max(e => int.Parse(e.Value.ToString()));
        }

        public int MaxWeight()
        {
            if (Count == 0)
                return 0;
            return this.Max(e => e.Weight);
        }

    }
}

using System.Linq;

namespace Archives
{
    public class AgeDistribution : WeightedArchive
    {

        public void Add(int age, int weight = 1)
        {
            if (this.Any(element => element.Value.Equals(age)))
                this.First(element => element.Value.Equals(age)).Weight = weight;
            else
                Add(new WeightedElement(age, weight));
        }

        public void AddRange(int from, int to, int weight = 1)
        {
            if (from > to)
                throw new System.ArgumentException("Parameter \"to\" ("+to+") shouldn't be lower than \"from\" ("+from+").");
            for(int i = from; i <= to; i++)
                Add(new WeightedElement(i, weight));
        }
    }
}

using System;

namespace Archives
{
    public class AgeRange
    {

        public string name { get; set; }
        public int lowerLimit { get; set; }
        public int upperLimit { get; set; }
        Random random = new Random();

        public AgeRange(string name, int lowerLimit, int upperLimit)
        {
            this.name = name;
            this.lowerLimit = lowerLimit;
            this.upperLimit = upperLimit;
        }

        internal int getRandom()
        {
            int r = random.Next(lowerLimit, upperLimit + 1);
            return r;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archives
{
    public class AgeRange
    {

        public string name;
        public int lowerLimit;
        public int upperLimit;
        Random random = new Random();

        public AgeRange(string name, int lowerLimit, int upperLimit)
        {
            this.name = name;
            this.lowerLimit = lowerLimit;
            this.upperLimit = upperLimit;
        }

        internal int getRandom()
        {
            int r = random.Next(lowerLimit, upperLimit+1);
            return r;
        }
    }
}

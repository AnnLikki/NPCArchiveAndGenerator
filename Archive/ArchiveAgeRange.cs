using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archives
{
    public class ArchiveAgeRange : ObservableCollection<AgeRange>
    {
        Random random = new Random();
        public int defaultValue { get; set; } = 0;

        public int getAnyRandomOrDefault(int defaultValue)
        {
            if (Count > 0)
            {
                int r = random.Next(0, this.Count);
                return this[r].getRandom();
            }
            else return defaultValue;
        }

        public int getAnyRandomOrDefault()
        {
            if (Count > 0)
            {
                int r = random.Next(0, this.Count);
                return this[r].getRandom();
            }
            else return this.defaultValue;
        }
    }
}

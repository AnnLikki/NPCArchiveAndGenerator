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

        public int getAnyRandom()
        {
            int r = random.Next(0, this.Count);
            return this[r].getRandom();
        }
    }
}

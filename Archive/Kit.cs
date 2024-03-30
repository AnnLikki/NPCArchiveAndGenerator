using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archives
{
    /// <summary>
    /// Stores IDs of objects.
    /// </summary>
    public class Kit : Dictionary<ArchiveType, WeightedArchive>
    {
        /// <summary>
        /// Add a race to a kit as an ID.
        /// </summary>
        public void Add(Race race, int weight = 1, Gender gender = Gender.Neutral)
        {
            if (!ContainsKey(ArchiveType.Race))
                Add(ArchiveType.Race, new WeightedArchive());
            this[ArchiveType.Race].Add(race.Id, weight, gender);
        }
        
        /// <summary>
        /// Add a bundle to a kit as an ID.
        /// </summary>
        public void Add(ArchiveType type, Bundle bundle, int weight = 1, Gender gender = Gender.Neutral)
        {
            if (!ContainsKey(type))
                Add(type, new WeightedArchive());
            this[type].Add(bundle, weight, gender);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using static Archives.Enums;

namespace Archives
{
    /// <summary>
    /// Stores IDs of objects. Archetypes can't be added to kits.
    /// </summary>
    public class Kit : Dictionary<ArchiveType, WeightedArchive>
    {
        /// <summary>
        /// Add a race to a kit as an ID.
        /// </summary>
        public void AddRace(Race race, int weight = 1)
        {
            if (!ContainsKey(ArchiveType.Race))
                Add(ArchiveType.Race, new WeightedArchive());
            this[ArchiveType.Race].AddElement(race.Id, weight);
        }
        /// <summary>
        /// Add a race to a kit as an ID.
        /// </summary>
        public void AddRace(Guid raceID, int weight = 1)
        {
            if (!ContainsKey(ArchiveType.Race))
                Add(ArchiveType.Race, new WeightedArchive());
            this[ArchiveType.Race].AddElement(raceID, weight);
        }

        /// <summary>
        /// Add a bundle to a kit as an ID.
        /// </summary>
        public void AddBundle(ArchiveType type, Bundle bundle, int weight = 1, Gender gender = Gender.Neutral)
        {
            if (!ContainsKey(type))
                Add(type, new WeightedArchive());
            this[type].AddElement(bundle.Id, weight, gender);
        }

        public string GetRandomFromBundle(ArchiveStorage storage, ArchiveType type, Gender gender, int age)
        {
            if (!ContainsKey(type))
                return null;
            return this[type].GetRandomFromBundle(storage, type, gender, age);
        }

        public Guid GetRandomRaceID()
        {
            if (!ContainsKey(ArchiveType.Race))
                return Guid.Empty;
            return (Guid) this[ArchiveType.Race].GetRandomUnrestricted();
        }
    }
}

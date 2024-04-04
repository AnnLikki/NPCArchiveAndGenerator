
using System;
using System.Linq;
using static Archives.Enums;

namespace Archives
{
    public class Archetype
    {
        /// <summary>
        /// The name of the architype in singular form, as a person of this archetype is called.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Compatable genders of this archetype. Elements of this Archive are Genders.
        /// </summary>
        public WeightedArchive Genders { get; set; }
        /// <summary>
        /// Compatable ages of this archetype. Elements of this Archive are integers.
        /// </summary>
        public AgeDistribution Ages { get; set; }
        /// <summary>
        /// Compatable Bundles of this archetype. Results of generation will only get picked from these Bundles.
        /// </summary>
        public Kit CompatableBundles { get; set; }

        public Archetype(string name) {
            Name = name;
            Genders = new WeightedArchive();
            Ages = new AgeDistribution();
            CompatableBundles = new Kit();
        }


        public int GetRandomAge(Race race)
        {
            return race.AgeDistribution.GetIntersection(Ages).GetRandom();
        }

        public string GetRandomFromBundle(ArchiveStorage storage, ArchiveType type, Race race, Gender gender, int age)
        {
            if (!CompatableBundles.ContainsKey(type) || CompatableBundles[type].Count == 0)
                return race.CompatableBundles.GetRandomFromBundle(storage, type, gender, age);
            return CompatableBundles.GetRandomFromBundle(storage, type, gender, age);
        }

        public Gender GetRandomGender(Race race)
        {
            if(Genders.Count!=0)
                return (Gender)race.Genders.GetIntersection(Genders).GetRandomUnrestricted();
            return (Gender)race.Genders.GetRandomUnrestricted();
        }
        public Race GetRandomRace(ArchiveStorage storage)
        {
            return storage.FindRace(CompatableBundles.GetRandomRaceID());
        }

        public void SetGender(Gender gender, int weight = 1)
        {
            if (Genders.Any(g => (Gender)g.Value == gender))
            {
                Genders.First(g => (Gender)g.Value == gender).Weight = weight;
            }
            else
            {
                Genders.Add(new WeightedElement(gender, weight));
            }
        }

        public void RemoveGender(Gender gender)
        {
            while (Genders.Any(g => (Gender)g.Value == gender))
            {
                Genders.Remove(Genders.First(g => (Gender)g.Value == gender));
            }
        }

    }
}

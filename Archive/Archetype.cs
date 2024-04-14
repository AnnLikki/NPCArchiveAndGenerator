
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


        public WeightedArchive Races { get; set; }
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

        public Archetype(string name)
        {
            Name = name;
            Races = new WeightedArchive();
            Genders = new WeightedArchive();
            Genders.DefaultValue = Gender.Neutral;
            Ages = new AgeDistribution();
            CompatableBundles = new Kit();
        }


        public int GetRandomAge(Race race)
        {
            return race.AgeDistribution.GetIntersection(Ages).GetRandom();
        }

        public string GetRandomFromBundle(BundleStorage storage, BundleType type, Race race, Gender gender, int age)
        {
            if (!CompatableBundles.ContainsKey(type) || CompatableBundles[type].Count == 0)
                return race.CompatableBundles.GetRandomFromBundle(storage, type, gender, age);
            return CompatableBundles.GetRandomFromBundle(storage, type, gender, age);
        }

        public void SetGender(Gender gender, int weight = 1)
        {
            if (Genders.Any(g => (Gender)g.Value == gender))
                Genders.First(g => (Gender)g.Value == gender).Weight = weight;
            else
                Genders.Add(new WeightedElement(gender, weight));
        }

        public void RemoveGender(Gender gender)
        {
            while (Genders.Any(g => (Gender)g.Value == gender))
                Genders.Remove(Genders.First(g => (Gender)g.Value == gender));
        }

        public Gender GetRandomGender(Race race)
        {
            if (Genders.Count != 0)
            {
                var intersection = race.Genders.GetIntersection(Genders);
                intersection.DefaultValue = Gender.Neutral;
                return (Gender)intersection.GetRandomUnrestricted();
            }
            return (Gender)race.Genders.GetRandomUnrestricted();
        }

        public void AddRace(Race race, int weight = 1)
        {
            if (Races.Any(g => (Guid)g.Value == race.Id))
                Races.First(g => (Guid)g.Value == race.Id).Weight = weight;
            else
                Races.AddElement(race.Id, weight);
        }

        public Race GetRandomRace(RaceStorage storage)
        {
            Guid raceID = (Guid)Races.GetRandomUnrestricted();
            return storage.FindRace(raceID);
        }

    }
}

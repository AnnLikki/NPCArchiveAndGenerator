
using System;
using System.Linq;
using static Archives.Enums;

namespace Archives
{
    public class Archetype
    {
        Random random = new Random();

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

        public Archetype(string name, WeightedArchive races = null, WeightedArchive genders = null, AgeDistribution ages = null, Kit compatableBundles = null)
        {
            Name = name;

            Races = new WeightedArchive();
            if (races != null)
                foreach (WeightedElement e in races)
                    Races.AddElement(Guid.Parse(e.Value.ToString()), e.Weight, e.Gender);

            Genders = new WeightedArchive();
            Genders.DefaultValue = Gender.Neutral;
            if (genders != null)
            {
                foreach (WeightedElement e in genders)
                {
                    if (int.TryParse(e.Value.ToString(), out int numerical))
                        Genders.AddElement((Gender)numerical, e.Weight);
                    else if (e.Value is Gender gender)
                        Genders.AddElement(gender, e.Weight);
                    else
                        throw new Exception("Yo wtf");

                }
                Genders.DefaultValue = genders.DefaultValue;
            }

            Ages = new AgeDistribution();
            if (ages != null)
                foreach (WeightedElement e in ages)
                    Ages.AddAge(int.Parse(e.Value.ToString()), e.Weight);

            CompatableBundles = new Kit();
            if (compatableBundles != null)
                foreach (BundleType type in (BundleType[])Enum.GetValues(typeof(BundleType)))
                    foreach (WeightedElement e in compatableBundles[type])
                        CompatableBundles.AddBundle(type, Guid.Parse(e.Value.ToString()), e.Weight, e.Gender);
        }


        public int GetRandomAgeBio(Race race)
        {
            return race.Ages.GetIntersection(Ages).GetRandom();
        }

        public int GetRandomAgeChrono(Race race)
        {
            int ageBio = GetRandomAgeBio(race);
            int from = race.calculateChronoAge(ageBio);
            int to = race.calculateChronoAge(ageBio+1);

            return random.Next(from, to);

        }

        public string GetRandomFromBundle(BundleStorage storage, BundleType type, Race race, Gender gender, int age)
        {
            if (!CompatableBundles.ContainsKey(type) || CompatableBundles[type].Count == 0)
                return race.CompatableBundles.GetRandomFromBundle(storage, type, gender, age);
            return CompatableBundles.GetRandomFromBundle(storage, type, gender, age);
        }

        public string GetRandomFromBundle(BundleType type, Race race, Gender gender, int age)
        {
            if (!CompatableBundles.ContainsKey(type) || CompatableBundles[type].Count == 0)
                return race.CompatableBundles.GetRandomFromBundle(type, gender, age);
            return CompatableBundles.GetRandomFromBundle(type, gender, age);
        }

        public void SetGender(Gender gender, int weight = 1)
        {
            if (Genders.Any(g => g.Value.Equals(gender)))
            {
                Genders.First(g => g.Value.Equals(gender)).Weight = weight;
            }
            else
            {
                Genders.Add(new WeightedElement(gender, weight));
            }

        }

        public void RemoveGender(Gender gender)
        {
            while (Genders.Any(g => g.Value.Equals(gender)))
                Genders.Remove(Genders.First(g => g.Value.Equals(gender)));
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
            if (Races.Any(g => g.Value.Equals(race.Id)))
                Races.First(g => g.Value.Equals(race.Id)).Weight = weight;
            else
                Races.AddElement(race.Id, weight);
        }

        public Race GetRandomRace(RaceStorage storage)
        {
            if (Races.Count != 0)
            {
                Guid raceID = (Guid)Races.GetRandomUnrestricted();
                return storage.FindRace(raceID);
            }
            else
                return null;
        }

        public Race GetRandomRace()
        {
            if (Races.Count != 0)
            {
                Guid raceID = (Guid)Races.GetRandomUnrestricted();
                return ArchiveHandler.raceStorage.FindRace(raceID);
            }
            else
                return null;
        }


        public override string ToString()
        {
            return Name;
        }

    }
}

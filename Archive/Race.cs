using System;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using static Archives.Enums;

namespace Archives
{
    public class Race : INotifyPropertyChanged
    {

        public static int basicMaturityAge = 18;
        public static int basicLifeExpectancy = 80;

        /// <summary>
        /// ID of the Race. Allows to reference it without duplication during deserialization.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The name of the race in singular form, as a person of this race is called.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Race description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Chronological age of biological maturity for this race. Equivalent of 18 years old in humans.
        /// If children of this race "age at the same rate as humans", it means their biological
        /// age of maturity is 18 y.o. regardles of lifespan or cultural norms.
        /// </summary>
        public int MaturityAge { get; set; }
        /// <summary>
        /// Chronological expected lifespan of a person of this race. Equivalent of 80 years old in humans.
        /// </summary>
        public int LifeExpectancy { get; set; }
        /// <summary>
        /// Compatable genders of this race. Elements of this Archive are Genders.
        /// </summary>
        public WeightedArchive Genders { get; set; }
        /// <summary>
        /// Compatable ages of this race. Elements of this Archive are integers.
        /// </summary>
        public AgeDistribution Ages { get; set; }
        /// <summary>
        /// Compatable Bundles of this race. Results of generation will only get picked from these Bundles.
        /// </summary>
        public Kit CompatableBundles { get; set; }

        public Race(string name, string description = "", int maturityAge = 18, int lifeExpectancy = 80)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            MaturityAge = maturityAge;
            LifeExpectancy = lifeExpectancy;
            Genders = new WeightedArchive();
            Genders.AddElement(Gender.Male);
            Genders.AddElement(Gender.Female);
            Genders.AddElement(Gender.Neutral);
            Genders.DefaultValue = Gender.Neutral;
            Ages = new AgeDistribution();
            Ages.AddRange(1, 60, 15);
            Ages.AddRange(61, 80, 5);
            Ages.AddRange(81, 90, 1);
            CompatableBundles = new Kit();

        }

        [JsonConstructor]
        public Race(Guid id, string name, string description, int maturityAge, int lifeExpectancy, WeightedArchive genders, AgeDistribution ages, Kit compatableBundles)
        {
            Id = id;
            Name = name;
            Description = description;
            MaturityAge = maturityAge;
            LifeExpectancy = lifeExpectancy;
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

        /// <summary>
        /// A method to update Race's info and call the PropertyChanged event on relevant properties.
        /// </summary>
        public void updateInfoNotifyably(string name, string description, int maturityAge, int lifeExpectancy)
        {
            if (Name != name)
            {
                Name = name;
                OnPropertyChanged(nameof(Name));
            }
            if (Description != description)
            {
                Description = description;
                OnPropertyChanged(nameof(Description));
            }
            MaturityAge = maturityAge;
            LifeExpectancy = lifeExpectancy;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// A delegate responsible for notifying about changed info.
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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


        public int calculateChronoAge(int ageBio)
        {
            int ageChrono;
            if (ageBio <= 0)
                ageChrono = 0;
            else
            {
                if (ageBio <= basicMaturityAge)
                    ageChrono = (int)Math.Round((double)(ageBio * MaturityAge / basicMaturityAge));
                else
                    ageChrono = (int)Math.Round((double)((ageBio - basicMaturityAge) * (LifeExpectancy - MaturityAge)) / (basicLifeExpectancy - basicMaturityAge) + MaturityAge);
            }
            return ageChrono;
        }

        public int calculateBioAge(int ageChrono)
        {
            int ageBio;

            if (ageChrono <= 0)
                ageBio = 0;
            else
            {
                if (ageChrono <= MaturityAge)
                    ageBio = (int)Math.Round((double)(ageChrono * basicMaturityAge) / MaturityAge);
                else
                    ageBio = (int)Math.Round((double)((ageChrono - MaturityAge) * (basicLifeExpectancy - basicMaturityAge)) / (LifeExpectancy - MaturityAge) + MaturityAge);
            }
            return ageBio;
        }

        public override string ToString()
        {
            return "Race " + Name;
        }


    }
}
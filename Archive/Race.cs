using System;
using System.ComponentModel;

namespace Archives
{
    public class Race : INotifyPropertyChanged
    {
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
        public AgeDistribution AgeDistribution { get; set; }
        /// <summary>
        /// Compatable Bundles of this race. Results of generation will only get picked from these Bundles.
        /// </summary>
        public Kit CompatableBundles { get; set; }

        public Race(string name, string description, int maturityAge, int lifeExpectancy)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            MaturityAge = maturityAge;
            LifeExpectancy = lifeExpectancy;
            Genders = new WeightedArchive();
            AgeDistribution = new AgeDistribution();
            CompatableBundles = new Kit();
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
    }
}

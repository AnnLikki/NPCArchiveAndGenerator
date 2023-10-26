using System;
using System.ComponentModel;

namespace Archives
{
    /// <summary>
    /// A class that represents a Race and contains all its variables.
    /// </summary>
    /// <remarks>
    /// It uses INotifyPropertyChanged to notify the DataGrid
    /// to update the data displayed.
    /// </remarks>
    public class Race : INotifyPropertyChanged
    {
        /// <summary>
        /// An ID of the race for identifying which race is linked to an NPC.
        /// </summary>
        public string ID { get; set; } = ArchiveRace.randomizer.Next(1000, 10000).ToString();
        public string Name { get; set; } = "New Race";
        public string Description { get; set; } = "";

        public int AgeMaturity { get; set; } = 18;
        public int LifeExpectancy { get; set; } = 80;

        public Race() { }
        public Race(string name, string description, int ageMaturity, int lifeExpectancy)
        {
            Name = name;
            Description = description;
            AgeMaturity = ageMaturity;
            LifeExpectancy = lifeExpectancy;
        }

        /// <summary>
        /// A method to update Race's info and call the PropertyChanged event on relevant properties.
        /// </summary>
        public void updateInfoNotifyably(string name, string description, int ageMaturity, int lifeExpectancy)
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
            AgeMaturity = ageMaturity;
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

        public override string ToString()
        {
            return Name;
        }

    }

}

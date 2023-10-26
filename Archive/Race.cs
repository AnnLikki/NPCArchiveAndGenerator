using System;
using System.ComponentModel;

namespace Archives
{
    // A class that contains all the variables of a Race.
    // It uses INotifyPropertyChanged to notify the DataGrid
    // to update the data displayed.
    public class Race : INotifyPropertyChanged
    {
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

        // A method to update Race's info and call the PropertyChanged event
        // on relevant properties.
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

        // Event and delegate responsible for notifying about changed info.
        public event PropertyChangedEventHandler PropertyChanged;

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

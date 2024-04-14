using System;
using System.ComponentModel;

namespace Archives
{
    /// <summary>
    /// A class that represents an NPC and contains all its variables.
    /// </summary>
    /// <remarks>
    /// It uses INotifyPropertyChanged to notify the DataGrid
    /// to update the data displayed.
    /// </remarks>
    public class NPC : INotifyPropertyChanged
    {
        // Main information
        public string Name { get; set; } = "No Name";
        public Guid RaceID { get; set; } = Guid.Empty;
        public string RaceName
        {
            get
            {
                Race race = ArchiveHandler.raceStorage.FindRace(RaceID);
                if (race == null)
                    return "No Race";
                else
                    return race.Name;

            }
        }
        public string Gender { get; set; } = "";
        public int AgeChrono { get; set; } = 0;
        public int AgeBio { get; set; } = 0;
        public string Occupation { get; set; } = "";
        public string Place { get; set; } = "";

        // Personality
        public string Character { get; set; } = "";
        public string Backstory { get; set; } = "";

        // Appearance
        public string Height { get; set; } = "";
        public string Physique { get; set; } = "";
        public string Skin { get; set; } = "";
        public string Hair { get; set; } = "";
        public string Face { get; set; } = "";
        public string Eyes { get; set; } = "";
        public string Clothes { get; set; } = "";
        public string Features { get; set; } = "";

        // Stats
        public int Str { get; set; } = 10;
        public int Dex { get; set; } = 10;
        public int Con { get; set; } = 10;
        public int Int { get; set; } = 10;
        public int Wis { get; set; } = 10;
        public int Cha { get; set; } = 10;

        // Extra
        public string Notes { get; set; } = "";

        public NPC() { }

        public NPC(string name, Race race, string gender, int ageChrono, int ageBio, string occupation, string place, string character, string backstory, string height, string physique, string skin, string hair, string face, string eyes, string clothes, string features, int str, int dex, int con, int intel, int wis, int cha, string notes)
        {
            Name = name;
            RaceID = race.Id;
            Gender = gender;
            AgeChrono = ageChrono;
            AgeBio = ageBio;
            Occupation = occupation;
            Place = place;
            Character = character;
            Backstory = backstory;
            Height = height;
            Physique = physique;
            Skin = skin;
            Hair = hair;
            Face = face;
            Eyes = eyes;
            Clothes = clothes;
            Features = features;
            Str = str;
            Dex = dex;
            Con = con;
            Int = intel;
            Wis = wis;
            Cha = cha;
            Notes = notes;
        }


        /// <summary>
        /// A method to calculate a modifier of a sertain stat.
        /// </summary>
        public static int calcMod(int stat)
        {
            return (int)Math.Floor(((double)stat - 10) / 2);
        }

        ///// <summary>
        ///// PROBS INCORRECT
        ///// Yet unused method of converting chronological age to biological (human) age. 
        ///// I decided to keep it because it might become handy later.
        ///// </summary>
        //public void chronoToBio()
        //{
        //    if (Race != null)
        //        if (AgeChrono <= Race.AgeMaturity)
        //            AgeBio = (int)Math.Round((double)(AgeChrono * ArchiveRace.baseRace.AgeMaturity) / Race.AgeMaturity);
        //        else
        //            AgeBio = (int)Math.Round((double)((AgeChrono - Race.AgeMaturity) * (ArchiveRace.baseRace.LifeExpectancy - ArchiveRace.baseRace.AgeMaturity)) / (Race.LifeExpectancy - Race.AgeMaturity)) + ArchiveRace.baseRace.AgeMaturity;
        //}

        ///// <summary>
        ///// PROBS INCORRECT
        ///// Yet unused method of converting biological (human) age to chronological age. 
        ///// I decided to keep it because it might become handy later.
        ///// </summary>
        //public void bioToChrono()
        //{
        //    if (Race != null)
        //        if (AgeBio <= ArchiveRace.baseRace.AgeMaturity)
        //            AgeChrono = (int)Math.Round((double)(AgeBio * Race.AgeMaturity) / ArchiveRace.baseRace.AgeMaturity);
        //        else
        //            AgeChrono = (int)Math.Round((double)((AgeBio - ArchiveRace.baseRace.AgeMaturity) * (Race.LifeExpectancy - Race.AgeMaturity)) / (ArchiveRace.baseRace.LifeExpectancy - ArchiveRace.baseRace.AgeMaturity)) + Race.AgeMaturity;
        //}

        /// <summary>
        /// A method to update NPC's info and call the PropertyChanged event on relevant properties.
        /// </summary>
        public void updateInfoNotifyably(string name, Race race, string gender, int ageChrono, int ageBio, string occupation, string place, string character, string backstory, string height, string physique, string skin, string hair, string face, string eyes, string clothes, string features, int str, int dex, int con, int intel, int wis, int cha, string notes)
        {
            if (Name != name)
            {
                Name = name;
                OnPropertyChanged(nameof(Name));
            }
            if (race!=null&&RaceID != race.Id)
            {
                RaceID = race.Id;
                OnPropertyChanged(nameof(RaceName));
            }
            if (Gender != gender)
            {
                Gender = gender;
                OnPropertyChanged(nameof(Gender));
            }
            if (AgeChrono != ageChrono)
            {
                AgeChrono = ageChrono;
                OnPropertyChanged(nameof(AgeChrono));
            }
            if (Occupation != occupation)
            {
                Occupation = occupation;
                OnPropertyChanged(nameof(Occupation));
            }


            AgeBio = ageBio;
            Occupation = occupation;
            Place = place;
            Character = character;
            Backstory = backstory;
            Height = height;
            Physique = physique;
            Skin = skin;
            Hair = hair;
            Face = face;
            Eyes = eyes;
            Clothes = clothes;
            Features = features;
            Str = str;
            Dex = dex;
            Con = con;
            Int = intel;
            Wis = wis;
            Cha = cha;
            Notes = notes;

        }

        /// <summary>
        /// A method to update NPC's info and call the PropertyChanged event on relevant properties.
        /// </summary>
        public void UpdateInfoNotifyably(NPC npcToCopyFrom)
        {
            if (Name != npcToCopyFrom.Name)
            {
                Name = npcToCopyFrom.Name;
                OnPropertyChanged(nameof(Name));
            }
            if (RaceID != npcToCopyFrom.RaceID)
            {
                RaceID = npcToCopyFrom.RaceID;
                OnPropertyChanged(nameof(RaceName));
            }
            if (Gender != npcToCopyFrom.Gender)
            {
                Gender = npcToCopyFrom.Gender;
                OnPropertyChanged(nameof(Gender));
            }
            if (AgeChrono != npcToCopyFrom.AgeChrono)
            {
                AgeChrono = npcToCopyFrom.AgeChrono;
                OnPropertyChanged(nameof(AgeChrono));
            }
            if (Occupation != npcToCopyFrom.Occupation)
            {
                Occupation = npcToCopyFrom.Occupation;
                OnPropertyChanged(nameof(Occupation));
            }

            AgeBio = npcToCopyFrom.AgeBio;
            Occupation = npcToCopyFrom.Occupation;
            Place = npcToCopyFrom.Place;
            Character = npcToCopyFrom.Character;
            Backstory = npcToCopyFrom.Backstory;
            Height = npcToCopyFrom.Height;
            Physique = npcToCopyFrom.Physique;
            Skin = npcToCopyFrom.Skin;
            Hair = npcToCopyFrom.Hair;
            Face = npcToCopyFrom.Face;
            Eyes = npcToCopyFrom.Eyes;
            Clothes = npcToCopyFrom.Clothes;
            Features = npcToCopyFrom.Features;
            Str = npcToCopyFrom.Str;
            Dex = npcToCopyFrom.Dex;
            Con = npcToCopyFrom.Con;
            Int = npcToCopyFrom.Int;
            Wis = npcToCopyFrom.Wis;
            Cha = npcToCopyFrom.Cha;
            Notes = npcToCopyFrom.Notes;
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
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Archives
{
    /// <summary>
    /// Stores actual instances of Races.
    /// </summary>
    public class RaceStorage : ObservableCollection<Race>
    {

        public Race FindRace(Guid ID)
        {
            return this.FirstOrDefault(r => r.Id.Equals(ID));
        }

        public Collection<Race> filterByKey(string keyword)
        {
            if (keyword.Count() == 0)
            {
                Collection<Race> fin1 = new Collection<Race>();
                foreach (Race i in this) fin1.Add(i);
                return fin1;
            }
            Collection<Race> name = new Collection<Race>();
            Collection<Race> desc = new Collection<Race>();
            foreach (Race race in this)
            {
                if (race.Name.ToLower().Contains(keyword.ToLower()))
                {
                    name.Add(race);
                }
                else if (race.Description != null && race.Description.ToLower().Contains(keyword.ToLower()))
                {
                    desc.Add(race);
                }

            }

            Collection<Race> fin = new Collection<Race>();
            foreach (Race i in name) fin.Add(i);
            foreach (Race i in desc) fin.Add(i);

            return fin;

        }

        public void Duplicate(Race race)
        {
            Race race1 = new Race(Guid.NewGuid(), race.Name, race.Description, race.MaturityAge, race.LifeExpectancy, race.Genders, race.Ages, race.CompatableBundles);
            Insert(IndexOf(race), race1);
        }

        public override string ToString()
        {
            string res = "";

            foreach (var r in this)
                res += r + "\n";

            return res;
        }

    }
}

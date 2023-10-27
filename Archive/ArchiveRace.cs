using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Archives
{
    /// <summary>
    /// A collection of Race Objects.
    /// </summary>
    /// <remarks>
    /// Inherits from ObresvableCollection, which allows
    /// to raise events like CollectionChanged, so the DataGrid will update accordingly.
    /// </remarks>
    public class ArchiveRace : ObservableCollection<Race>
    {
        /// <summary>
        /// Used to generate unique IDs for races.
        /// </summary>
        public static Random randomizer = new Random();

        /// <summary>
        /// Is a basic (human) race that is reffered to when converting
        /// chonological age to biological (human) age and vice versa.
        /// </summary>
        public static Race baseRace = new Race();


        /// <summary>
        /// <returns>
        /// <para>
        /// Returns a Race in this ArchiveRace that matches given Race by its ID.
        /// </para>
        /// <para>
        /// Returns null if raceToMatch is null.
        /// </para>
        /// </returns>
        /// </summary>
        public Race FindMatching(Race raceToMatch)
        {
            if (raceToMatch == null) return null;
            return this.FirstOrDefault(race => race.ID == raceToMatch.ID);
        }

        /// <summary>
        /// Returns an ArchiveRace, filtered and sorted by the keyword.
        /// </summary>
        /// <remarks>
        /// Returns:
        /// <para>
        /// A filtered and sorted ArchiveRace using the method:
        /// </para>
        /// <para>
        /// If zero length keyword - all Objects.
        /// </para>
        /// <para>
        /// If the keyword is found in either <c>name</c> or <c>desc</c>,
        /// disregarding letter case, it will recombine the found results by these groups in this order,
        /// in the groups themselves Objects will keep their relative position.
        /// </para>
        /// </remarks>
        public ArchiveRace filterByKey(string keyword)
        {

            if (keyword.Count() == 0)
            {
                ArchiveRace fin1 = new ArchiveRace();
                foreach (Race i in this) fin1.Add(i);
                return fin1;
            }
            ArchiveRace name = new ArchiveRace();
            ArchiveRace desc = new ArchiveRace();
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

            ArchiveRace fin = new ArchiveRace();
            foreach (Race i in name) fin.Add(i);
            foreach (Race i in desc) fin.Add(i);

            return fin;

        }
    }
}

using System.Collections.ObjectModel;
using System.Linq;

namespace Archives
{
    // ArchiveRace is a class that inherits from ObresvableCollection, which allows
    // to raise events like ColletionChanged, so the DataGrid will update accordingly.
    // It is a collection of Race type.
    // In future I am planning to add sorting/filtering by a text query. 
    public class ArchiveRace : ObservableCollection<Race>
    {

        // baseRace is a basic (human) race that is reffered to when converting
        // chonological age to biological (human) age and vice versa.
        public static Race baseRace { get; set; } = new Race();

        private ObservableCollection<Race> races = new ObservableCollection<Race>();

        public ObservableCollection<Race> Races
        {
            get { return races; }
            set { races = value; }

        }
        // Returns the Race in this ArchiveRace that matches some other Race by its fields.
        public Race FindMatching(Race raceToMatch)
        {
            if (raceToMatch == null) return null;
            return this.FirstOrDefault(race =>
                race.Name == raceToMatch.Name &&
                race.Description == raceToMatch.Description &&
                race.AgeMaturity == raceToMatch.AgeMaturity &&
                race.LifeExpectancy == raceToMatch.LifeExpectancy);
        }

    }
}

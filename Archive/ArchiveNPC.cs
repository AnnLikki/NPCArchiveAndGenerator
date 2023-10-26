using System.Collections.ObjectModel;
using System.Linq;

namespace Archives
{
    /// <summary>
    /// A collection of NPC Objects.
    /// </summary>
    /// <remarks>
    /// Inherits from ObresvableCollection, which allows
    /// to raise events like CollectionChanged, so the DataGrid will update accordingly.
    /// </remarks>

    public class ArchiveNPC : ObservableCollection<NPC>
    {
        // TODO Remove unused code after tests.

        //private ObservableCollection<NPC> npcs = new ObservableCollection<NPC>();

        /*public ObservableCollection<NPC> NPCs
        {
            get { return npcs; }
            set { npcs = value; }
        }
        */

        //public ArchiveNPC() { }

        /// <summary>
        /// Returns an ArciveNPC, filtered and sorted by the keyword.
        /// </summary>
        /// <remarks>
        /// Returns:
        /// <para>
        /// A filtered and sorted ArchiveNPC using the method:
        /// </para>
        /// <para>
        /// If zero length keyword - all Objects.
        /// </para>
        /// <para>
        /// If the keyword is found in either <c>name</c>, <c>race.Name</c>, 
        /// <c>place</c>, <c>occup</c>, <c>other</c>, <c>notes</c>,
        /// disregarding letter case, it will recombine the found results by these groups in this order,
        /// in the groups themselves Objects will keep their relative position.
        /// </para>
        /// </remarks>
        public ArchiveNPC filterByKey(string keyword)
        {
            if (keyword.Count() == 0)
            {
                ArchiveNPC fin1 = new ArchiveNPC();
                foreach (NPC i in this) fin1.Add(i);
                return fin1;
            }
            ArchiveNPC name = new ArchiveNPC();
            ArchiveNPC race = new ArchiveNPC();
            ArchiveNPC place = new ArchiveNPC();
            ArchiveNPC occup = new ArchiveNPC();
            ArchiveNPC other = new ArchiveNPC();
            ArchiveNPC notes = new ArchiveNPC();
            foreach (NPC npc in this)
            {
                if (npc.Name.ToLower().Contains(keyword.ToLower()))
                {
                    name.Add(npc);
                }
                else if (npc.Race != null && npc.Race.Name.ToLower().Contains(keyword.ToLower()))
                {
                    race.Add(npc);
                }
                else if (npc.Place.ToLower().Contains(keyword.ToLower()))
                {
                    place.Add(npc);
                }
                else if (npc.Occupation.ToLower().Contains(keyword.ToLower()))
                {
                    occup.Add(npc);
                }
                else if (npc.Gender.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.AgeChrono.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.Character.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.Backstory.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.Height.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.Physique.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.Skin.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.Hair.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.Face.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.Eyes.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.Clothes.ToString().ToLower().Contains(keyword.ToLower()) ||
                    npc.Features.ToString().ToLower().Contains(keyword.ToLower()))
                {
                    other.Add(npc);
                }
                else if (npc.Notes.ToString().ToLower().Contains(keyword.ToLower()))
                {
                    notes.Add(npc);
                }
            }

            ArchiveNPC fin = new ArchiveNPC();
            foreach (NPC i in name) fin.Add(i);
            foreach (NPC i in race) fin.Add(i);
            foreach (NPC i in place) fin.Add(i);
            foreach (NPC i in occup) fin.Add(i);
            foreach (NPC i in other) fin.Add(i);
            foreach (NPC i in notes) fin.Add(i);

            return fin;

        }


    }

}

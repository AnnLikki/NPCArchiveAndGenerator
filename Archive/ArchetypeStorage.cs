using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Archives
{
    /// <summary>
    /// Stores actual instances of Archetypes.
    /// </summary>

    public class ArchetypeStorage
    {
        public ObservableCollection<Archetype> Items { get; set; } = new ObservableCollection<Archetype>();
        public int DefaultArchiveIndex { get; set; } = 0;


        public void Add(Archetype item)
        {
            Items.Add(item);
        }

        public bool Remove(Archetype item)
        {
            return Items.Remove(item);
        }

        public override string ToString()
        {
            string res = "Def ind " + DefaultArchiveIndex + " \n";

            foreach (var r in Items)
                res += r + "\n";

            return res;
        }

        public IEnumerator<Archetype> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
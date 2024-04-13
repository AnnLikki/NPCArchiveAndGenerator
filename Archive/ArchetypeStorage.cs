using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Archives.Enums;

namespace Archives
{
    /// <summary>
    /// Stores actual instances of Archetypes.
    /// </summary>
    public class ArchetypeStorage : ObservableCollection<Archetype>
    {

        public override string ToString()
        {
            string res = "";

            foreach (var r in this)
                res += r + "\n";

            return res;
        }
    }
}

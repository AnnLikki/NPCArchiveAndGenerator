using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Archives
{
    /// <summary>
    /// Stores actual instances of objects. Doesn't partisipate in generation.
    /// </summary>
    public class ArchiveStorage : Dictionary<ArchiveType, ObservableCollection<object>>
    {
        /// <summary>
        /// Add a race to storage.
        /// </summary>
        public void Add(Race race)
        {
            if(!ContainsKey(ArchiveType.Race))
                Add(ArchiveType.Race, new ObservableCollection<object>());
            this[ArchiveType.Race].Add(race);
        }

        /// <summary>
        /// Add an archetype to storage.
        /// </summary>
        public void Add(Archetype archetype)
        {
            if (!ContainsKey(ArchiveType.Archetype))
                Add(ArchiveType.Archetype, new ObservableCollection<object>());
            this[ArchiveType.Archetype].Add(archetype);
        }

        /// <summary>
        /// Add a bundle to storage.
        /// </summary>
        public void Add(ArchiveType type, Bundle bundle)
        {
            if (!ContainsKey(type))
                Add(type, new ObservableCollection<object>());
            this[type].Add(bundle);
        }

        internal Race FindRace(Guid ID)
        {
            throw new NotImplementedException();
        }
        internal Archetype FindArchetype(Guid ID)
        {
            throw new NotImplementedException();
        }
        internal Bundle FindBundle(ArchiveType archiveType, Guid ID)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Archives.Enums;

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
            if (!ContainsKey(ArchiveType.Race))
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

        /// <summary>
        /// Removes an element at index from collection corresponding with specified key.
        /// </summary>
        public void RemoveAt(ArchiveType type, int index)
        {
            this[type].RemoveAt(index);
        }

        /// <summary>
        /// Removes an element from collection corresponding with specified key.
        /// </summary>
        public void Remove(ArchiveType type, object element)
        {
            this[type].Remove(element);
        }

        public Race FindRace(Guid ID)
        {
            if (!ContainsKey(ArchiveType.Race))
                return null;
            return (Race)this[ArchiveType.Race].FirstOrDefault(r => ((Race)r).Id.Equals(ID));
        }
        public Bundle FindBundle(ArchiveType archiveType, Guid ID)
        {
            if (!ContainsKey(archiveType))
                return null;
            return (Bundle)this[archiveType].FirstOrDefault(r => ((Bundle)r).Id.Equals(ID));
        }

    }
}

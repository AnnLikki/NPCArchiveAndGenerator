﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Archives
{
    /// <summary>
    /// Stores actual instances of Archetypes.
    /// </summary>

    public class ArchetypeStorage
    {
        public ObservableCollection<Archetype> Items { get; set; } = new ObservableCollection<Archetype>();
        public Archetype DefaultArchetype { get; set; }


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
            string res = "";

            foreach (var r in Items)
                res += r + "\n";

            return res;
        }

        public IEnumerator<Archetype> GetEnumerator()
        {
            return Items.GetEnumerator();
        }


        public List<Archetype> filterByKey(string keyword)
        {
            if (keyword.Count() == 0)
            {
                List<Archetype> fin1 = new List<Archetype>();
                foreach (Archetype i in this) fin1.Add(i);
                return fin1;
            }
            List<Archetype> name = new List<Archetype>();
            foreach (Archetype Archetype in this)
            {
                if (Archetype.Name.ToLower().Contains(keyword.ToLower()))
                {
                    name.Add(Archetype);
                }

            }

            return name;

        }


        public void SetDefaultArchetype(Archetype archetype)
        {

            // Items.Add(new Archetype(DefaultArchetype.Name, DefaultArchetype.Races, DefaultArchetype.Genders, DefaultArchetype.Ages, DefaultArchetype.CompatableBundles));

            if (DefaultArchetype != null)
                Items.Add(DefaultArchetype);
            Items.Remove(archetype);

            DefaultArchetype = archetype;

        }

        public List<Archetype> ToList()
        {
            List<Archetype> list = new List<Archetype>();
            list.Add(DefaultArchetype);
            foreach(Archetype ar in Items)
                list.Add(ar);
            return list;
        }
    }
}
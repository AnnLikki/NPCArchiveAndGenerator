using System;
using System.Collections.Generic;

namespace Archives
{
    public class ArchiveDictionary : Dictionary<ArchiveType, ArchiveBundles>
    {

        public string defaultValue = null;

        /*
        public new ArchiveBundles this[ArchiveType archiveType]
        {
            get
            {
                if (ContainsKey(archiveType))
                {
                    return base[archiveType];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                base[archiveType] = value;
            }
        }
        */

        public string getRandomFromAnyOrDefault(ArchiveType type, string defaultValue)
        {
            if (!ContainsKey(type))
            {
                return defaultValue;
            }
            else
            {
                return base[type].getRandomFromAnyOrDefault(defaultValue);
            }
        }

        public string getRandomFromAnyOrDefault(ArchiveType type)
        {
            if (!ContainsKey(type))
            {
                return this.defaultValue;
            }
            else
            {
                return base[type].getRandomFromAnyOrDefault(defaultValue);
            }
        }

        public override string ToString()
        {
            string output = "";

            foreach (ArchiveType type in Enum.GetValues(typeof(ArchiveType)))
            {
                output += type.ToString() + "\n";
                if (ContainsKey(type))
                {
                    output += this[type].ToString() + "\n";
                }
                else
                {
                    output += "EMPTY" + "\n";
                }
            }
            return output;
        }

    }
}

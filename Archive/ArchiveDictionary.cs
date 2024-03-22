using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archives
{
    public class ArchiveDictionary : Dictionary<ArchiveType, ArchiveBundles>
    {

        public string defaultValue = null;

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

    }
}

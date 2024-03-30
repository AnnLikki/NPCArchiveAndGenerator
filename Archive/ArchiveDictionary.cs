using System.Collections.Generic;

namespace Archives
{
    public class ArchiveDictionary : Dictionary<ArchiveType, WeightedArchive>
    {
        /// <summary>
        /// The object that will be returned if the Dictionary contains no Archives
        /// of the requested type.
        /// </summary>
        public object DefaultValue { get; set; }
    }
}

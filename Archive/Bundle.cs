
using System;
using System.Collections.ObjectModel;

namespace Archives
{
    public class Bundle
    {
        /// <summary>
        /// ID of the Bundle. Allows to reference it without duplication during deserialization.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Given name of the Bundle.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// If the Bundle is gendered it will get picked only for characters of the same gender.
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// The lowest biological age of a character that this Bundle would be comapatable with.
        /// </summary>
        public uint LowerAgeLimit { get; set; }
        /// <summary>
        /// The highest biological age of a character that this Bundle would be comapatable with.
        /// </summary>
        public uint UpperAgeLimit { get; set; }
        /// <summary>
        /// If true - deeper layers can be picked if the previous one wasn't,
        /// if false - it stops the generation on the first layer that wasn't picked.
        /// </summary>
        public bool IndependentLayers { get; set; }
        /// <summary>
        /// A collection of Layers of elements. Layers allow for multi-level results by placing different 
        /// independent types of data separately and picking randomly from each layer. 
        /// </summary>
        public Collection<Layer> Layers { get; set; }
    }
}

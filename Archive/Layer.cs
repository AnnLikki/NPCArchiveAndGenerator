using System.Collections.ObjectModel;

namespace Archives
{
    public class Layer
    {
        /// <summary>
        /// If Layer wasn't picked (applies only if it failed the check regarging the Chance), 
        /// this value will be returned as the result of picking.
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// A value between 0.0 and 1.0 that determines a chance of picking this Layer 
        /// while generating a multi-level result.
        /// </summary>
        public double Chance { get; set; }
        /// <summary>
        /// If gendered, this Layer will only get picked if the character has the same gender.
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// The lowest biological age of a character that this Layer would be comapatable with.
        /// </summary>
        public uint LowerAgeLimit { get; set; }
        /// <summary>
        /// The highest biological age of a character that this Layer would be comapatable with.
        /// </summary>
        public uint UpperAgeLimit { get; set; }
        /// <summary>
        /// A collection of element from which the result of randomization will be picked. 
        /// If empty, returns the Default Value.
        /// </summary>
        public Collection<WeightedElement> Elements { get; set; }
    }
}

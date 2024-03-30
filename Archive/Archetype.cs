
namespace Archives
{
    public class Archetype
    {
        /// <summary>
        /// The name of the architype in singular form, as a person of this archetype is called.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Compatable genders of this archetype. Elements of this Archive are Genders.
        /// </summary>
        public WeightedArchive Genders { get; set; }
        /// <summary>
        /// Compatable ages of this archetype. Elements of this Archive are integers.
        /// </summary>
        public WeightedArchive Ages { get; set; }
        /// <summary>
        /// Compatable Bundles of this archetype. Results of generation will only get picked from these Bundles.
        /// </summary>
        public Kit CompatableBundles { get; set; }
    }
}

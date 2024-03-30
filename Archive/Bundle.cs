
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
        public int LowerAgeLimit { get; set; }
        /// <summary>
        /// The highest biological age of a character that this Bundle would be comapatable with.
        /// </summary>
        public int UpperAgeLimit { get; set; }
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


        public Bundle(string name, bool independentLayers = true, Gender gender = Gender.Neutral, int lowerAgeLimit = 0, int upperAgeLimit = int.MaxValue)
        {
            Id = Guid.NewGuid();
            Name = name;
            IndependentLayers = independentLayers;
            Gender = gender;
            LowerAgeLimit = lowerAgeLimit;
            UpperAgeLimit = upperAgeLimit;
            Layers = new Collection<Layer>();
        }

        public void InsertNewLayer(int index, double chance = 1.0, string defaultValue = "", Gender gender=Gender.Neutral, int lowerAgeLimit = 0, int upperAgeLimit = int.MaxValue, Collection<WeightedElement> elements = null)
        {
            Layers.Insert(index, new Layer(chance, defaultValue, gender, lowerAgeLimit, upperAgeLimit, elements));
        }

        public void AddToLayer(int index, WeightedElement element)
        {
            Layers[index].Add(element);
        }
        public void AddToLayer(int index, string value, int weight = 1, Gender gender = Gender.Neutral)
        {
            Layers[index].Add(new WeightedElement(value, weight, gender));
        }
    }
}

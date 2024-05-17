
using System;
using System.Collections.ObjectModel;
using System.Linq;
using static Archives.Enums;

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
        /// Default Value is returned if the bundle contains no Layers.
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// A collection of Layers of elements. Layers allow for multi-level results by placing different 
        /// independent types of data separately and picking randomly from each layer. 
        /// </summary>
        public Collection<Layer> Layers { get; set; }
        /// <summary>
        /// Gets the number of layers in this bundle.
        /// </summary>
        public int Count { get { return Layers.Count; } }

        private Random random = new Random();

        public Bundle(string name, bool independentLayers = true, Gender gender = Gender.Neutral, int lowerAgeLimit = 0, int upperAgeLimit = int.MaxValue, string defaultValue = "No suitable layers", Collection<Layer> layers = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            IndependentLayers = independentLayers;
            Gender = gender;
            LowerAgeLimit = lowerAgeLimit;
            UpperAgeLimit = upperAgeLimit;
            DefaultValue = defaultValue;
            Layers = new Collection<Layer>();
            if (layers != null)
                foreach (Layer layer in layers)
                    Layers.Add(new Layer(layer.Chance, layer.After, layer.DefaultValue, layer.Gender, layer.LowerAgeLimit, layer.UpperAgeLimit, layer.Elements));
        }

        /// <summary>
        /// Inser new layer at specified index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws ArgumentOutOfRangeException if index is out of bounds [0, Count].
        /// </exception>
        public void InsertNewLayer(int index, double chance = 1.0, string after = " ", string defaultValue = "", Gender gender = Gender.Neutral, int lowerAgeLimit = 0, int upperAgeLimit = int.MaxValue, Collection<WeightedElement> elements = null)
        {
            if (index < 0 || index > Layers.Count)
                throw new ArgumentOutOfRangeException("index");
            Layers.Insert(index, new Layer(chance, after, defaultValue, gender, lowerAgeLimit, upperAgeLimit, elements));
        }
        public void InsertNewLayer(int index, Layer layer)
        {
            if (index < 0 || index > Layers.Count)
                throw new ArgumentOutOfRangeException("index");
            Layers.Insert(index, layer);
        }

        public void Duplicate(Layer layer)
        {
            Layer layer1 = new Layer(layer.Chance, layer.After, layer.DefaultValue, layer.Gender, layer.LowerAgeLimit, layer.UpperAgeLimit, layer.Elements);

            InsertNewLayer(Layers.IndexOf(layer), layer1);
        }

        /// <summary>
        /// Removes layer at specified index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws ArgumentOutOfRangeException if index is out of bounds.
        /// </exception>
        public void RemoveLayerAt(int index)
        {
            if (index < 0 || index >= Layers.Count)
                throw new ArgumentOutOfRangeException("index");
            Layers.RemoveAt(index);
        }

        public void RemoveLayer(Layer layer)
        {
            Layers.Remove(layer);
        }

        /// <summary>
        /// Removes all layers from this bundle.
        /// </summary>
        public void ClearLayers()
        {
            Layers.Clear();
        }

        /// <summary>
        /// Returns the layer at specified index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Layer GetLayer(int index)
        {
            if (index < 0 || index >= Layers.Count)
                throw new ArgumentOutOfRangeException("index");
            return Layers[index];
        }

        public bool MoveLayerUp(Layer layer)
        {
            int ind = Layers.IndexOf(layer);
            if (ind > 0)
            {
                Layers.Remove(layer);
                Layers.Insert(ind - 1, layer);
                return true;
            }
            return false;

        }
        public bool MoveLayerDown(Layer layer)
        {
            int ind = Layers.IndexOf(layer);
            if (ind < Count - 1)
            {
                Layers.Remove(layer);
                Layers.Insert(ind + 1, layer);
                return true;
            }
            return false;

        }

        /// <summary>
        /// Add WeightedElement to the Layer at specified index. Can't add an element that has already been added to this Layer. 
        /// Can't add a null.
        /// </summary>
        /// <param name="index">Index of Layer that the element will be added to.</param>
        public void AddToLayer(int index, WeightedElement element)
        {
            Layers[index].Add(element);
        }

        /// <summary>
        /// Add a new WeighedElement with string Value and other specified parameters to the Layer at specified index.
        /// </summary>
        /// <param name="index">Index of Layer that the element will be added to.</param>
        public void AddToLayer(int index, string value, int weight = 1, Gender gender = Gender.Neutral)
        {
            Layers[index].Add(new WeightedElement(value, weight, gender));
        }

        public string GetRandom(Gender gender = Gender.Neutral, int ageBio = -1)
        {
            // If the bundle has no suitable layers
            if (Layers.Count(
                l =>
                (l.Gender == gender || l.Gender == Gender.Neutral)
                &&
                (ageBio == -1 || (l.LowerAgeLimit <= ageBio && l.UpperAgeLimit >= ageBio))) == 0)
                return DefaultValue;

            string result = "";

            // Picking only from Layers that are the same gender as provided or gender-neutral
            // So gendered layers can only be picked if requested, otherwise always ungendered layers
            // will get picked
            foreach (Layer layer in Layers.Where(
                l =>
                (l.Gender == gender || l.Gender == Gender.Neutral)
                &&
                (ageBio == -1 || (l.LowerAgeLimit <= ageBio && l.UpperAgeLimit >= ageBio))))
            {
                double chance = random.NextDouble();
                if (chance <= layer.Chance) // Layer is picked
                    result += layer.GetRandom(gender);
                else // Layer isn't picked
                {
                    if (layer.DefaultValue.Length > 0)
                        result += layer.DefaultValue;
                    if (!IndependentLayers)
                        break;
                }
            }

            return result.Trim();
        }

        public override string ToString()
        {
            string res = "Bundle " + Name + "\n";
            int i = 0;
            foreach (Layer l in Layers)
            {
                res += "Layer #" + i;
                res += l.ToString() + "\n";
                i++;
            }
            return res;
        }

    }
}

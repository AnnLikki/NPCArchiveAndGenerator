using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Archives
{
    /// <summary>
    ///  Bundle is a collection of a specific type and purpose. It contains a selection of elements
    ///  that are interchangable, e.g. Positive Personalities Bundle (with elements like "kind", "generous" etc.)
    ///  or Common Names Bundle ("Steve", "Helen", etc).
    ///  It can also have multiple layers, each one can have a chance to get picked.
    /// </summary>
    public class Bundle
    {
        /// <summary>
        /// Type of the Bundle
        /// </summary>
        public ArchiveType type;
        /// <summary>
        /// Given name of the Bundle, represents it's purpose.
        /// </summary>
        public string name;
        /// <summary>
        /// True if each layer after the first one can be picked regardless if the layer before was picked or not.\n
        /// False if layers can't be picked if the previous layer wasn't picked.\n
        /// Layer that wasn't picked can have a default value that will be used instead.\n
        /// E.g. if true:\n
        /// Layer one - Neutral personality trait (chance=0.5): "Calm", "Neat", "Shrewd"\n
        /// Layer two - Good personality trait (chance=0.5): "Kind", "Empathetic", "Loyal"\n
        /// Layer three - Bad personality trait (chance=0.5): "Evil", "Aggressive", "Greedy"\n
        /// Possible results: "Clam, Kind", "Loyal, Greedy", "Evil" etc.\n
        /// \n
        /// E.g. if false:n\
        /// Layer one - Hair color (chance=1): "Blond", "Black", "Brown", "Ginger"\n
        /// Layer two - Hair length (chance=0.9 default="facial hair, bald head"): "long", "medium length", "short"\n
        /// Layer three - Hairstyle (chance=0.5, default="plain hairstyle"): "Braided hair", "Curly hair", "Up-do"\n
        /// Possible results: "Blond long up-do", "Brown facial hair, bald head", "Ginger medium length plain hairsyle" etc.\n
        /// </summary>
        public bool independentParts;
        /// <summary>
        /// Collection of layers, the higher the index, the lower the layer.
        /// </summary>
        private Collection<Layer> layers;

        Random random = new Random();

        public Bundle(ArchiveType type, string name, bool independentParts)
        {
            this.type = type;
            this.name = name;
            this.independentParts = independentParts;
            this.layers = new Collection<Layer>();
        }

        public void insertLayer(int layerNumber, float chance = 1.0f, string defaultValue = "", Collection<ListElement> elements = null)
        {
            layers.Insert(layerNumber, new Layer(elements, chance, defaultValue));
        }

        public void insertLayer(int layerNumber, Layer layer)
        {
            layers.Insert(layerNumber, layer);
        }

        public void addElementToLayer(int layer, ListElement element)
        {
            layers[layer].getElements().Add(element);
        }

        public Collection<Layer> getAllLayers()
        {
            return layers;
        }

        public List<string> getValuesFromLayer(int layer) { 
            
            List<string> values = new List<string>();
            foreach(ListElement le in layers[layer].getElements())
            {
                values.Add(le.value);
            }

            return values;
        }

        public Layer getLayer(int layer)
        {
            return layers[layer];
        }
        
        public string getRandom()
        {
            string result = "";

            foreach(Layer layer in layers)
            {
                float chance = (float)random.NextDouble();
                if (chance <= layer.chance) // layer is chosen
                {
                    result += layer.getRandom() + " ";
                }
                else // layer isn't chosen
                {
                    if(layer.defaultValue.Length>0)
                        result += layer.defaultValue;
                    if (!independentParts)
                        break;
                }
            }

            return result.Trim();
        }
    }

    public enum ArchiveType
    {
        Name,
        Gender,
        Occupation,
        Personality,
        Height,
        Physique,
        Skin,
        Hair,
        Face,
        Eyes,
        Clothes,
        Features
    }
}

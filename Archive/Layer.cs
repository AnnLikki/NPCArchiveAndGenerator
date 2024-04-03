using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using static Archives.Enums;

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
        /// If ungendered, Layer is compatable with any character.
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// The lowest biological age of a character that this Layer is comapatable with.
        /// </summary>
        public int LowerAgeLimit { get; set; }
        /// <summary>
        /// The highest biological age of a character that this Layer is comapatable with.
        /// </summary>
        public int UpperAgeLimit { get; set; }
        /// <summary>
        /// A collection of elements from which the randomization result is picked. 
        /// If empty, returns the Default Value.
        /// </summary>
        public Collection<WeightedElement> Elements { get; set; }

        /// <summary>
        /// Gets the number of element in this layer.
        /// </summary>
        public int Count { get { return Elements.Count; } }

        private Random random = new Random();

        public Layer(double chance = 1.0, string defaultValue = "", Gender gender = Gender.Neutral, int lowerAgeLimit = 0, int upperAgeLimit = int.MaxValue, Collection<WeightedElement> elements = null)
        {
            DefaultValue = defaultValue;
            Chance = chance;
            Gender = gender;
            LowerAgeLimit = lowerAgeLimit;
            UpperAgeLimit = upperAgeLimit;
            if (elements != null)
                Elements = elements;
            else
                Elements = new Collection<WeightedElement>();
        }

        /// <summary>
        /// Add a WeighedElement. Can't add an element that has been added already. 
        /// Can't add a null.
        /// </summary>
        public void Add(WeightedElement element)
        {
            if(element!=null && !Elements.Contains(element))
                Elements.Add(element);
            else if(element==null)
                throw new ArgumentNullException(nameof(element));
        }

        /// <summary>
        /// Add a new WeighedElement with specified parameters.
        /// </summary>
        public void Add(object value, int weight = 1, Gender gender = Gender.Neutral)
        {
            Elements.Add(new WeightedElement(value, weight, gender));
        }

        /// <summary>
        /// Removes first entry of an element.
        /// </summary>
        /// <returns>
        /// Returns true, if such element was found and was succesfully removed, 
        /// false - if it wasn't found or removed.
        /// </returns>
        public bool Remove(WeightedElement element)
        {
            return Elements.Remove(element);
        }

        /// <summary>
        /// Removes first element with matching value.
        /// </summary>
        /// <param name="value">Object to match.</param>
        /// <returns>
        /// Returns true, if such element was found and was succesfully removed, 
        /// false - if it wasn't found or removed.
        /// </returns>
        public bool RemoveByValue(object value)
        {
            var matchingElement = Elements.FirstOrDefault(el => el.Value.Equals(value));
            if (matchingElement != null)
                return Elements.Remove(matchingElement);
            return false;
        }

        /// <summary>
        /// Removes element at specified index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws ArgumentOutOfRangeException if index is out of bounds.
        /// </exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Elements.Count)
                throw new ArgumentOutOfRangeException("index");
            Elements.RemoveAt(index);
        }

        /// <summary>
        /// Returns layer element at specified index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws ArgumentOutOfRangeException if index is out of bounds.
        /// </exception>
        public WeightedElement Get(int index)
        {
            if(index < 0 || index >= Elements.Count)
                throw new ArgumentOutOfRangeException("index");
            return Elements[index];
        }

        /// <summary>
        /// Returns first element of the Layer with matching value.
        /// </summary>
        /// <param name="value">Object to match.</param>
        /// <returns>First WeightedElement with matching Value. null if no matches found.</returns>
        public WeightedElement GetByValue(object value)
        {
            return Elements.FirstOrDefault(el => el.Value.Equals(value));
        }

        /// <summary>
        /// Removes all elements from the layer.
        /// </summary>
        public void Clear()
        {
            Elements.Clear();
        }

        /// <summary>
        /// Returns weighted random string result from this Layer.
        /// </summary>
        /// <param name="gender">Character's gender. Results are matching this gender or are gender-neutral.
        /// If no gender specification is provided, returns only gender-neutral options.</param>
        /// <returns>Random string Value of one of the weighted elements.         
        /// If the Layer is empty, returns Layer's Default Value.</returns>
        public string GetRandom(Gender gender = Gender.Neutral)
        {
            int totalSum = 0;
            // Only element of the same gender or neutral are included in generation
            // If no gender specification is provided, uses only gender-neutral optionsd1
            foreach (WeightedElement we in Elements.Where(e => e.Gender == gender || e.Gender == Gender.Neutral))
                totalSum += we.Weight;

            int r = random.Next(1, totalSum+1);
            int sum = 0;

            foreach (WeightedElement we in Elements.Where(e => e.Gender == gender || e.Gender == Gender.Neutral))
            {
                sum += we.Weight;
                if (sum >= r)
                    return we.Value.ToString();
            }
            // If there are no elements on this layer
            return DefaultValue;
        }
        
    }
}

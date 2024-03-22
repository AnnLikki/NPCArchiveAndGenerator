using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archives
{
    public class Layer
    {

        public Collection<ListElement> elements { get; set; }
        public float chance { get; set; }
        public string defaultValue { get; set; }

        Random random = new Random();

        public Layer(Collection<ListElement> elements, float chance, string defaultValue)
        {
            if(elements==null)
                this.elements = new Collection<ListElement>();
            else
                this.elements = elements;
            this.chance = chance;
            this.defaultValue = defaultValue;
        }

        public Collection<ListElement> getElements()
        {
            return elements;
        }
        public string getRandom()
        {
            int totalSum = 0;
            foreach(ListElement le in elements)
                totalSum += le.frequency;

            int r = random.Next(totalSum);
            int sum = 0;
       
            foreach (ListElement le in elements)
            {
                sum += le.frequency;
                if (sum >= r)
                    return le.value;
            }
            throw new Exception("Unexpected Behaviour please check");
        }
    }
}

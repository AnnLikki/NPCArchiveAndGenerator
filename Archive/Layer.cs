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

        Collection<ListElement> elements;
        public float chance;
        public string defaultValue;

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
            //Console.WriteLine("___NEW RANDOM___ ");
            foreach (ListElement le in elements)
            {
                //Console.WriteLine("CURRENT ELEMENT " + le.value + " " + le.frequency);

                if (sum >= r)
                {
                    //Console.WriteLine("SUM "+sum + " is greater than RANDOM " + r);
                    return le.value;
                }
                else
                {
                    sum += le.frequency;
                    //Console.WriteLine("SUM increased to " + sum + ", RANDOM " + r);
                }
                
            }
            throw new Exception("Unexpected Behaviour please check");
        }
    }
}

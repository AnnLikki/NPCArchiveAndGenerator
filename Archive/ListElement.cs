using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archives
{
    public class ListElement
    {
        public string value;
        public int frequency;

        public ListElement(string value, int frequency=1)
        {
            this.value = value;
            this.frequency = frequency;
        }
    }
}

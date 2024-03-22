namespace Archives
{
    public class ListElement
    {
        public string value { get; set; }
        public int weight { get; set; }

        public ListElement(string value, int weight = 1)
        {
            this.value = value;
            this.weight = weight;
        }

        public override string ToString()
        {
            return value + ":" + weight;
        }
    }
}

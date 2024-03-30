using System.Collections.ObjectModel;

namespace Archives
{
    /// <summary>
    /// Stores weighted elements.
    /// </summary>
    public class WeightedArchive : ObservableCollection<WeightedElement>
    {

        /// <summary>
        /// Add an instance of any element to archive.
        /// </summary>
        public void Add(object value, int weight = 1, Gender gender = Gender.Neutral)
        {
            Add(new WeightedElement(value, weight, gender));
        }

    }
}

using System.Collections.ObjectModel;

namespace Archives
{
    public class WeightedArchive : ObservableCollection<WeightedElement>
    {
        // TODO check if this thing is needed after the implementation.
        /// <summary>
        /// Value that will be returned if the archive contains no suitable returns.
        /// </summary>
        public object DefaultValue { get; set; }
    }
}

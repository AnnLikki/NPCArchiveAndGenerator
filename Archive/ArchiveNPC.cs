using System.Collections.ObjectModel;

namespace Archives
{
    // ArchiveNPC is a class that inherits from ObresvableCollection, which allows
    // to raise events like ColletionChanged, so the DataGrid will update accordingly.
    // It is a collection of NPC type.
    // In future I am planning to add sorting/filtering by a text query. 
    public class ArchiveNPC : ObservableCollection<NPC>
    {

        private ObservableCollection<NPC> npcs = new ObservableCollection<NPC>();

        public ArchiveNPC() { }

        public ObservableCollection<NPC> NPCs
        {
            get { return npcs; }
            set { npcs = value; }
        }

    }
}

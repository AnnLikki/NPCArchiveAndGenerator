using Archives;
using System.Windows.Controls;

namespace NPCArchiveAndGenerator
{
    public partial class ArchetypesArchivesUC : UserControl
    {
        public ArchetypesArchivesUC()
        {
            InitializeComponent();

            Lbl0.Content = ArchiveHandler.archetypeStorage.ToString();

        }
    }
}

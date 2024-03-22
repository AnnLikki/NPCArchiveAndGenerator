using Archives;
using System.Windows.Controls;

namespace NPCArchiveAndGenerator
{
    public partial class LittleArchivesUC : UserControl
    {
        public LittleArchivesUC()
        {
            InitializeComponent();

            Lbl0.Content = ArchiveHandler.defaultArchives.ToString();

        }
    }
}

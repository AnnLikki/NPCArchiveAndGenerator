using Archives;
using System.Windows.Controls;

namespace NPCArchiveAndGenerator
{
    public partial class BundlesArchivesUC : UserControl
    {
        public BundlesArchivesUC()
        {
            InitializeComponent();

            Lbl0.Content = ArchiveHandler.bundleStorage.ToString();

        }
    }
}

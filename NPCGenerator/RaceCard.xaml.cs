using Archives;
using System.Windows.Controls;

namespace NPCGenerator
{
    // Race Card is a User Control that is placed next to the race archive
    // when sertain race is seleted. Allows to view and change race's data.
    public partial class RaceCard : UserControl
    {
        Race race;
        public RaceCard(Race race)
        {
            InitializeComponent();
            this.race = race;

            // Filling all the fields with race's data.
            nameTB.Text = race.Name;
            descTB.Text = race.Description;
            maturityTB.Text = race.AgeMaturity.ToString();
            expectancyTB.Text = race.LifeExpectancy.ToString();
        }

        // Save button updates the race's data and notifies the DataGrid to update.
        private void saveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            race.updateInfoNotifyably(nameTB.Text, descTB.Text, int.Parse(maturityTB.Text), int.Parse(expectancyTB.Text));
        }

        // Delete button deletes the race from the global archive,
        // consequently changing DataGrid's selection and destroying
        // this Race Card.
        private void deleteBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ArchiveHandler.archiveRace.Remove(race);
        }

    }
}

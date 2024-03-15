using Archives;

namespace Tests
{
    public class Tests1
    {
        Bundle names1, names2;
        ArchiveBundles namesArchive;
        Dictionary<ArchiveType, ArchiveBundles> archives;

        [SetUp]
        public void Setup()
        {
            names1 = new Bundle(
                ArchiveType.Name,
                "Names1",
                true
            );

            // new bundle, no layers
            Assert.That(names1.getAllLayers().Count, Is.EqualTo(0));

            names1.insertLayer(0);

            // checking that there's one layer total
            Assert.That(names1.getAllLayers().Count, Is.EqualTo(1));
            // and it's empty
            Assert.That(names1.getLayer(0).getElements().Count, Is.EqualTo(0));

            names1.addElementToLayer(0, new ListElement("Oleg"));
            names1.addElementToLayer(0, new ListElement("Artem"));
            names1.addElementToLayer(0, new ListElement("Anton"));
            names1.addElementToLayer(0, new ListElement("Katya"));
            names1.addElementToLayer(0, new ListElement("Kira"));

            // populated with new elements
            Assert.That(names1.getLayer(0).getElements().Count, Is.EqualTo(5));

            names2 = new Bundle(
                ArchiveType.Name,
                "Names2",
                true
            );

            // new bundle, no layers
            Assert.That(names2.getAllLayers().Count, Is.EqualTo(0));

            names2.insertLayer(0);

            // checking that there's one layer total
            Assert.That(names2.getAllLayers().Count, Is.EqualTo(1));
            // and it's empty
            Assert.That(names2.getLayer(0).getElements().Count, Is.EqualTo(0));

            names2.addElementToLayer(0, new ListElement("Misha"));
            names2.addElementToLayer(0, new ListElement("Kristina"));
            names2.addElementToLayer(0, new ListElement("Alena"));
            names2.addElementToLayer(0, new ListElement("Lera"));
            names2.addElementToLayer(0, new ListElement("Kolya"));

            // populated with new elements
            Assert.That(names2.getLayer(0).getElements().Count, Is.EqualTo(5));

            namesArchive = new ArchiveBundles();
            namesArchive.Add(names1);
            namesArchive.Add(names2);

            archives = new Dictionary<ArchiveType, ArchiveBundles>();
            archives.Add(ArchiveType.Name, namesArchive);
        }

        // Tests picking elements from all bundles combined
        [Test]
        public void PickingFromAll()
        {

            bool youGood = false;
            for (int i = 0; i < 10; i++)
            {
                string randomName1 = archives[ArchiveType.Name].getRandomFromAny();
                string randomName2 = archives[ArchiveType.Name].getRandomFromAny();
                if (randomName1 != randomName2)
                {
                    youGood = true;
                    break;
                }

            }
            if (!youGood)
            {
                // cheking that different names are picked
                Assert.Fail();
            }

            youGood = false;
            for (int i = 0; i < 10; i++)
            {
                string randomName1 = archives[ArchiveType.Name].getRandomFromAny();
                string randomName2 = archives[ArchiveType.Name].getRandomFromAny();

                if ((names1.getValuesFromLayer(0).Contains(randomName1) && names2.getValuesFromLayer(0).Contains(randomName2)) ||
                    (names1.getValuesFromLayer(0).Contains(randomName2) && names2.getValuesFromLayer(0).Contains(randomName1)))
                {
                    youGood = true;
                    break;
                }
            }
            if (!youGood)
            {
                // cheking that different bundles are picked
                Assert.Fail();
            }
        }

        // Tests picking elements from one bundle
        [Test]
        public void PickingFromParticular()
        {
            for (int i = 0; i < 10; i++)
            {
                string randomName1 = archives[ArchiveType.Name].getBundle(0).getRandom();

                // random name was picked from the first bundle
                Assert.IsTrue(names1.getValuesFromLayer(0).Contains(randomName1));

                string randomName2 = archives[ArchiveType.Name].getBundle(1).getRandom();

                // random name was picked from the second bundle
                Assert.IsTrue(names2.getValuesFromLayer(0).Contains(randomName2));
            }

            for (int i = 0; i < 10; i++)
            {
                string randomName1 = archives[ArchiveType.Name].getBundle(0).getRandom();
                string randomName2 = archives[ArchiveType.Name].getBundle(0).getRandom();
                if (randomName1 != randomName2)
                    break;

                // cheking that different names are picked
                Assert.Fail();
            }
        }



    }
}
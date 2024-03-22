using Archives;

namespace Tests
{
    public class IntegrationTests
    {
        Bundle names1, names2;
        AgeRange adult, child;
        ArchiveBundles namesArchive;
        ArchiveAgeRange ageRanges;
        ArchiveDictionary archives;

        [SetUp]
        public void Setup()
        {

            names1 = new Bundle(ArchiveType.Name, "Names1", true);

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

            names2 = new Bundle(ArchiveType.Name, "Names2", true);

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

            archives = new ArchiveDictionary();
            archives.Add(ArchiveType.Name, namesArchive);


            // age ranges
            ageRanges = new ArchiveAgeRange();
            adult = new AgeRange("Adult", 35, 55);
            ageRanges.Add(adult);

            child = new AgeRange("Child", 5, 14);
            ageRanges.Add(child);

        }

        // Tests picking elements from all bundles combined
        [Test]
        public void PickingFromAll()
        {

            bool youGood = false;
            for (int i = 0; i < 10; i++)
            {
                string randomName1 = archives[ArchiveType.Name].getRandomFromAnyOrDefault();
                string randomName2 = archives[ArchiveType.Name].getRandomFromAnyOrDefault();
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
                string randomName1 = archives[ArchiveType.Name].getRandomFromAnyOrDefault();
                string randomName2 = archives[ArchiveType.Name].getRandomFromAnyOrDefault();

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

            bool youGood = false;
            for (int i = 0; i < 10; i++)
            {
                string randomName1 = archives[ArchiveType.Name].getBundle(0).getRandom();
                string randomName2 = archives[ArchiveType.Name].getBundle(0).getRandom();
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
        }

        // Tests generating ages via age range from all ranges combined
        [Test]
        public void AgeRangeGen()
        {
            int tests = 1000;
            for (int i = 0; i < tests; i++)
            {
                int age = ageRanges.getAnyRandomOrDefault();
                // All the generated ages fall into this interval
                //"Adult", 35, 55
                //"Child", 5, 14
                Assert.True((age >= 5 && age <= 14) || (age >= 35 && age <= 55));
            }
        }

        // Tests pulling from not existing archives
        // We added only names bundles to the setup, let's try getting any other type of data,
        // it should return a specified default value
        [Test]
        public void NonExistentKeyPulling()
        {
            string str = archives.getRandomFromAnyOrDefault(ArchiveType.Face, "face");
            Assert.True(str == "face");
        }

        // Tests pulling from an empty ArchiveBundle
        [Test]
        public void EmptyArchiveBundlesPulling()
        {
            ArchiveBundles faces = new ArchiveBundles();
            archives.Add(ArchiveType.Face, faces);

            string str = archives[ArchiveType.Face].getRandomFromAnyOrDefault("face");
            Assert.True(str == "face");
        }

        // Tests pulling from empty ArchiveAgeRange 
        [Test]
        public void EmptyArchiveAgeRangePulling()
        {
            ArchiveAgeRange ageRanges = new ArchiveAgeRange();

            int age = ageRanges.getAnyRandomOrDefault(10);
            Assert.True(age == 10);
        }

    }
}
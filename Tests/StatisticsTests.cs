using Archives;

namespace Tests
{
    public class StatisticsTests
    {
        Bundle names; // first, middle and last names with chance and no dependency
        Bundle hair; // color, length, hairsyle with frequencies, chances and dependencies
        Bundle hairColors; // just hair colors, different frequency
        AgeRange adult, child;
        ArchiveAgeRange ageRanges;
        ArchiveBundles namesArchive;
        ArchiveBundles hairArchive;
        Dictionary<ArchiveType, ArchiveBundles> archives;

        [SetUp]
        public void Setup()
        {
            namesArchive = new ArchiveBundles();
            hairArchive = new ArchiveBundles();
            archives = new Dictionary<ArchiveType, ArchiveBundles>();
            archives.Add(ArchiveType.Name, namesArchive);
            archives.Add(ArchiveType.Hair, hairArchive);

            // first, middle and last names with chance and no dependency
            names = new Bundle(ArchiveType.Name, "Names", true);

            // Layer 0 - first names, 100% chance
            names.insertLayer(0);

            names.addElementToLayer(0, new ListElement("Oleg"));
            names.addElementToLayer(0, new ListElement("Artem"));
            names.addElementToLayer(0, new ListElement("Anton"));
            names.addElementToLayer(0, new ListElement("Katya"));
            names.addElementToLayer(0, new ListElement("Kira"));

            // Layer 1 - midlle names, 30% chance
            names.insertLayer(1, 0.3f);

            names.addElementToLayer(1, new ListElement("Misha"));
            names.addElementToLayer(1, new ListElement("Kristina"));
            names.addElementToLayer(1, new ListElement("Alena"));
            names.addElementToLayer(1, new ListElement("Lera"));
            names.addElementToLayer(1, new ListElement("Kolya"));

            // Layer 2 - last names, 70% chance
            names.insertLayer(2, 0.7f);

            names.addElementToLayer(2, new ListElement("Sidorov"));
            names.addElementToLayer(2, new ListElement("Matveev"));
            names.addElementToLayer(2, new ListElement("Ivanov"));
            names.addElementToLayer(2, new ListElement("Petrov"));
            names.addElementToLayer(2, new ListElement("Sokolov"));

            namesArchive.Add(names);

            // color, length, hairsyle with frequencies, chances and dependencies
            hair = new Bundle(ArchiveType.Hair, "Hair", false);

            // Layer 0 - color, 100% chance
            hair.insertLayer(0);

            hair.addElementToLayer(0, new ListElement("Ginger"));
            hair.addElementToLayer(0, new ListElement("Brown"));
            hair.addElementToLayer(0, new ListElement("Black"));
            hair.addElementToLayer(0, new ListElement("Blond"));

            // Layer 1 - lengths, 90% chance (10% is default value "Bald")
            hair.insertLayer(1, 0.9f, "facial hair, bald head");

            hair.addElementToLayer(1, new ListElement("long"));
            hair.addElementToLayer(1, new ListElement("medium length"));
            hair.addElementToLayer(1, new ListElement("short"));

            // Layer 2 - hairstyle, 80% chance if the previous was picked,
            // so objective chance is 90%*80%=72%
            // Objective chance of default is 90%*20%=18% 
            hair.insertLayer(2, 0.8f, "plain hair");

            hair.addElementToLayer(2, new ListElement("curly hair"));
            hair.addElementToLayer(2, new ListElement("up-do"));
            hair.addElementToLayer(2, new ListElement("braided hair"));
            hair.addElementToLayer(2, new ListElement("hair with jewelery"));
            hair.addElementToLayer(2, new ListElement("neatly trimmed hair"));

            hairArchive.Add(hair);


            // just hair colors, different frequency
            hairColors = new Bundle(ArchiveType.Hair, "Hair Colors", true);

            // Layer 0 - color, 100% chance
            hairColors.insertLayer(0);

            hairColors.addElementToLayer(0, new ListElement("Ginger", 5)); // 5% of gingers
            hairColors.addElementToLayer(0, new ListElement("Brown", 25)); // 25% of brown hair
            hairColors.addElementToLayer(0, new ListElement("Black", 60)); // 60% of black hair
            hairColors.addElementToLayer(0, new ListElement("Blond", 10)); // 10% of blond hair

            hairArchive.Add(hairColors);



            ageRanges = new ArchiveAgeRange();
            adult = new AgeRange("Adult", 35, 55);
            ageRanges.Add(adult);

            child = new AgeRange("Child", 5, 14);
            ageRanges.Add(child);
        }

        // Tests if generated compound results with independent parts have right statistics
        [Test]
        public void IndependentCompoundStatistics()
        {
            int firstNames = 0, middleNames = 0, lastNames = 0;

            int tests = 1000;
            for (int i = 0; i < tests; i++)
            {

                int firstNamesTmp = 0, middleNamesTmp = 0, lastNamesTmp = 0;

                string name = archives[ArchiveType.Name].getRandomFromAny();

                foreach (string fn in names.getValuesFromLayer(0))
                    if (name.Contains(fn))
                        firstNamesTmp++;

                foreach (string mn in names.getValuesFromLayer(1))
                    if (name.Contains(mn))
                        middleNamesTmp++;

                foreach (string ln in names.getValuesFromLayer(2))
                    if (name.Contains(ln))
                        lastNamesTmp++;

                // a proper ammount of names from each category is used
                Assert.That(firstNamesTmp == 1);
                Assert.That(middleNamesTmp <= 1);
                Assert.That(lastNamesTmp <= 1);

                firstNames += firstNamesTmp;
                middleNames += middleNamesTmp;
                lastNames += lastNamesTmp;

                Console.WriteLine(name);

            }

            // all the names should have a first name
            Assert.True((double)firstNames / tests == 1.0f);
            // middle names have a chance of 0.3, the results on a large scale should represent that
            Assert.True((double)middleNames / tests > 0.2f || (double)middleNames / tests > 0.4f);
            // last names have a chance of 0.7
            Assert.True((double)lastNames / tests > 0.6f || (double)lastNames / tests > 0.8f);

        }

        // Tests if generated compound results with dependent parts have right statistics
        // and if the default values are applied properly
        [Test]
        public void DependentCompoundStatistics()
        {
            int color = 0, length = 0, bald = 0, hairstyle = 0, plain = 0;

            int tests = 1000;
            for (int i = 0; i < tests; i++)
            {
                int colorT = 0, lengthT = 0, baldT = 0, hairstyleT = 0, plainT = 0;

                string hairStr = archives[ArchiveType.Hair].getBundle(0).getRandom();

                foreach (string c in hair.getValuesFromLayer(0))
                    if (hairStr.Contains(c))
                        colorT++;

                foreach (string l in hair.getValuesFromLayer(1))
                    if (hairStr.Contains(l))
                        lengthT++;

                if (hairStr.Contains(hair.getLayer(1).defaultValue))
                    baldT++;

                foreach (string h in hair.getValuesFromLayer(2))
                    if (hairStr.Contains(h))
                        hairstyleT++;

                if (hairStr.Contains(hair.getLayer(2).defaultValue))
                    plainT++;

                // a proper ammount of names from each category is used
                Assert.True(colorT == 1);
                Assert.True((lengthT == 1 && baldT == 0) || (lengthT == 0 && baldT == 1));
                Assert.True((hairstyleT <= 1 && plainT == 0) || (hairstyleT == 0 && plainT <= 1));

                color += colorT;
                length += lengthT;
                bald += baldT;
                hairstyle += hairstyleT;
                plain += plainT;

                Console.WriteLine(hairStr);

            }

            // color, 100% chance
            Assert.True((double)color / tests == 1.0f);
            // lengths, 90% chance
            Assert.True((double)length / tests > 0.8f || (double)length / tests < 1.0f);
            // baldness, 10% chance
            Assert.That((double)bald / tests > 0.1f || (double)bald / tests < 0.2f);
            // hairstyle, 90%*80%=72% chance
            Assert.That((double)hairstyle / tests > 0.6f || (double)bald / tests < 0.8f);
            // plain, 90%*20%=18% 
            Assert.That((double)plain / tests > 0.1f || (double)bald / tests < 0.25f);

        }

        // Tests if generated results represent set frequencies of elements
        [Test]
        public void ElementFrequencyStatistics()
        {
            int ginger = 0, brown = 0, black = 0, blond = 0;

            int tests = 5000;
            for (int i = 0; i < tests; i++)
            {
                string hairStr = archives[ArchiveType.Hair].getBundle(1).getRandom();

                switch (hairStr)
                {
                    case "Ginger":
                        ginger++;
                        break;
                    case "Brown":
                        brown++;
                        break;
                    case "Black":
                        black++;
                        break;
                    case "Blond":
                        blond++;
                        break;
                    default:
                        Assert.Fail();
                        break;
                }
            }

            Console.WriteLine(Math.Round((double)ginger / tests * 100) + "% of gingers");
            Console.WriteLine(Math.Round((double)brown / tests * 100) + "% of brown hair");
            Console.WriteLine(Math.Round((double)black / tests * 100) + "% of black hair");
            Console.WriteLine(Math.Round((double)blond / tests * 100) + "% of blond hair");

            // 5% of gingers
            Assert.True((double)ginger / tests > 0.02f || (double)ginger / tests < 0.08f);
            // 25% of brown hair
            Assert.True((double)brown / tests > 0.22f || (double)brown / tests < 0.28f);
            // 60% of black hair
            Assert.True((double)black / tests > 0.57f || (double)black / tests < 0.63f);
            // 10% of blond hair
            Assert.True((double)blond / tests > 0.07f || (double)blond / tests < 0.13f);

        }


        // Tests that generated ages are equally spread among ranges if picked from all
        [Test]
        public void AgeRangeGen()
        {

            int children = 0, adults = 0;
            int tests = 1000;
            for (int i = 0; i < tests; i++)
            {
                int age = ageRanges.getAnyRandom();
                if ((age >= 5 && age <= 14))
                    children++;
                if (age >= 35 && age <= 14)
                    adults++;
            }
            Assert.True((double)children / tests > 0.45f || (double)children / tests < 0.55f);
            Assert.True((double)adults / tests > 0.45f || (double)adults / tests < 0.55f);
        }
    }
}
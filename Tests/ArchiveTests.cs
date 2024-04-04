using Archives;
using NUnit.Framework;
using static Archives.Enums;

namespace Tests
{
    internal class ArchiveTests
    {
        // Archive Storage stores instances of objects like Bundles, Races and Archetypes
        // Can be accessed with IDs to get Bundles and Races

        // Weighted Archive inherits from ObservableColection<WeightedElement>
        // It serves the purpuse of storing any weighted objects, so their frequency
        // of occurances can be tweaked
        // It is used to store: Bundle IDs, Race IDs and Genders

        // Age Distribution is very similar to Weighted Archive, and specializes in storing
        // weighted integers (biological ages)

        // ArchiveStorage testing

        [Test]
        public void ASBasicManipulation()
        {
            ArchiveStorage storage = new ArchiveStorage();

            // Adding races
            Assert.That(!storage.ContainsKey(ArchiveType.Race));
            Race race = new Race("Race", "", 10, 100);
            storage.Add(race);
            Assert.That(storage.ContainsKey(ArchiveType.Race));
            Assert.That(storage[ArchiveType.Race], Does.Contain(race));

            // Adding archetypes
            Assert.That(!storage.ContainsKey(ArchiveType.Archetype));
            Archetype archetype = new Archetype("Archetype");
            storage.Add(archetype);
            Assert.That(storage.ContainsKey(ArchiveType.Archetype));
            Assert.That(storage[ArchiveType.Archetype], Does.Contain(archetype));

            // Adding bundles
            Assert.That(!storage.ContainsKey(ArchiveType.Name));
            Bundle bundle = new Bundle("Bundle");
            storage.Add(ArchiveType.Name, bundle);
            Assert.That(storage.ContainsKey(ArchiveType.Name));
            Assert.That(storage[ArchiveType.Name], Does.Contain(bundle));

            // Removing element by object
            storage.Remove(ArchiveType.Name, bundle);
            Assert.That(storage[ArchiveType.Name], Does.Not.Contain(bundle));

            // Removing element by index
            storage.RemoveAt(ArchiveType.Race, 0);
            Assert.That(storage[ArchiveType.Race], Does.Not.Contain(race));

        }

        [Test]
        public void ASSearching()
        {
            ArchiveStorage storage = new ArchiveStorage();

            // Searching races by id
            Race r1 = new Race("r1");
            storage.Add(r1);
            Guid guidr1 = r1.Id;
            Assert.That(r1.Equals(storage.FindRace(guidr1)));

            // Searching bundles by id
            Bundle b1 = new Bundle("b1");
            storage.Add(ArchiveType.Name, b1);
            Guid guidb1 = b1.Id;
            Assert.That(b1.Equals(storage.FindBundle(ArchiveType.Name, guidb1)));

            storage.Clear();


            // Edge cases of searching

            // Searching in non-existent archives: race and regular
            Assert.That(storage.FindRace(Guid.NewGuid()), Is.EqualTo(null));
            Assert.That(storage.FindBundle(ArchiveType.Name, Guid.NewGuid()), Is.EqualTo(null));

            // Searching for non-existent object
            storage.Add(r1);
            Assert.That(storage.FindRace(Guid.NewGuid()), Is.EqualTo(null));
            storage.Add(ArchiveType.Name, b1);
            Assert.That(storage.FindBundle(ArchiveType.Name, Guid.NewGuid()), Is.EqualTo(null));
        }


        // WeightedArchive testing

        [Test]
        public void WABasicManipulation()
        {
            WeightedArchive wa = new WeightedArchive();

            // Adding new elements
            WeightedElement we1 = new WeightedElement("E1");
            wa.Add(we1);
            Assert.That(wa, Has.Count.EqualTo(1));
            Assert.That(wa, Does.Contain(we1));
            wa.AddElement("E2");
            Assert.That(wa, Has.Count.EqualTo(2));
            Assert.That(wa[1].Value, Is.EqualTo("E2"));

        }


        // Getting a randomized result without restrictions
        [Test]
        public void WAUnrestrictedRandomization()
        {
            WeightedArchive wa = new WeightedArchive();

            wa.AddElement("E0", 0);
            wa.AddElement("E1", 1);
            wa.AddElement("E2", 2, Gender.Neutral);
            wa.AddElement("E3", 3, Gender.Male);
            wa.AddElement("E4", 4, Gender.Female);

            int tests = 1000;
            int[] results = new int[5];
            for (int i = 0; i < tests; i++)
            {
                var result = wa.GetRandomUnrestricted().ToString();
                if (result == "E0")
                    results[0]++;
                if (result == "E1")
                    results[1]++;
                if (result == "E2")
                    results[2]++;
                if (result == "E3")
                    results[3]++;
                if (result == "E4")
                    results[4]++;

            }

            Assert.That(results[0], Is.EqualTo(0));
            Assert.That((double)results[1]/tests >= 1.0 / 10.0 - 0.05 && (double)results[1] / tests <= 1.0 / 10.0 + 0.05);
            Assert.That((double)results[2] / tests >= 2.0 / 10.0 - 0.05 && (double)results[2] / tests <= 2.0 / 10.0 + 0.05);
            Assert.That((double)results[3] / tests >= 3.0 / 10.0 - 0.05 && (double)results[3] / tests <= 3.0 / 10.0 + 0.05);
            Assert.That((double)results[4] / tests >= 4.0 / 10.0 - 0.05 && (double)results[4] / tests <= 4.0 / 10.0 + 0.05);

        }

        [Test]
        public void WABasicRandomFromBundleViaIDs()
        {
            ArchiveStorage storage = new ArchiveStorage();
            WeightedArchive wa = new WeightedArchive();

            Bundle bundle1 = new("B1");
            bundle1.InsertNewLayer(0);
            bundle1.AddToLayer(0, "B1E");
            storage.Add(ArchiveType.Name, bundle1);

            Bundle bundle2 = new("B2");
            bundle2.InsertNewLayer(0);
            bundle2.AddToLayer(0, "B2E");
            storage.Add(ArchiveType.Name, bundle2);

            Bundle bundle3 = new("B3");
            bundle3.InsertNewLayer(0);
            bundle3.AddToLayer(0, "B3E");
            storage.Add(ArchiveType.Name, bundle3);

            wa.AddElement(bundle1.Id, 0);
            wa.AddElement(bundle2.Id, 1);
            wa.AddElement(bundle3.Id, 2);


            int tests = 1000;
            int[] results = new int[3];
            for (int i = 0; i < tests; i++)
            {
                string result = wa.GetRandomFromBundle(storage, ArchiveType.Name);
                if (result == "B1E")
                    results[0]++;
                if (result == "B2E")
                    results[1]++;
                if (result == "B3E")
                    results[2]++;
            }

            Assert.That(results[0], Is.EqualTo(0));
            Assert.That((double)results[1] / tests >= 1.0 / 3.0 - 0.05 && (double)results[1] / tests <= 1.0 / 3.0 + 0.05);
            Assert.That((double)results[2] / tests >= 2.0 / 3.0 - 0.05 && (double)results[2] / tests <= 2.0 / 3.0 + 0.05);
        }

        [Test]
        public void WAGenderedRandomFromBundleViaIDs()
        {
            ArchiveStorage storage = new ArchiveStorage();
            WeightedArchive wa = new WeightedArchive();

            Bundle bundle1 = new("B1");
            bundle1.InsertNewLayer(0);
            bundle1.AddToLayer(0, "F");
            storage.Add(ArchiveType.Name, bundle1);

            Bundle bundle2 = new("B2");
            bundle2.InsertNewLayer(0);
            bundle2.AddToLayer(0, "M");
            storage.Add(ArchiveType.Name, bundle2);

            Bundle bundle3 = new("B3");
            bundle3.InsertNewLayer(0);
            bundle3.AddToLayer(0, "N");
            storage.Add(ArchiveType.Name, bundle3);

            wa.AddElement(bundle1.Id, 1, Gender.Female);
            wa.AddElement(bundle2.Id, 1, Gender.Male);
            wa.AddElement(bundle3.Id, 1, Gender.Neutral);


            int tests = 1000;
            int[] resMale = new int[3];
            int[] resFemale = new int[3];
            int[] resNeutral = new int[3];

            for (int i = 0; i < tests; i++)
            {
                string result = wa.GetRandomFromBundle(storage, ArchiveType.Name, Gender.Female);
                if (result == "F")
                    resFemale[0]++;
                else if (result == "M")
                    resFemale[1]++;
                else if (result == "N")
                    resFemale[2]++;

                result = wa.GetRandomFromBundle(storage, ArchiveType.Name, Gender.Male);
                if (result == "F")
                    resMale[0]++;
                else if (result == "M")
                    resMale[1]++;
                else if (result == "N")
                    resMale[2]++;

                result = wa.GetRandomFromBundle(storage, ArchiveType.Name, Gender.Neutral);
                if (result == "F")
                    resNeutral[0]++;
                else if (result == "M")
                    resNeutral[1]++;
                else if (result == "N")
                    resNeutral[2]++;
            }

            // Female
            Assert.That(resFemale[1], Is.EqualTo(0));
            Assert.That((double)resFemale[0] / tests >= 1.0 / 2.0 - 0.05 && (double)resFemale[0] / tests <= 1.0 / 2.0 + 0.05);
            Assert.That((double)resFemale[2] / tests >= 1.0 / 2.0 - 0.05 && (double)resFemale[2] / tests <= 1.0 / 2.0 + 0.05);

            // Male
            Assert.That(resMale[0], Is.EqualTo(0));
            Assert.That((double)resMale[1] / tests >= 1.0 / 2.0 - 0.05 && (double)resMale[1] / tests <= 1.0 / 2.0 + 0.05);
            Assert.That((double)resMale[2] / tests >= 1.0 / 2.0 - 0.05 && (double)resMale[2] / tests <= 1.0 / 2.0 + 0.05);

            // Neutral
            Assert.That(resNeutral[2], Is.EqualTo(tests));
            Assert.That(resNeutral[0], Is.EqualTo(0));
            Assert.That(resNeutral[1], Is.EqualTo(0));
        }

        [Test]
        public void WAAgedRandomFromBundleViaIDs()
        {
            ArchiveStorage storage = new ArchiveStorage();
            WeightedArchive wa = new WeightedArchive();

            Bundle bundle1 = new("B1", true, Gender.Neutral, 10, 20);
            bundle1.InsertNewLayer(0);
            bundle1.AddToLayer(0, "1");
            storage.Add(ArchiveType.Name, bundle1);

            Bundle bundle2 = new("B2", true, Gender.Neutral, 30, 40);
            bundle2.InsertNewLayer(0);
            bundle2.AddToLayer(0, "2");
            storage.Add(ArchiveType.Name, bundle2);

            Bundle bundle3 = new("B3", true, Gender.Neutral, 10, 40);
            bundle3.InsertNewLayer(0);
            bundle3.AddToLayer(0, "3");
            storage.Add(ArchiveType.Name, bundle3);

            wa.AddElement(bundle1.Id);
            wa.AddElement(bundle2.Id);
            wa.AddElement(bundle3.Id);


            int tests = 1000;
            Random random = new Random();

            for (int i = 0; i < tests; i++)
            {
                int age = random.Next();
                string result = wa.GetRandomFromBundle(storage, ArchiveType.Name, Gender.Neutral, age);

                if (result == "1")
                    Assert.That(age>=10 && age<=20);
                else if (result == "2")
                    Assert.That(age >= 30 && age <= 40);
                else if (result == "3")
                    Assert.That(age >= 10 && age <= 40);

            }
        }

        [Test]
        public void WAIntersection()
        {
            WeightedArchive wa1 = new WeightedArchive();
            wa1.AddElement("A");
            wa1.AddElement("B"); 
            wa1.AddElement("C");

            WeightedArchive wa2 = new WeightedArchive();
            wa2.AddElement("B");
            wa2.AddElement("C");
            wa2.AddElement("D");

            WeightedArchive wa3 = wa1.GetIntersection(wa2);

            Assert.That(wa3.Any(el => el.Value.Equals("B")));
            Assert.That(wa3.Any(el => el.Value.Equals("C")));
            Assert.That(wa3.Count==2);
        }

        [Test]
        public void WAEdgeCases()
        {
            WeightedArchive wa = new WeightedArchive();
            ArchiveStorage storage = new ArchiveStorage();

            // Randomizing from an empty archive
            wa.DefaultValue = "Default";
            for (int i = 0; i < 10; i++)
                Assert.That(wa.GetRandomUnrestricted(), Is.EqualTo("Default"));

            // Randomizing elements with weight = 0
            wa.AddElement("1", 0);
            wa.AddElement("2", 0);
            wa.AddElement("3", 0);
            for (int i = 0; i < 10; i++)
                Assert.That(wa.GetRandomUnrestricted(), Is.EqualTo("Default"));
            wa.Clear();

            // Randomizing from bundles that are not in the storage
            wa.AddElement(new Bundle("B1").Id);
            wa.AddElement(new Bundle("B2").Id);
            for (int i = 0; i < 10; i++)
                Assert.That(wa.GetRandomFromBundle(storage, ArchiveType.Name), Is.EqualTo("Default"));

            // Randomizing from only unsuitable bundles
            Bundle bundle1 = new Bundle("B1", true, Gender.Male);
            bundle1.InsertNewLayer(0);
            bundle1.AddToLayer(0, "B1E");
            Bundle bundle2 = new Bundle("B2", true, Gender.Neutral, 50, 100);
            bundle2.InsertNewLayer(0);
            bundle2.AddToLayer(0, "B2E");
            wa.AddElement(bundle1.Id, 1, Gender.Male);
            wa.AddElement(bundle2.Id);
            storage.Add(ArchiveType.Name, bundle1);
            storage.Add(ArchiveType.Name, bundle2);
            for (int i = 0; i < 10; i++)
                Assert.That(wa.GetRandomFromBundle(storage, ArchiveType.Name, Gender.Female, 15), Is.EqualTo("Default"));
        }


        // AgeDistribution testing

        [Test]
        public void ADBasicManipulation()
        {
            AgeDistribution ag = new AgeDistribution();

            // Adding single elements
            ag.AddAge(12);
            ag.AddAge(23, 5);
            Assert.That(ag.Any(el => el.Value.Equals(12)));
            Assert.That(ag.Any(el => el.Value.Equals(23)&& el.Weight.Equals(5)));
            Assert.That(ag.Count == 2);

            ag.Clear();

            // Adding ranges
            ag.AddRange(30, 40, 2);
            Assert.That(ag.Count == 11);
            ag.AddRange(50, 55, 2);
            Assert.That(ag.Count == 17);

            ag.Clear();

            // Overlaps
            ag.AddAge(12, 2);
            Assert.That(ag.Any(el => el.Value.Equals(12) && el.Weight.Equals(2)));
            Assert.That(ag.Count == 1);
            ag.AddAge(12, 5);
            Assert.That(ag.Any(el => el.Value.Equals(12) && el.Weight.Equals(5)));
            Assert.That(ag.Count == 1);

            ag.Clear();

            ag.AddRange(30, 40, 2);
            Assert.That(ag.Count == 11);
            Assert.That(ag.Any(el => el.Value.Equals(38) && el.Weight.Equals(2)));
            ag.AddRange(35, 45, 5);
            Assert.That(ag.Count == 16);
            Assert.That(ag.Any(el => el.Value.Equals(38) && el.Weight.Equals(5)));


            // Edge cases

            // From > to
            Assert.Throws<ArgumentException>(()=> ag.AddRange(10, 5));


        }

        [Test]
        public void ADRandomization()
        {
            AgeDistribution ad = new AgeDistribution();

            ad.AddRange(1, 3, 5);
            ad.AddRange(7, 9, 2);

            int tests = 1000;
            int[] results = new int[7];
            for (int i = 0; i < tests; i++)
            {
                var result = ad.GetRandom();
                if (result == 1)
                    results[1]++;
                else if (result == 2)
                    results[2]++;
                else if(result == 3)
                    results[3]++;
                else if(result == 7)
                    results[4]++;
                else if(result == 8)
                    results[5]++;
                else if(result == 9)
                    results[6]++;
                else 
                    results[0]++;

            }

            Assert.That(results[0], Is.EqualTo(0));
            for(int i = 1; i<4; i++)
            {
                Assert.That((double)results[i] / tests >= 5.0 / 21.0 - 0.05 && (double)results[i] / tests <= 5.0 / 21.0 + 0.05);
            }
            for (int i = 4; i < 7; i++)
            {
                Assert.That((double)results[i] / tests >= 2.0 / 21.0 - 0.05 && (double)results[i] / tests <= 2.0 / 21.0 + 0.05);
            }

        }

        [Test]
        public void ADIntersection()
        {
            AgeDistribution ad1 = new AgeDistribution();
            ad1.AddAge(1);
            ad1.AddAge(2);
            ad1.AddAge(3);

            AgeDistribution ad2 = new AgeDistribution();
            ad2.AddAge(2);
            ad2.AddAge(3);
            ad2.AddAge(4);

            AgeDistribution ad3 = ad1.GetIntersection(ad2);

            Assert.That(ad3.Any(el => el.Value.Equals(2)));
            Assert.That(ad3.Any(el => el.Value.Equals(3)));
            Assert.That(ad3.Count == 2);
        }

    }
}

using Archives;
using NUnit.Framework;
using static Archives.Enums;

namespace Tests
{
    internal class ArchetypeTests
    {

        //TODO fix comment

        // Archetype, Kit and Race testing

        // Kits inherit from Dictionary<ArchiveType, WeightedArchive> and basically are sets of weighted archives
        // that can store IDs of bundles and races.
        // Kits are used as achetype and race containers of compatable objects.


        // Kit testing

        [Test]
        public void KitBasicManipulation()
        {
            Kit kit = new Kit();

            // Adding bundles as-is and via its ID
            // In both cases should add just the ID
            Assert.That(!kit.ContainsKey(BundleType.Name));
            Bundle bundle = new Bundle("Bundle");
            kit.AddBundle(BundleType.Name, bundle);
            Assert.That(kit[BundleType.Name].Any(el => el.Value.Equals(bundle.Id)), Is.True);

            kit.Clear();
            Assert.That(!kit.ContainsKey(BundleType.Name));
            kit.AddBundle(BundleType.Name, bundle.Id);
            Assert.That(kit[BundleType.Name].Any(el => el.Value.Equals(bundle.Id)), Is.True);

        }

        [Test]
        public void KitRandomization()
        {
            Kit kit = new Kit();
            BundleStorage storage = new BundleStorage();

            // Bundle randomization

            Bundle bundle1 = new("B1");
            bundle1.InsertNewLayer(0);
            bundle1.AddToLayer(0, "1");
            storage.Add(BundleType.Name, bundle1);

            Bundle bundle2 = new("B2");
            bundle2.InsertNewLayer(0);
            bundle2.AddToLayer(0, "2");
            storage.Add(BundleType.Name, bundle2);

            kit.AddBundle(BundleType.Name, bundle1);
            kit.AddBundle(BundleType.Name, bundle2);

            for (int i = 0; i < 10; i++)
            {
                string res = kit.GetRandomFromBundle(storage, BundleType.Name);
                Assert.That(res.Equals("1") || res.Equals("2"));
            }
        }

        [Test]
        public void KitEdgeCases()
        {
            Kit kit = new Kit();
            BundleStorage storage = new BundleStorage();

            // Randomization from a non-existent archive
            // Bundle
            Assert.That(kit.GetRandomFromBundle(storage, BundleType.Name), Is.EqualTo(null));

            kit.Clear();

            // Randomization from an empty archive
            // Bundle
            kit.Add(BundleType.Name, new WeightedArchive());
            Assert.That(kit.GetRandomFromBundle(storage, BundleType.Name), Is.EqualTo(kit[BundleType.Name].DefaultValue.ToString()));
        }


        // Race testing


        [Test]
        public void RaceGenderSetup()
        {
            Race race = new Race("Race");

            // Adding the same gender is prohibited, doesn't throw an exception, changes weight of the element
            Assert.That(race.Genders.Count == 0);
            race.SetGender(Gender.Female);
            Assert.That(race.Genders.Count == 1);
            race.SetGender(Gender.Female);
            Assert.That(race.Genders.Count == 1);
            race.SetGender(Gender.Male);
            Assert.That(race.Genders.Count == 2);

            // Remove gender
            race.RemoveGender(Gender.Female);
            Assert.That(race.Genders.Count == 1);
            race.RemoveGender(Gender.Female);
            Assert.That(race.Genders.Count == 1);
            race.RemoveGender(Gender.Male);
            Assert.That(race.Genders.Count == 0);

        }

        // Archetype testing

        // Generation happens through archetype
        // Step 1. It generates a race from its list of races
        // Step 2. It generates age and gender by intersecting its and race's age dists and genders
        // Step 3. It generates results from bundles using gender and race. If it has corresponding archives and
        // they aren't empty, it generates from them, otherwise it generates from the race's

        [Test]
        public void ArchetypeBasicGen()
        {
            Archetype archetype = new Archetype("Archetype");
            BundleStorage bundleStorage = new BundleStorage();
            RaceStorage raceStorage = new RaceStorage();
            ArchetypeStorage archetypeStorage = new ArchetypeStorage();
            archetypeStorage.Add(archetype);

            //Set up

            archetype.SetGender(Gender.Female);
            archetype.SetGender(Gender.Neutral);

            archetype.Ages.AddRange(10, 20);

            Race race = new Race("Race");
            race.SetGender(Gender.Male);
            race.SetGender(Gender.Female);
            race.AgeDistribution.AddRange(15, 25);
            Bundle bundle1 = new Bundle("Bundle1");
            bundle1.InsertNewLayer(0);
            bundle1.AddToLayer(0, "Feat");
            bundleStorage.Add(BundleType.Features, bundle1);
            raceStorage.Add(race);
            race.CompatableBundles.AddBundle(BundleType.Features, bundle1);
            archetype.AddRace(race);

            Bundle bundle2 = new Bundle("Bundle2");
            bundle2.InsertNewLayer(0);
            bundle2.AddToLayer(0, "Name");
            bundleStorage.Add(BundleType.Name, bundle2);
            archetype.CompatableBundles.AddBundle(BundleType.Name, bundle2);

            // Gen
            Race raceGen = archetype.GetRandomRace(raceStorage);
            Gender gender = archetype.GetRandomGender(raceGen);
            int ageBio = archetype.GetRandomAge(raceGen);

            Assert.That(raceGen, Is.EqualTo(race));
            Assert.That(gender, Is.EqualTo(Gender.Female));
            Assert.That(ageBio >= 15 && ageBio <= 20);

            // From archetype
            string nameArchiveGen = archetype.GetRandomFromBundle(bundleStorage, BundleType.Name, raceGen, gender, ageBio);
            Assert.That(nameArchiveGen, Is.EqualTo("Name"));

            // From race
            string featArchiveGen = archetype.GetRandomFromBundle(bundleStorage, BundleType.Features, raceGen, gender, ageBio);
            Assert.That(featArchiveGen, Is.EqualTo("Feat"));
        }


        [Test]
        public void ArchetypeEdgeCases()
        {
            Archetype archetype = new Archetype("Archetype");
            BundleStorage bundleStorage = new BundleStorage();
            RaceStorage raceStorage = new RaceStorage();
            ArchetypeStorage archetypeStorage = new ArchetypeStorage();
            archetypeStorage.Add(archetype);


            // No genders in intersection = should return DefaultValue, Neutral by default
            archetype.SetGender(Gender.Female);

            Race race = new Race("Race");
            race.SetGender(Gender.Male);
            raceStorage.Add(race);
            archetype.AddRace(race);

            Race raceGen = archetype.GetRandomRace(raceStorage);
            Gender gender = archetype.GetRandomGender(raceGen);
            Assert.That(gender, Is.EqualTo(Gender.Neutral));

            // No ages in intersection = should return -1
            archetype.Ages.AddRange(10, 20);

            race.AgeDistribution.AddRange(30, 40);

            raceGen = archetype.GetRandomRace(raceStorage);
            int ageBio = archetype.GetRandomAge(raceGen);
            Assert.That(ageBio, Is.EqualTo(-1));

        }
    }
}
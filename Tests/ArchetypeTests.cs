using Archives;
using NUnit.Framework;
using static Archives.Enums;

namespace Tests
{
    internal class ArchetypeTests
    {
        // Archetype, Kit and Race testing

        // Kits inherit from Dictionary<ArchiveType, WeightedArchive> and basically are sets of weighted archives
        // that can store IDs of bundles and races.
        // Kits are used as achetype and race containers of compatable objects.


        // Kit testing

        [Test]
        public void KitBasicManipulation()
        {
            Kit kit = new Kit();

            // Adding race as-is and via its ID
            // In both cases should add just the ID
            Assert.That(!kit.ContainsKey(ArchiveType.Race));
            Race race = new Race("Race");
            kit.AddRace(race);
            Assert.That(kit[ArchiveType.Race].Any(el => el.Value.Equals(race.Id)), Is.True);

            kit.Clear();
            Assert.That(!kit.ContainsKey(ArchiveType.Race));
            kit.AddRace(race.Id);
            Assert.That(kit[ArchiveType.Race].Any(el => el.Value.Equals(race.Id)), Is.True);

            // Adding bundles with the same logic
            Assert.That(!kit.ContainsKey(ArchiveType.Name));
            Bundle bundle = new Bundle("Bundle");
            kit.AddBundle(ArchiveType.Name, bundle);
            Assert.That(kit[ArchiveType.Name].Any(el => el.Value.Equals(bundle.Id)), Is.True);

            kit.Clear();
            Assert.That(!kit.ContainsKey(ArchiveType.Name));
            kit.AddBundle(ArchiveType.Name, bundle.Id);
            Assert.That(kit[ArchiveType.Name].Any(el => el.Value.Equals(bundle.Id)), Is.True);

        }

        [Test]
        public void KitRandomization()
        {
            Kit kit = new Kit();
            ArchiveStorage storage = new ArchiveStorage();

            // Race randomization

            Race r1 = new Race("R1");
            Race r2 = new Race("R2");
            storage.Add(r1);
            storage.Add(r2);

            kit.AddRace(r1);
            kit.AddRace(r2);


            for (int i = 0; i < 10; i++)
            {
                Race r = kit.GetRandomRace(storage);
                Assert.That(r.Equals(r1) || r.Equals(r2));
            }

            // Bundle randomization

            Bundle bundle1 = new("B1");
            bundle1.InsertNewLayer(0);
            bundle1.AddToLayer(0, "1");
            storage.Add(ArchiveType.Name, bundle1);

            Bundle bundle2 = new("B2");
            bundle2.InsertNewLayer(0);
            bundle2.AddToLayer(0, "2");
            storage.Add(ArchiveType.Name, bundle2);

            kit.AddBundle(ArchiveType.Name, bundle1);
            kit.AddBundle(ArchiveType.Name, bundle2);

            for (int i = 0; i < 10; i++)
            {
                string res = kit.GetRandomFromBundle(storage, ArchiveType.Name);
                Assert.That(res.Equals("1") || res.Equals("2"));
            }
        }

        [Test]
        public void KitEdgeCases()
        {
            Kit kit = new Kit();
            ArchiveStorage storage = new ArchiveStorage();

            // Randomization from a non-existent archive
            // Race
            Assert.That(kit.GetRandomRace(storage), Is.EqualTo(null));
            // Bundle
            Assert.That(kit.GetRandomFromBundle(storage, ArchiveType.Name), Is.EqualTo(null));

            kit.Clear();

            // Randomization from an empty archive
            // Race
            kit.Add(ArchiveType.Race, new WeightedArchive());
            Assert.That(kit.GetRandomRace(storage), Is.EqualTo(null));
            // Bundle
            kit.Add(ArchiveType.Name, new WeightedArchive());
            Assert.That(kit.GetRandomFromBundle(storage, ArchiveType.Name), Is.EqualTo(kit[ArchiveType.Name].DefaultValue.ToString()));
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
            ArchiveStorage storage = new ArchiveStorage();
            storage.Add(archetype);

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
            storage.Add(ArchiveType.Features, bundle1);
            storage.Add(race);
            race.CompatableBundles.AddBundle(ArchiveType.Features, bundle1);
            archetype.CompatableBundles.AddRace(race);

            Bundle bundle2 = new Bundle("Bundle2");
            bundle2.InsertNewLayer(0);
            bundle2.AddToLayer(0, "Name");
            storage.Add(ArchiveType.Name, bundle2);
            archetype.CompatableBundles.AddBundle(ArchiveType.Name,bundle2);

            // Gen
            Race raceGen = archetype.GetRandomRace(storage);
            Gender gender = archetype.GetRandomGender(raceGen);
            int ageBio = archetype.GetRandomAge(raceGen);

            Assert.That(raceGen, Is.EqualTo(race));
            Assert.That(gender, Is.EqualTo(Gender.Female));
            Assert.That(ageBio >= 15 && ageBio <= 20);

            // From archetype
            string nameArchiveGen = archetype.GetRandomFromBundle(storage, ArchiveType.Name, raceGen, gender, ageBio);
            Assert.That(nameArchiveGen, Is.EqualTo("Name"));

            // From race
            string featArchiveGen = archetype.GetRandomFromBundle(storage, ArchiveType.Features, raceGen, gender, ageBio);
            Assert.That(featArchiveGen, Is.EqualTo("Feat"));
        }


        [Test]
        public void ArchetypeEdgeCases()
        {
            Archetype archetype = new Archetype("Archetype");
            ArchiveStorage storage = new ArchiveStorage();
            storage.Add(archetype);


            // No genders in intersection = should return DefaultValue, Neutral by default
            archetype.SetGender(Gender.Female);

            Race race = new Race("Race");
            race.SetGender(Gender.Male);
            storage.Add(race);
            archetype.CompatableBundles.AddRace(race);

            Race raceGen = archetype.GetRandomRace(storage);
            Gender gender = archetype.GetRandomGender(raceGen);
            Assert.That(gender, Is.EqualTo(Gender.Neutral));

            // No ages in intersection = should return -1
            archetype.Ages.AddRange(10, 20);

            race.AgeDistribution.AddRange(30, 40);

            raceGen = archetype.GetRandomRace(storage);
            int ageBio = archetype.GetRandomAge(raceGen);
            Assert.That(ageBio, Is.EqualTo(-1));

        }
    }
}
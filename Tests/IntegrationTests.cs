using Archives;
using NUnit.Framework;
using static Archives.Enums;

namespace Tests
{
    public class IntegrationTests
    {

        ArchiveStorage archiveStorage = new ArchiveStorage();
        Archetype defaultArchetype = new Archetype("Default Archetype");
        Race human, elf;

        Bundle commonNames, elvenNames, ageNicknames;
        Bundle jobs;

        static string[] commonMaleNames = { "Arthur", "James", "Henry" };
        static string[] commonFemaleNames = { "Alice", "Olivia", "Evelyn" };
        static string[] commonGenderNeutralNames = { "Alex", "Jamie", "Sam" };
        static string[] commonLastNames = { "Adams", "Allen", "Smith" };

        static string[] elvenMaleNames = { "Arin", "Althaea", "Calathes", "Darindel", "Ondine", "Eira" };
        static string[] elvenFemaleNames = { "Eira", "Faerydae", "Kaelen", "Darindel", "Ondine", "Eira" };
        static string[] elvenNeutralNames = { "Darindel", "Ondine", "Eira" };
        static string[] elvenLastNames = { "Pharomir", "Rivenneth", "Silvethiel" };


        static string[] ageNicknames_0_5 = { "Baby", "Little one", "Munchkin" };
        static string[] ageNicknames_6_10 = { "Kiddo", "Youngster", "Junior" };
        static string[] ageNicknames_11_19 = { "Teenie", "Teenster", "Youth" };
        static string[] ageNicknames_20_25 = { "Twentysomething", "Quarter-lifer", "Early bird" };
        static string[] ageNicknames_26_35 = { "Prime timer", "Bloomer", "Seasoned soul" };
        static string[] ageNicknames_36_55 = { "Mid-lifer", "Silver streak", "Golden Journeyer" };
        static string[] ageNicknames_56_80 = { "Senior sage", "Wise one", "Oldie" };

        void racesSetup()
        {
            // Creating races and adding them to the dictionaries
            human = new Race("Human", "", 18, 80);
            human.SetGender(Gender.Female, 5);
            human.SetGender(Gender.Male, 5);
            human.SetGender(Gender.Neutral, 1);

            elf = new Race("Elf", "", 18, 750);
            elf.SetGender(Gender.Female, 5);
            elf.SetGender(Gender.Male, 5);
            elf.SetGender(Gender.Neutral, 2);

            archiveStorage.Add(human);
            archiveStorage.Add(elf);
            defaultArchetype.CompatableBundles.AddRace(human, 7);
            defaultArchetype.CompatableBundles.AddRace(elf, 3);

            defaultArchetype.SetGender(Gender.Female, 1);
            defaultArchetype.SetGender(Gender.Male, 1);
        }

        void commonNamesSetup()
        {
            // Common names bundle, contains regular names that are influenced by human culture
            commonNames = new Bundle("Common Names");
            // Layer 0 - male, female and gerder-neutral first names
            commonNames.InsertNewLayer(0, 1.0);
            commonNames.AddToLayer(0, "Arthur", 1, Gender.Male);
            commonNames.AddToLayer(0, "James", 1, Gender.Male);
            commonNames.AddToLayer(0, "Henry", 1, Gender.Male);
            commonNames.AddToLayer(0, "Alice", 1, Gender.Female);
            commonNames.AddToLayer(0, "Olivia", 1, Gender.Female);
            commonNames.AddToLayer(0, "Evelyn", 1, Gender.Female);
            commonNames.AddToLayer(0, "Alex");
            commonNames.AddToLayer(0, "Jamie");
            commonNames.AddToLayer(0, "Sam");
            // Layer 1 - male, female and gerder-neutral middle names, 30% chance of picking
            commonNames.InsertNewLayer(1, 0.3);
            commonNames.AddToLayer(1, " Arthur", 1, Gender.Male);
            commonNames.AddToLayer(1, " James", 1, Gender.Male);
            commonNames.AddToLayer(1, " Henry", 1, Gender.Male);
            commonNames.AddToLayer(1, " Alice", 1, Gender.Female);
            commonNames.AddToLayer(1, " Olivia", 1, Gender.Female);
            commonNames.AddToLayer(1, " Evelyn", 1, Gender.Female);
            commonNames.AddToLayer(1, " Alex");
            commonNames.AddToLayer(1, " Jamie");
            commonNames.AddToLayer(1, " Sam");
            // Layer 2 - gerder-neutral last names, 70% chance of picking
            commonNames.InsertNewLayer(2, 0.7);
            commonNames.AddToLayer(2, " Adams");
            commonNames.AddToLayer(2, " Allen");
            commonNames.AddToLayer(2, " Smith");

            human.CompatableBundles.AddBundle(ArchiveType.Name, commonNames);
            elf.CompatableBundles.AddBundle(ArchiveType.Name, commonNames);
            archiveStorage.Add(ArchiveType.Name, commonNames);
        }

        void elvenNamesSetup()
        {
            // Elven names bundle, contains only elven names
            elvenNames = new Bundle("Elven Names");
            // Layer 0 - male, female and gerder-neutral first names
            elvenNames.InsertNewLayer(0, 1.0);
            elvenNames.AddToLayer(0, "Arin", 1, Gender.Male);
            elvenNames.AddToLayer(0, "Althaea", 1, Gender.Male);
            elvenNames.AddToLayer(0, "Calathes", 1, Gender.Male);
            elvenNames.AddToLayer(0, "Eira", 1, Gender.Female);
            elvenNames.AddToLayer(0, "Faerydae", 1, Gender.Female);
            elvenNames.AddToLayer(0, "Kaelen", 1, Gender.Female);
            elvenNames.AddToLayer(0, "Darindel");
            elvenNames.AddToLayer(0, "Ondine");
            elvenNames.AddToLayer(0, "Eira");

            // Layer 1 - gerder-neutral last names, 70% chance of picking
            elvenNames.InsertNewLayer(1, 0.7);
            elvenNames.AddToLayer(1, " Pharomir");
            elvenNames.AddToLayer(1, " Rivenneth");
            elvenNames.AddToLayer(1, " Silvethiel");

            elf.CompatableBundles.AddBundle(ArchiveType.Name, elvenNames);
            archiveStorage.Add(ArchiveType.Name, elvenNames);
        }

        void ageNicknameSetup()
        {
            // Age nicknames bundle, contains nicknames that relate to gender
            ageNicknames = new Bundle("Age Nicknames");
            // Layer 0 - gerder-neutral nicknames for 0-5 years
            ageNicknames.InsertNewLayer(0, 1.0, "", Gender.Neutral, 0, 5);
            ageNicknames.AddToLayer(0, "Baby");
            ageNicknames.AddToLayer(0, "Little one");
            ageNicknames.AddToLayer(0, "Munchkin");
            // Layer 1 - gerder-neutral nicknames for 6-10 years
            ageNicknames.InsertNewLayer(1, 1.0, "", Gender.Neutral, 6, 10);
            ageNicknames.AddToLayer(1, "Kiddo");
            ageNicknames.AddToLayer(1, "Youngster");
            ageNicknames.AddToLayer(1, "Junior");
            // Layer 2 - gerder-neutral nicknames for 11-19 years
            ageNicknames.InsertNewLayer(2, 1.0, "", Gender.Neutral, 11, 19);
            ageNicknames.AddToLayer(2, "Teenie");
            ageNicknames.AddToLayer(2, "Teenster");
            ageNicknames.AddToLayer(2, "Youth");
            // Layer 3 - gerder-neutral nicknames for 20-25 years
            ageNicknames.InsertNewLayer(3, 1.0, "", Gender.Neutral, 20, 25);
            ageNicknames.AddToLayer(3, "Twentysomething");
            ageNicknames.AddToLayer(3, "Quarter-lifer");
            ageNicknames.AddToLayer(3, "Early bird");
            // Layer 4 - gerder-neutral nicknames for 26-35 years
            ageNicknames.InsertNewLayer(4, 1.0, "", Gender.Neutral, 26, 35);
            ageNicknames.AddToLayer(4, "Prime timer");
            ageNicknames.AddToLayer(4, "Bloomer");
            ageNicknames.AddToLayer(4, "Seasoned soul");
            // Layer 5 - gerder-neutral nicknames for 36-55 years
            ageNicknames.InsertNewLayer(5, 1.0, "", Gender.Neutral, 36, 55);
            ageNicknames.AddToLayer(5, "Mid-lifer");
            ageNicknames.AddToLayer(5, "Silver streak");
            ageNicknames.AddToLayer(5, "Golden Journeyer");
            // Layer 6 - gerder-neutral nicknames for 55-80 years
            ageNicknames.InsertNewLayer(6, 1.0, "", Gender.Neutral, 56, 80);
            ageNicknames.AddToLayer(6, "Senior sage");
            ageNicknames.AddToLayer(6, "Wise one");
            ageNicknames.AddToLayer(6, "Oldie");

            human.CompatableBundles.AddBundle(ArchiveType.Name, ageNicknames);
            elf.CompatableBundles.AddBundle(ArchiveType.Name, ageNicknames);
            archiveStorage.Add(ArchiveType.Name, ageNicknames);
        }

        void ageDistributionsSetup()
        {
            // Filling archives of ages
            defaultArchetype.Ages.AddRange(10, 15, 0);
            defaultArchetype.Ages.AddRange(15, 35, 10);
            defaultArchetype.Ages.AddRange(35, 55, 5);

            human.AgeDistribution.AddRange(15, 30, 5);
            elf.AgeDistribution.AddRange(30, 40, 10);
        }

        void jobsSetup()
        {
            // Simple jobs bundle with age restrictions (only 20-50) and no genger restrictions
            jobs = new Bundle("Jobs", false, Gender.Neutral, 20, 50);
            // Layer 0 - job titles, 80% chance of picking, if fails - default value "Unemployed"
            // will get returned
            jobs.InsertNewLayer(0, 0.8, "Unemployed");
            jobs.AddToLayer(0, "Teacher");
            jobs.AddToLayer(0, "Doctor");
            jobs.AddToLayer(0, "Policeman", 1, Gender.Male);
            jobs.AddToLayer(0, "Policewoman", 1, Gender.Female);
            jobs.AddToLayer(0, "Judge");
            jobs.AddToLayer(0, "Vendor");

            archiveStorage.Add(ArchiveType.Occupation, jobs);
            defaultArchetype.CompatableBundles.AddBundle(ArchiveType.Occupation, jobs);

        }


        [SetUp]
        public void Setup()
        {
            racesSetup();

            // Creating name bundles
            commonNamesSetup();
            elvenNamesSetup();
            ageNicknameSetup();

            // Creating a job bundle
            jobsSetup();


            // Filling age distributions
            ageDistributionsSetup();
        }


        // Generating a result from a bundle with a gender condition
        // Only gender-neutral results or results of the provided gender should be generated
        [Test]
        public void GenderedLayersBundleGen()
        {
            // Female
            for (int i = 0; i < 100; i++)
            {
                string name = commonNames.GetRandom(Gender.Female);
                string[] nameParts = name.Split(' ');

                bool allFemaleOrNeutral = true;
                foreach (string part in nameParts)
                {
                    if (!commonFemaleNames.Contains(part) && !commonGenderNeutralNames.Contains(part) && !commonLastNames.Contains(part))
                    {
                        allFemaleOrNeutral = false;
                        break;
                    }
                }

                Assert.That(allFemaleOrNeutral);
            }

            // Male
            for (int i = 0; i < 100; i++)
            {
                string name = commonNames.GetRandom(Gender.Male);
                string[] nameParts = name.Split(' ');

                bool allMaleOrNeutral = true;
                foreach (string part in nameParts)
                {
                    if (!commonMaleNames.Contains(part) && !commonGenderNeutralNames.Contains(part) && !commonLastNames.Contains(part))
                    {
                        allMaleOrNeutral = false;
                        break;
                    }
                }

                Assert.That(allMaleOrNeutral);
            }

            // Neutral
            for (int i = 0; i < 100; i++)
            {
                string name = commonNames.GetRandom();
                string[] nameParts = name.Split(' ');

                bool allNeutral = true;
                foreach (string part in nameParts)
                {
                    if (!commonGenderNeutralNames.Contains(part) && !commonLastNames.Contains(part))
                    {
                        allNeutral = false;
                        break;
                    }
                }

                Assert.That(allNeutral);
            }
        }

        // Generating a result from a bundle with layer age condition
        [Test]
        public void AgeRestrictedLayersBundleGen()
        {
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                int age = random.Next(0, 81);

                string name = ageNicknames.GetRandom(Gender.Neutral, age);

                if (age >= 0 && age <= 5)
                    Assert.That(ageNicknames_0_5.Contains(name));
                else if (age >= 6 && age < 11)
                    Assert.That(ageNicknames_6_10.Contains(name));
                else if (age >= 11 && age < 20)
                    Assert.That(ageNicknames_11_19.Contains(name));
                else if (age >= 20 && age < 26)
                    Assert.That(ageNicknames_20_25.Contains(name));
                else if (age >= 26 && age < 36)
                    Assert.That(ageNicknames_26_35.Contains(name));
                else if (age >= 36 && age < 56)
                    Assert.That(ageNicknames_36_55.Contains(name));
                else if (age >= 56 && age <= 80)
                    Assert.That(ageNicknames_56_80.Contains(name));


            }

        }


        public static bool IsCommonName(string fullName)
        {
            string[] nameParts = fullName.Split(' ');
            foreach (string part in nameParts)
                if (!(commonMaleNames.Contains(part) || commonFemaleNames.Contains(part) || commonGenderNeutralNames.Contains(part) || commonLastNames.Contains(part)))
                    return false;
            return true;
        }

        public static bool IsElvenName(string fullName)
        {
            string[] nameParts = fullName.Split(' ');
            foreach (string part in nameParts)
                if (!(elvenMaleNames.Contains(part) || elvenFemaleNames.Contains(part) || elvenNeutralNames.Contains(part) || elvenLastNames.Contains(part)))
                    return false;
            return true;
        }

        // Generating from default archetype
        // It has Occupations archive, has no Names archive
        // The ones it doesn't have will come from the generated race
        [Test]
        public void DefaultArchetypeGen()
        {
            for (int i = 0; i < 100; i++)
            {
                Race race = defaultArchetype.GetRandomRace(archiveStorage);
                Gender gender = defaultArchetype.GetRandomGender(race);
                
                int age = defaultArchetype.GetRandomAge(race);

                string name = defaultArchetype.GetRandomFromBundle(archiveStorage, ArchiveType.Name, race, gender, age);
                string job = defaultArchetype.GetRandomFromBundle(archiveStorage, ArchiveType.Occupation, race, gender, age);

                Console.WriteLine(race.Name+" "+gender+" "+age+", "+name+", "+job);

            }
        }
    }
}
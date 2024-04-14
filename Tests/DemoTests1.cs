using Archives;
using NUnit.Framework;
using static Archives.Enums;

namespace Tests
{
    public class DemoTests1
    {

        BundleStorage bundleStorage = new BundleStorage();
        Archetype defaultArchetype = new Archetype("Default Archetype");
        RaceStorage raceStorage = new RaceStorage();
        ArchetypeStorage archetypeStorage = new ArchetypeStorage();
        Race human, elf;

        Bundle commonNames, elvenNames, ageNicknames;
        Bundle jobs;

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

            raceStorage.Add(human);
            raceStorage.Add(elf);
            defaultArchetype.AddRace(human, 7);
            defaultArchetype.AddRace(elf, 3);

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

            human.CompatableBundles.AddBundle(BundleType.Name, commonNames);
            elf.CompatableBundles.AddBundle(BundleType.Name, commonNames);
            bundleStorage.Add(BundleType.Name, commonNames);
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

            elf.CompatableBundles.AddBundle(BundleType.Name, elvenNames);
            bundleStorage.Add(BundleType.Name, elvenNames);
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

            human.CompatableBundles.AddBundle(BundleType.Name, ageNicknames);
            elf.CompatableBundles.AddBundle(BundleType.Name, ageNicknames);
            bundleStorage.Add(BundleType.Name, ageNicknames);
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

            bundleStorage.Add(BundleType.Occupation, jobs);
            defaultArchetype.CompatableBundles.AddBundle(BundleType.Occupation, jobs);
            defaultArchetype.CompatableBundles[BundleType.Occupation].DefaultValue = "Can't work";
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


        // Work Demo
        [Test]
        public void DefaultArchetypeGen()
        {
            for (int i = 0; i < 100; i++)
            {
                Race race = defaultArchetype.GetRandomRace(raceStorage);
                Gender gender = defaultArchetype.GetRandomGender(race);

                int age = defaultArchetype.GetRandomAge(race);

                string name = defaultArchetype.GetRandomFromBundle(bundleStorage, BundleType.Name, race, gender, age);
                string job = defaultArchetype.GetRandomFromBundle(bundleStorage, BundleType.Occupation, race, gender, age);

                Console.WriteLine(race.Name + " " + gender + " " + age + ", " + name + ", " + job);

            }
        }


        string[] dwarvenMaleFirstNames = {
            "Адрик", "Альберих", "Баренд", "Баэрн", "Броттор", "Бруенор", "Вондал", "Вэйт",
            "Гардаин", "Даин", "Даррак", "Делг", "Килдрак", "Моргран", "Орсик", "Оскар",
            "Рангрим", "Рюрик", "Таклинн", "Торадин", "Тордек", "Торин", "Травок", "Траубон",
            "Ульфгар", "Фаргрим", "Флинт", "Харбек", "Эберк", "Эйнкиль" };
        string[] dwarvenFemaleFirstNames = {
            "Артин", "Бардрин", "Вистра", "Гуннлода", "Гурдис", "Дагнал",
            "Диеза", "Илде", "Катра", "Кристид", "Лифтраса", "Мардред",
            "Одхильд", "Рисвин", "Саннл", "Торбера", "Торгга", "Фалкрунн",
            "Финеллен", "Хельджа", "Хлин", "Эльдет", "Эмбер" };
        string[] dwarvenClanNames = {
            "Балдерк", "Боевой Молот", "Горунн", "Данкил", "Железный Кулак",
            "Крепкая Наковальня", "Ледяная Борода", "Лодерр", "Лютгер",
            "Огненная Кузня", "Рамнахейм", "Стракелн", "Торунн",
            "Унгарт", "Холдерхек" };



        [Test]
        public void DemoGen()
        {
            // Set Up

            Archetype defaultArchetype = new Archetype("Default Archetype");
            RaceStorage raceStorage = new RaceStorage();
            ArchetypeStorage archetypeStorage = new ArchetypeStorage();
            BundleStorage bundleStorage = new BundleStorage();

            Race dwarf = new("Дварф", "", 18, 450);
            dwarf.SetGender(Gender.Male);
            dwarf.SetGender(Gender.Female);
            dwarf.AgeDistribution.AddRange(0, 100);
            dwarf.AgeDistribution.AddRange(15, 50, 2);
            raceStorage.Add(dwarf);

            Bundle dwarvenNames = new("Дварфийские имена");
            // First names
            dwarvenNames.InsertNewLayer(0);
            foreach (string name in dwarvenMaleFirstNames)
                dwarvenNames.AddToLayer(0, name, 1, Gender.Male);
            foreach (string name in dwarvenFemaleFirstNames)
                dwarvenNames.AddToLayer(0, name, 1, Gender.Female);
            // Clan names
            dwarvenNames.InsertNewLayer(1, 0.8);
            foreach (string name in dwarvenClanNames)
                dwarvenNames.AddToLayer(1, " " + name, 1, Gender.Neutral);

            bundleStorage.Add(BundleType.Name, dwarvenNames);
            dwarf.CompatableBundles.AddBundle(BundleType.Name, dwarvenNames);

            Archetype dwarvenMaleAcademyStudent = new("Ученик мужской дварфийской академии");
            dwarvenMaleAcademyStudent.Ages.AddRange(10, 20);
            dwarvenMaleAcademyStudent.SetGender(Gender.Male);
            dwarvenMaleAcademyStudent.AddRace(dwarf);

            archetypeStorage.Add(dwarvenMaleAcademyStudent);


            Archetype dwarvenSchoolKid = new("Дварфийский школьник");
            dwarvenSchoolKid.Ages.AddRange(6, 16);
            dwarvenSchoolKid.SetGender(Gender.Male);
            dwarvenSchoolKid.SetGender(Gender.Female);
            dwarvenSchoolKid.AddRace(dwarf);

            archetypeStorage.Add(dwarvenSchoolKid);

            // Gen
            foreach (Archetype ar in archetypeStorage)
            {
                Console.WriteLine();
                Console.WriteLine(ar.Name);
                for (int i = 0; i < 10; i++)
                {
                    Race race = ar.GetRandomRace(raceStorage);
                    int age = ar.GetRandomAge(race);
                    Gender gender = ar.GetRandomGender(race);

                    string nameGen = ar.GetRandomFromBundle(bundleStorage, BundleType.Name, race, gender, age);

                    Console.WriteLine(race.Name + " " + age + " " + gender + " " + nameGen);
                }
            }
        }


    }
}
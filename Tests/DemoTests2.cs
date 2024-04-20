using Archives;
using NUnit.Framework;
using static Archives.Enums;

namespace Tests
{
    internal class DemoTests2
    {

        BundleStorage bundleStorage = new();
        RaceStorage raceStorage = new RaceStorage();
        ArchetypeStorage archetypeStorage = new ArchetypeStorage();


        Race human = new("Human");
        Race elf = new("Elf");
        Race dwarf = new("Dwarf");

        Archetype citizenAT = new("Citizen");
        Archetype schoolKidAT = new("School Kid");
        Archetype elderlyAT = new("Elderly");
        Archetype criminalAT = new("Criminal");
        Archetype wizardAT = new("Wizard");

        Bundle dwarvenNames, elvenNames, humanNames;

        void raccesSetUp()
        {
            raceStorage.Add(human);
            raceStorage.Add(elf);
            raceStorage.Add(dwarf);

            human.MaturityAge = 18;
            human.LifeExpectancy = 80;
            human.Genders.AddElement(Gender.Female);
            human.Genders.AddElement(Gender.Male);
            human.Ages.AddRange(0, 80, 2);
            human.Ages.AddRange(80, 100, 1);
            human.Ages.AddRange(15, 50, 3);

            elf.MaturityAge = 18;
            elf.LifeExpectancy = 750;
            elf.Genders.AddElement(Gender.Female);
            elf.Genders.AddElement(Gender.Male);
            elf.Ages.AddRange(0, 80);
            elf.Ages.AddRange(0, 50, 2);
            elf.Ages.AddRange(15, 30, 3);

            dwarf.MaturityAge = 18;
            dwarf.LifeExpectancy = 400;
            dwarf.Genders.AddElement(Gender.Female);
            dwarf.Genders.AddElement(Gender.Male);
            dwarf.Ages.AddRange(0, 80);
            dwarf.Ages.AddRange(0, 50, 2);
            dwarf.Ages.AddRange(15, 30, 3);

        }


        void dwarvenNamesSetUp()
        {
            string[] dwarfMaleNames = { "Thrain", "Gundar", "Korgrim", "Borin", "Hulgar", "Thurin", "Gimrik", "Durgan", "Orin", "Brogar" };
            string[] dwarfFemaleNames = { "Hilda", "Grenda", "Brunhild", "Elgrin", "Ulga", "Filda", "Gilda", "Ragna", "Thora", "Ingrith" };
            string[] dwarfNeutralNames = { "Karn", "Dolgrim", "Balinor", "Fargrim", "Durn", "Oskar", "Rogar", "Skorin", "Torrin", "Vargrim" };

            string[] dwarfClanNames = { "Stonebreaker", "Ironhelm", "Goldbeard", "Battleborn", "Axehammer", "Deepdelver", "Mountainforge", "Firebeard", "Steelborn", "Rockcrusher" };

            // Dwarven names are built with this logic:
            // It can have First Name and Clan Name
            // Children have "Lil" or "Little" before the name and don't have clan names
            // Clan names are preset with "of" or "from clan"
            // Clan can be generate with 80% chance
            // People with no clan get "Clanless" mark
            dwarvenNames = new("Dwarven Names", false);
            dwarf.CompatableBundles.AddBundle(BundleType.Name, dwarvenNames);
            bundleStorage.Add(BundleType.Name, dwarvenNames);

            // Child predicates
            dwarvenNames.InsertNewLayer(0, 1, "", Gender.Neutral, 0, 15);
            dwarvenNames.AddToLayer(0, "Lil ");
            dwarvenNames.AddToLayer(0, "Little ");

            // First names
            dwarvenNames.InsertNewLayer(1);
            foreach (string name in dwarfMaleNames)
                dwarvenNames.AddToLayer(1, name, 1, Gender.Male);
            foreach (string name in dwarfFemaleNames)
                dwarvenNames.AddToLayer(1, name, 1, Gender.Female);
            foreach (string name in dwarfNeutralNames)
                dwarvenNames.AddToLayer(1, name, 1, Gender.Neutral);

            // Clan names predicate 80% chance
            // If wasn't picked sets "Clanless"
            dwarvenNames.InsertNewLayer(2, 0.8, " Clanless", Gender.Neutral, 16);
            dwarvenNames.AddToLayer(2, " of ");
            dwarvenNames.AddToLayer(2, " from clan ");

            // Clan names. 100% chance of picking, depends on the previous layer
            dwarvenNames.InsertNewLayer(3, 1, "", Gender.Neutral, 16);
            foreach (string name in dwarfClanNames)
                dwarvenNames.AddToLayer(3, name, 1, Gender.Neutral);
        }

        void elvenNamesSetUp()
        {
            string[] elvenChildMaleNames = { "Aldi", "Elio", "Fael", "Lorin", "Ari", "Nen", "Riv", "Din", "Thal", "Cael" };
            string[] elvenChildFemaleNames = { "Aria", "Ella", "Fae", "Luna", "Nia", "Syl", "Ria", "Lea", "Tia", "Zara" };
            string[] elvenChildNeutralNames = { "Ari", "Eli", "Fen", "Lu", "Nim", "Ren", "Sky", "Tan", "Ves", "Zin" };

            string[] elvenMaleNames = { "Lorandil", "Caladorn", "Eldoril", "Thalendir", "Aldarion", "Elaris", "Tharion", "Maendil", "Erevan", "Silvaris" };
            string[] elvenFemaleNames = { "Lysara", "Aeliana", "Galadra", "Elenia", "Nimue", "Ariana", "Aeris", "Thalara", "Elindra", "Mirella" };
            string[] elvenNeutralNames = { "Aerwyn", "Lorien", "Rivanna", "Ardalin", "Elowyn", "Thalor", "Erendil", "Sylwyn", "Aranis", "Elaris" };

            string[] elvenLastNames = { "Starwhisper", "Moonshadow", "Silverleaf", "Frostwind", "Riverrunner", "Eagleflight", "Sunsinger", "Woodwalker", "Whitestar", "Leafwalker" };

            // Elven names are built with this logic:
            // It can have First Name and Last Name
            // Children have separate list of names
            // Last names have 80% chance
            elvenNames = new("Elven Names");
            elf.CompatableBundles.AddBundle(BundleType.Name, elvenNames);
            bundleStorage.Add(BundleType.Name, elvenNames);

            // Child names
            elvenNames.InsertNewLayer(0, 1, "", Gender.Neutral, 0, 17);
            foreach (string name in elvenChildMaleNames)
                elvenNames.AddToLayer(0, name, 1, Gender.Male);
            foreach (string name in elvenChildFemaleNames)
                elvenNames.AddToLayer(0, name, 1, Gender.Female);
            foreach (string name in elvenChildNeutralNames)
                elvenNames.AddToLayer(0, name, 1, Gender.Neutral);

            // First names for adults
            elvenNames.InsertNewLayer(1, 1, "", Gender.Neutral, 18);
            foreach (string name in elvenMaleNames)
                elvenNames.AddToLayer(1, name, 1, Gender.Male);
            foreach (string name in elvenFemaleNames)
                elvenNames.AddToLayer(1, name, 1, Gender.Female);
            foreach (string name in elvenNeutralNames)
                elvenNames.AddToLayer(1, name, 1, Gender.Neutral);

            // Last names have 80% chance
            elvenNames.InsertNewLayer(2, 0.8, "", Gender.Neutral, 18);
            foreach (string name in elvenLastNames)
                elvenNames.AddToLayer(2, " " + name, 1);
        }

        void humanNamesSetUp()
        {
            string[] humanMaleNames = { "Gareth", "Ethan", "Liam", "Finn", "Aldric", "Darius", "Cedric", "Malcolm", "Tristan", "Xavier" };
            string[] humanFemaleNames = { "Aria", "Elena", "Isabella", "Lyra", "Elara", "Seraphina", "Vivienne", "Maeve", "Astrid", "Olivia" };
            string[] humanNeutralNames = { "Rowan", "Jordan", "Alex", "Casey", "Taylor", "Reese", "Harper", "Sage", "Quinn", "Avery" };
            string[] humanLastNames = { "Blackwood", "Silverbane", "Stormwind", "Frostbeard", "Fireforge", "Ironheart", "Whitewood", "Swiftwind", "Darkstone", "Goldenshield" };

            // Human names are built with this logic:
            // It can have First Name, Middle Name and Last Name
            // Middle Names are not nessesary 
            // Young children only have First Names
            // Older people have Mr. or Mrs. before the name
            humanNames = new("Human Names");
            human.CompatableBundles.AddBundle(BundleType.Name, humanNames);
            bundleStorage.Add(BundleType.Name, humanNames);

            // Mr/Mrs before the name at 40 and up
            humanNames.InsertNewLayer(0, 1, "", Gender.Neutral, 40);
            humanNames.AddToLayer(0, "Mr. ", 1, Gender.Male);
            humanNames.AddToLayer(0, "Mrs. ", 1, Gender.Female);

            // First names for everyone
            humanNames.InsertNewLayer(1);
            foreach (string name in humanMaleNames)
                humanNames.AddToLayer(1, name, 1, Gender.Male);
            foreach (string name in humanFemaleNames)
                humanNames.AddToLayer(1, name, 1, Gender.Female);
            foreach (string name in humanNeutralNames)
                humanNames.AddToLayer(1, name, 1, Gender.Neutral);

            // Middle names are the same as first names but with 50% chance at 18 and up
            humanNames.InsertNewLayer(2, 0.5, "", Gender.Neutral, 18);
            foreach (string name in humanMaleNames)
                humanNames.AddToLayer(2, " " + name, 1, Gender.Male);
            foreach (string name in humanFemaleNames)
                humanNames.AddToLayer(2, " " + name, 1, Gender.Female);
            foreach (string name in humanNeutralNames)
                humanNames.AddToLayer(2, " " + name, 1, Gender.Neutral);

            // Last names at 15 and up
            humanNames.InsertNewLayer(3, 1, "", Gender.Neutral, 15);
            foreach (string name in humanLastNames)
                humanNames.AddToLayer(3, " " + name, 1);
        }




        void citizenArchetypeSetup()
        {

        }


        [SetUp]
        public void SetUp()
        {
            raccesSetUp();
            dwarvenNamesSetUp();
            elvenNamesSetUp();
            humanNamesSetUp();
        }

        [Test]
        public void Gen()
        {
            Console.WriteLine("Humans");
            Console.WriteLine("// Human names are built with this logic:\r\n// It can have First Name, Middle Name and Last Name\r\n// Middle Names are not nessesary \r\n// Young children only have First Names\r\n// Older people have Mr. or Mrs. before the name\r\n");
            for (int i = 0; i < 10; i++)
            {
                Gender gender = (Gender)human.Genders.GetRandomUnrestricted();
                int age = human.Ages.GetRandom();
                Console.WriteLine(gender + " " + age + " " + humanNames.GetRandom(gender, age));
            }
            Console.WriteLine();
            Console.WriteLine("Elves");
            Console.WriteLine("// Elven names are built with this logic:\r\n// It can have First Name and Last Name\r\n// Children have separate list of names\r\n// Last names have 80% chance");
            for (int i = 0; i < 10; i++)
            {
                Gender gender = (Gender)elf.Genders.GetRandomUnrestricted();
                int age = elf.Ages.GetRandom();
                Console.WriteLine(gender + " " + age + " " + elvenNames.GetRandom(gender, age));
            }
            Console.WriteLine();
            Console.WriteLine("Dwarves");
            Console.WriteLine("// Dwarven names are built with this logic:\r\n// It can have First Name and Clan Name\r\n// Children have \"Lil\" or \"Little\" before the name and don't have clan names\r\n// Clan names are preset with \"of\" or \"from clan\"\r\n// Clan can be generate with 80% chance\r\n// People with no clan get \"Clanless\" mark");
            for (int i = 0; i < 10; i++)
            {
                Gender gender = (Gender)dwarf.Genders.GetRandomUnrestricted();
                int age = dwarf.Ages.GetRandom();
                Console.WriteLine(gender + " " + age + " " + dwarvenNames.GetRandom(gender, age));
            }
        }


    }
}

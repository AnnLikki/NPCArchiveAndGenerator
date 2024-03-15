using Archives;

namespace Tests
{
   public class FullNPCGen
    {
        Bundle englishNames, russianNames;
        Bundle genders;
        AgeRange child, teen, young, adult, old;
        Bundle criminalOcc, regularOcc;
        Bundle goodPers, badPers, combinedPers;
        Bundle humanHeights;
        Bundle humanPhys;
        Bundle lightSkins, darkSkins;
        Bundle masculineHair, feminineHair;
        Bundle elegantFaces, roughFaces;
        Bundle naturalEyes, colorfulEyes;
        Bundle regularClothes, nobleClothes;
        Bundle tattoos, scars;

        ArchiveRace races;

        ArchiveAgeRange ageRanges;

        ArchiveBundles namesArchive;
        ArchiveBundles gendersArchive;
        ArchiveBundles occupationsArchive;
        ArchiveBundles personalitiesArchive;
        ArchiveBundles heightsArchive;
        ArchiveBundles physiquesArchive;
        ArchiveBundles skinsArchive;
        ArchiveBundles hairArchive;
        ArchiveBundles facesArchive;
        ArchiveBundles eyesArchive;
        ArchiveBundles clothesArchive;
        ArchiveBundles featuresArchive;

        Dictionary<ArchiveType, ArchiveBundles> archives;

        [SetUp]
        public void Setup()
        {

            races = new ArchiveRace();
            ageRanges = new ArchiveAgeRange();

            namesArchive= new ArchiveBundles();
            gendersArchive= new ArchiveBundles();
            occupationsArchive= new ArchiveBundles();
            personalitiesArchive= new ArchiveBundles();
            heightsArchive= new ArchiveBundles();
            physiquesArchive= new ArchiveBundles();
            skinsArchive= new ArchiveBundles();
            hairArchive = new ArchiveBundles();
            facesArchive = new ArchiveBundles();
            eyesArchive = new ArchiveBundles();
            clothesArchive= new ArchiveBundles();
            featuresArchive= new ArchiveBundles();

            archives = new Dictionary<ArchiveType, ArchiveBundles>();
           
            archives.Add(ArchiveType.Name, namesArchive);
            archives.Add(ArchiveType.Gender, gendersArchive);
            archives.Add(ArchiveType.Occupation, occupationsArchive);
            archives.Add(ArchiveType.Personality, personalitiesArchive);
            archives.Add(ArchiveType.Height, heightsArchive);
            archives.Add(ArchiveType.Physique, physiquesArchive);
            archives.Add(ArchiveType.Skin, skinsArchive);
            archives.Add(ArchiveType.Hair, hairArchive);
            archives.Add(ArchiveType.Face, facesArchive);
            archives.Add(ArchiveType.Eyes, eyesArchive);
            archives.Add(ArchiveType.Clothes, clothesArchive);
            archives.Add(ArchiveType.Features, featuresArchive);


            // first, middle and last names with chance and no dependency
            russianNames = new Bundle(ArchiveType.Name, "Russian Names", true);

            // Layer 0 - first names, 100% chance
            russianNames.insertLayer(0);

            russianNames.addElementToLayer(0, new ListElement("Oleg"));
            russianNames.addElementToLayer(0, new ListElement("Artem"));
            russianNames.addElementToLayer(0, new ListElement("Anton"));
            russianNames.addElementToLayer(0, new ListElement("Katya"));
            russianNames.addElementToLayer(0, new ListElement("Kira"));

            // Layer 1 - midlle names, 30% chance
            russianNames.insertLayer(1, 0.3f);

            russianNames.addElementToLayer(1, new ListElement("Misha"));
            russianNames.addElementToLayer(1, new ListElement("Kristina"));
            russianNames.addElementToLayer(1, new ListElement("Alena"));
            russianNames.addElementToLayer(1, new ListElement("Lera"));
            russianNames.addElementToLayer(1, new ListElement("Kolya"));

            // Layer 2 - last names, 70% chance
            russianNames.insertLayer(2, 0.7f);

            russianNames.addElementToLayer(2, new ListElement("Sidorov"));
            russianNames.addElementToLayer(2, new ListElement("Matveev"));
            russianNames.addElementToLayer(2, new ListElement("Ivanov"));
            russianNames.addElementToLayer(2, new ListElement("Petrov"));
            russianNames.addElementToLayer(2, new ListElement("Sokolov"));

            namesArchive.Add(russianNames);

            // -------------------------------- ChatGPT generated data. Comments are wrong. --------------------------------

            englishNames = new Bundle(ArchiveType.Name, "English Names", true);

            // Layer 0 - first names, 100% chance
            englishNames.insertLayer(0);

            englishNames.addElementToLayer(0, new ListElement("John"));
            englishNames.addElementToLayer(0, new ListElement("Emily"));
            englishNames.addElementToLayer(0, new ListElement("Michael"));
            englishNames.addElementToLayer(0, new ListElement("Sophia"));
            englishNames.addElementToLayer(0, new ListElement("William"));

            // Layer 1 - middle names, 30% chance
            englishNames.insertLayer(1, 0.3f);

            englishNames.addElementToLayer(1, new ListElement("Alexander", 10)); // 10% frequency
            englishNames.addElementToLayer(1, new ListElement("Grace", 5));      // 5% frequency
            englishNames.addElementToLayer(1, new ListElement("Christopher", 15)); // 15% frequency

            // Layer 2 - last names, 70% chance
            englishNames.insertLayer(2, 0.7f);

            englishNames.addElementToLayer(2, new ListElement("Smith", 20));   // 20% frequency
            englishNames.addElementToLayer(2, new ListElement("Johnson", 15)); // 15% frequency
            englishNames.addElementToLayer(2, new ListElement("Brown", 25));   // 25% frequency

            namesArchive.Add(englishNames);


            genders = new Bundle(ArchiveType.Gender, "Genders", true);

            genders.insertLayer(0);

            genders.addElementToLayer(0, new ListElement("Male", 50));
            genders.addElementToLayer(0, new ListElement("Female", 50));
            genders.addElementToLayer(0, new ListElement("Non-Binary", 5));

            gendersArchive.Add(genders);


            criminalOcc = new Bundle(ArchiveType.Occupation, "Criminal Occupations", true);

            criminalOcc.insertLayer(0);

            criminalOcc.addElementToLayer(0, new ListElement("Thief"));
            criminalOcc.addElementToLayer(0, new ListElement("Assassin"));
            criminalOcc.addElementToLayer(0, new ListElement("Hacker"));

            occupationsArchive.Add(criminalOcc);

            regularOcc = new Bundle(ArchiveType.Occupation, "Regular Occupations", true);

            regularOcc.insertLayer(0);

            regularOcc.addElementToLayer(0, new ListElement("Doctor"));
            regularOcc.addElementToLayer(0, new ListElement("Teacher"));
            regularOcc.addElementToLayer(0, new ListElement("Engineer"));
            regularOcc.addElementToLayer(0, new ListElement("Artist"));

            occupationsArchive.Add(regularOcc);


            // Good Personality Traits
            goodPers = new Bundle(ArchiveType.Personality, "Good Personality Traits", true);

            // Layer 0 - primary good traits, 100% chance
            goodPers.insertLayer(0);

            goodPers.addElementToLayer(0, new ListElement("Kind", 20));          // 20% frequency
            goodPers.addElementToLayer(0, new ListElement("Generous", 15));      // 15% frequency
            goodPers.addElementToLayer(0, new ListElement("Optimistic", 25));    // 25% frequency
            goodPers.addElementToLayer(0, new ListElement("Empathetic", 10));    // 10% frequency

            // Layer 1 - secondary good traits, 50% chance
            goodPers.insertLayer(1, 0.5f);

            goodPers.addElementToLayer(1, new ListElement("Compassionate", 10)); // 10% frequency
            goodPers.addElementToLayer(1, new ListElement("Honest", 15));        // 15% frequency

            personalitiesArchive.Add(goodPers);

            // Bad Personality Traits
            badPers = new Bundle(ArchiveType.Personality, "Bad Personality Traits", true);

            // Layer 0 - primary bad traits, 100% chance
            badPers.insertLayer(0);

            badPers.addElementToLayer(0, new ListElement("Cruel", 15));           // 15% frequency
            badPers.addElementToLayer(0, new ListElement("Manipulative", 20));    // 20% frequency
            badPers.addElementToLayer(0, new ListElement("Pessimistic", 25));     // 25% frequency

            // Layer 1 - secondary bad traits, 50% chance
            badPers.insertLayer(1, 0.5f);

            badPers.addElementToLayer(1, new ListElement("Spiteful", 10));        // 10% frequency
            badPers.addElementToLayer(1, new ListElement("Arrogant", 12));        // 12% frequency

            personalitiesArchive.Add(badPers);


            // Combined Personalities
            combinedPers = new Bundle(ArchiveType.Personality, "Combined Personalities", true);

            // Layer 0 - primary good traits, 100% chance
            combinedPers.insertLayer(0);

            combinedPers.addElementToLayer(0, new ListElement("Kind", 20));          // 20% frequency
            combinedPers.addElementToLayer(0, new ListElement("Generous", 15));      // 15% frequency
            combinedPers.addElementToLayer(0, new ListElement("Optimistic", 25));    // 25% frequency
            combinedPers.addElementToLayer(0, new ListElement("Empathetic", 10));    // 10% frequency

            // Layer 1 - secondary bad traits, 70% chance
            combinedPers.insertLayer(1, 0.7f);

            combinedPers.addElementToLayer(1, new ListElement("Manipulative", 20));    // 20% frequency
            combinedPers.addElementToLayer(1, new ListElement("Pessimistic", 25));     // 25% frequency
            combinedPers.addElementToLayer(1, new ListElement("Spiteful", 10));        // 10% frequency

            // Layer 2 - additional quirky traits, 50% chance
            combinedPers.insertLayer(2, 0.5f);

            combinedPers.addElementToLayer(2, new ListElement("Quirky", 15));          // 15% frequency
            combinedPers.addElementToLayer(2, new ListElement("Eccentric", 10));       // 10% frequency

            personalitiesArchive.Add(combinedPers);


            humanHeights = new Bundle(ArchiveType.Height, "Human Heights", true);

            // Layer 0 - typical heights, 100% chance
            humanHeights.insertLayer(0);

            humanHeights.addElementToLayer(0, new ListElement("Short", 10));      // 10% frequency
            humanHeights.addElementToLayer(0, new ListElement("Average", 70));    // 70% frequency
            humanHeights.addElementToLayer(0, new ListElement("Tall", 20));       // 20% frequency

            heightsArchive.Add(humanHeights);


            humanPhys = new Bundle(ArchiveType.Physique, "Human Physique", true);

            // Layer 0 - physique types, 100% chance
            humanPhys.insertLayer(0);

            humanPhys.addElementToLayer(0, new ListElement("Lean", 25));        // 25% frequency
            humanPhys.addElementToLayer(0, new ListElement("Average", 50));     // 50% frequency
            humanPhys.addElementToLayer(0, new ListElement("Athletic", 15));    // 15% frequency
            humanPhys.addElementToLayer(0, new ListElement("Stocky", 10));      // 10% frequency

            physiquesArchive.Add(humanPhys);


            // Light Skins
            lightSkins = new Bundle(ArchiveType.Skin, "Light Skins", true);

            // Layer 0 - primary skin tones, 100% chance
            lightSkins.insertLayer(0);

            lightSkins.addElementToLayer(0, new ListElement("Fair", 40));         // 40% frequency
            lightSkins.addElementToLayer(0, new ListElement("Pale", 30));         // 30% frequency
            lightSkins.addElementToLayer(0, new ListElement("Light Olive", 20));  // 20% frequency
            lightSkins.addElementToLayer(0, new ListElement("Rosy", 10));         // 10% frequency

            // Layer 1 - additional features, 50% chance
            lightSkins.insertLayer(1, 0.5f);

            lightSkins.addElementToLayer(1, new ListElement("Freckles", 20));    // 20% frequency
            lightSkins.addElementToLayer(1, new ListElement("Sunburns", 15));    // 15% frequency
            lightSkins.addElementToLayer(1, new ListElement("Vitiligo", 5));     // 5% frequency

            skinsArchive.Add(lightSkins);

            // Dark Skins
            darkSkins = new Bundle(ArchiveType.Skin, "Dark Skins", true);

            // Layer 0 - primary skin tones, 100% chance
            darkSkins.insertLayer(0);

            darkSkins.addElementToLayer(0, new ListElement("Medium Brown", 40));     // 40% frequency
            darkSkins.addElementToLayer(0, new ListElement("Dark Brown", 30));       // 30% frequency
            darkSkins.addElementToLayer(0, new ListElement("Deep Tan", 20));         // 20% frequency
            darkSkins.addElementToLayer(0, new ListElement("Ebony", 10));            // 10% frequency

            // Layer 1 - additional features, 50% chance
            darkSkins.insertLayer(1, 0.5f);

            darkSkins.addElementToLayer(1, new ListElement("Freckles", 15));    // 15% frequency
            darkSkins.addElementToLayer(1, new ListElement("Sunburns", 10));    // 10% frequency
            darkSkins.addElementToLayer(1, new ListElement("Vitiligo", 8));     // 8% frequency

            skinsArchive.Add(darkSkins);


            masculineHair = new Bundle(ArchiveType.Hair, "Masculine Hair", true);

            // Layer 0 - hair types, 100% chance
            masculineHair.insertLayer(0);

            masculineHair.addElementToLayer(0, new ListElement("Short", 40));       // 40% frequency
            masculineHair.addElementToLayer(0, new ListElement("Buzz Cut", 20));    // 20% frequency
            masculineHair.addElementToLayer(0, new ListElement("Crew Cut", 20));    // 20% frequency
            masculineHair.addElementToLayer(0, new ListElement("Medium Length", 15)); // 15% frequency
            masculineHair.addElementToLayer(0, new ListElement("Bald", 5));         // 5% frequency

            // Layer 1 - additional masculine hair features, 50% chance
            masculineHair.insertLayer(1, 0.5f);

            masculineHair.addElementToLayer(1, new ListElement("Beard", 30));       // 30% frequency
            masculineHair.addElementToLayer(1, new ListElement("Mustache", 20));    // 20% frequency
            masculineHair.addElementToLayer(1, new ListElement("Stubble", 10));     // 10% frequency

            hairArchive.Add(masculineHair);


            feminineHair = new Bundle(ArchiveType.Hair, "Feminine Hair", true);

            // Layer 0 - hair types, 100% chance
            feminineHair.insertLayer(0);

            feminineHair.addElementToLayer(0, new ListElement("Long", 50));          // 50% frequency
            feminineHair.addElementToLayer(0, new ListElement("Medium Length", 30)); // 30% frequency
            feminineHair.addElementToLayer(0, new ListElement("Short", 15));         // 15% frequency

            // Layer 1 - additional feminine hair features, 50% chance
            feminineHair.insertLayer(1, 0.5f);

            feminineHair.addElementToLayer(1, new ListElement("Bangs", 20));         // 20% frequency
            feminineHair.addElementToLayer(1, new ListElement("Braids", 15));        // 15% frequency
            feminineHair.addElementToLayer(1, new ListElement("Ponytail", 25));      // 25% frequency

            hairArchive.Add(feminineHair);


            elegantFaces = new Bundle(ArchiveType.Face, "Elegant Faces", true);

            // Layer 0 - face types, 100% chance
            elegantFaces.insertLayer(0);

            elegantFaces.addElementToLayer(0, new ListElement("Oval", 30));             // 30% frequency
            elegantFaces.addElementToLayer(0, new ListElement("Heart-shaped", 20));     // 20% frequency
            elegantFaces.addElementToLayer(0, new ListElement("Diamond", 15));          // 15% frequency
            elegantFaces.addElementToLayer(0, new ListElement("Square", 15));           // 15% frequency
            elegantFaces.addElementToLayer(0, new ListElement("Triangular", 10));       // 10% frequency
            elegantFaces.addElementToLayer(0, new ListElement("Rectangular", 10));      // 10% frequency

            facesArchive.Add(elegantFaces);

            roughFaces = new Bundle(ArchiveType.Face, "Rough Faces", true);

            // Layer 0 - face types, 100% chance
            roughFaces.insertLayer(0);

            roughFaces.addElementToLayer(0, new ListElement("Square", 25));             // 25% frequency
            roughFaces.addElementToLayer(0, new ListElement("Round", 20));              // 20% frequency
            roughFaces.addElementToLayer(0, new ListElement("Angular", 15));            // 15% frequency
            roughFaces.addElementToLayer(0, new ListElement("Chiseled", 15));           // 15% frequency
            roughFaces.addElementToLayer(0, new ListElement("Weathered", 15));          // 15% frequency
            roughFaces.addElementToLayer(0, new ListElement("Scarred", 10));            // 10% frequency

            facesArchive.Add(roughFaces);

            naturalEyes = new Bundle(ArchiveType.Eyes, "Natural Eyes", true);

            // Layer 0 - eye colors, 100% chance
            naturalEyes.insertLayer(0);

            naturalEyes.addElementToLayer(0, new ListElement("Brown", 40));      // 40% frequency
            naturalEyes.addElementToLayer(0, new ListElement("Blue", 25));       // 25% frequency
            naturalEyes.addElementToLayer(0, new ListElement("Green", 20));      // 20% frequency
            naturalEyes.addElementToLayer(0, new ListElement("Hazel", 15));      // 15% frequency

            eyesArchive.Add(naturalEyes);

            colorfulEyes = new Bundle(ArchiveType.Eyes, "Colorful Eyes", true);

            // Layer 0 - eye colors, 100% chance
            colorfulEyes.insertLayer(0);

            colorfulEyes.addElementToLayer(0, new ListElement("Amber", 30));         // 30% frequency
            colorfulEyes.addElementToLayer(0, new ListElement("Gray", 20));          // 20% frequency
            colorfulEyes.addElementToLayer(0, new ListElement("Violet", 15));        // 15% frequency
            colorfulEyes.addElementToLayer(0, new ListElement("Red", 10));           // 10% frequency
            colorfulEyes.addElementToLayer(0, new ListElement("Heterochromia", 25)); // 25% frequency

            regularClothes = new Bundle(ArchiveType.Clothes, "Regular Clothes", true);

            // Layer 0 - clothing styles, 100% chance
            regularClothes.insertLayer(0);

            regularClothes.addElementToLayer(0, new ListElement("Casual", 40));          // 40% frequency
            regularClothes.addElementToLayer(0, new ListElement("Jeans and T-shirt", 25)); // 25% frequency
            regularClothes.addElementToLayer(0, new ListElement("Business Casual", 20));  // 20% frequency
            regularClothes.addElementToLayer(0, new ListElement("Sportswear", 15));       // 15% frequency

            clothesArchive.Add(regularClothes);


            nobleClothes = new Bundle(ArchiveType.Clothes, "Noble Clothes", true);

            // Layer 0 - clothing styles, 100% chance
            nobleClothes.insertLayer(0);

            nobleClothes.addElementToLayer(0, new ListElement("Formal Suit", 30));        // 30% frequency
            nobleClothes.addElementToLayer(0, new ListElement("Evening Gown", 25));       // 25% frequency
            nobleClothes.addElementToLayer(0, new ListElement("Tuxedo", 20));             // 20% frequency
            nobleClothes.addElementToLayer(0, new ListElement("Ballroom Dress", 25));     //


            tattoos = new Bundle(ArchiveType.Features, "Tattoos", true);

            // Layer 0 - tattoo styles, 100% chance
            tattoos.insertLayer(0);

            tattoos.addElementToLayer(0, new ListElement("Traditional tattoos", 30));     // 30% frequency
            tattoos.addElementToLayer(0, new ListElement("Realistic tattoos", 25));       // 25% frequency
            tattoos.addElementToLayer(0, new ListElement("Neo-traditional tattoos", 20)); // 20% frequency
            tattoos.addElementToLayer(0, new ListElement("Watercolor tattoos", 15));      // 15% frequency
            tattoos.addElementToLayer(0, new ListElement("Geometric tattoos", 10));       // 10% frequency

            featuresArchive.Add(tattoos);


            scars = new Bundle(ArchiveType.Features, "Scars", true);

            // Layer 0 - scar types, 100% chance
            scars.insertLayer(0);

            scars.addElementToLayer(0, new ListElement("Surgical scar", 30));       // 30% frequency
            scars.addElementToLayer(0, new ListElement("Burn scar", 25));           // 25% frequency
            scars.addElementToLayer(0, new ListElement("Accidental scar", 20));     // 20% frequency
            scars.addElementToLayer(0, new ListElement("Combat scar", 15));         // 15% frequency
            scars.addElementToLayer(0, new ListElement("Animal Bite scar", 10));    // 10% frequency

            featuresArchive.Add(scars);


            // Child
            child = new AgeRange("Child", 5, 14);
            ageRanges.Add(child);

            // Teen
            teen = new AgeRange("Teen", 15, 19);
            ageRanges.Add(teen);

            // Young
            young = new AgeRange("Young", 20, 34);
            ageRanges.Add(young);

            // Adult
            adult = new AgeRange("Adult", 35, 55);
            ageRanges.Add(adult);

            // Old
            old = new AgeRange("Old", 56, 80);
            ageRanges.Add(old);

        }

        // Tests full generation from any sources
        [Test]
        public void DependentCompoundStatistics()
        { 
            int npcs = 5;
            for (int i = 0; i < npcs; i++)
            {
                Console.WriteLine();
                Console.WriteLine("NPC #"+i);
                Console.WriteLine("Name: " + archives[ArchiveType.Name].getRandomFromAny());
                Console.WriteLine("Gender: " + archives[ArchiveType.Gender].getRandomFromAny());
                Console.WriteLine("Occupation: " + archives[ArchiveType.Occupation].getRandomFromAny());
                Console.WriteLine("Personality: " + archives[ArchiveType.Personality].getRandomFromAny());
                Console.WriteLine("Height: " + archives[ArchiveType.Height].getRandomFromAny());
                Console.WriteLine("Physique: " + archives[ArchiveType.Physique].getRandomFromAny());
                Console.WriteLine("Skin Color: " + archives[ArchiveType.Skin].getRandomFromAny());
                Console.WriteLine("Hair Style: " + archives[ArchiveType.Hair].getRandomFromAny());
                Console.WriteLine("Face: " + archives[ArchiveType.Face].getRandomFromAny());
                Console.WriteLine("Eye Color: " + archives[ArchiveType.Eyes].getRandomFromAny());
                Console.WriteLine("Clothing Style: " + archives[ArchiveType.Clothes].getRandomFromAny());
                Console.WriteLine("Feature (Tattoo or Scar): " + archives[ArchiveType.Features].getRandomFromAny());
            }

        }

        
    }
}
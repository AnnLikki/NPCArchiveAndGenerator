
using Archives;

ArchiveStorage archiveStorage;
Kit defaultArchives;
AgeDistribution defaultAgeDistribution;
Race human, elf;

Bundle commonNames, elvenNames;


// Initializing main archives
archiveStorage = new ArchiveStorage();
defaultArchives = new Kit();
defaultAgeDistribution = new AgeDistribution();

// Creating races and adding them to the dictionaries
human = new Race("Human", "", 18, 80);
elf = new Race("Elf", "", 18, 750);
archiveStorage.Add(human);
archiveStorage.Add(elf);
defaultArchives.Add(human, 7);
defaultArchives.Add(elf, 3);

// Creating and filling name archive

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

human.CompatableBundles.Add(ArchiveType.Name, commonNames);
archiveStorage.Add(ArchiveType.Name, commonNames);
defaultArchives.Add(ArchiveType.Name, commonNames);

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

elf.CompatableBundles.Add(ArchiveType.Name, elvenNames);
archiveStorage.Add(ArchiveType.Name, elvenNames);

// Filling archives of ages
defaultAgeDistribution.AddRange(0, 15, 0);
defaultAgeDistribution.AddRange(15, 35, 10);
defaultAgeDistribution.AddRange(35, 55, 5);
defaultAgeDistribution.AddRange(55, 100, 0);

human.AgeDistribution.AddRange(15, 30, 5);
elf.AgeDistribution.AddRange(30, 40, 10);


using Archives;
using System;
using System.IO;
using System.Text.Json;
using Path = System.IO.Path;

namespace FileManager
{
    // SnL means Save and Load.
    // This class handles working with files and directories.
    // In the future as I add more data (such as little archives and settings)
    // this class will gain more methods to save it.
    // This class utilizes catching errors and sending them to
    // the error handler to be shown when needes, not every time
    // the error occures.
    public static class SnL
    {
        public static string NPCsSavePath { get; } = "dataNPCMaker\\NPCsData.json";
        public static string racesSavePath { get; } = "dataNPCMaker\\RacesData.json";


        // These methods save and load the archives in their entirety using JSON files
        // while collecting any occuring errors.
        // The errors will definetely occur if there's no files in the default
        // directory to load, but it's not that bad as the program will be clear
        // for creating new archives.
        // The files are saved with a format decriptor that marks the type of
        // archive saved. An archive file can not be loaded in a different type of archive.
        public static void saveNPCData(string path)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                if (!File.Exists(path))
                    using (File.Create(path)) { }
                string jsonData = "NPC ARCHIVE\n" +
                    JsonSerializer.Serialize(ArchiveHandler.archiveNPC);
                File.WriteAllText(path, jsonData);

            }
            catch (Exception e)
            {
                ErrorHandler.collectError("Could not save file " + path);
                Console.WriteLine(e);
            }
        }

        public static void loadNPCData(string path)
        {
            try
            {
                if (File.ReadAllLines(path)[0] == "NPC ARCHIVE")
                {
                    string jsonData = File.ReadAllText(path).Replace("NPC ARCHIVE", "");
                    ArchiveHandler.archiveNPC = JsonSerializer.Deserialize<ArchiveNPC>(jsonData);
                }
                else
                    throw new FormatException("No correct format descriptor found.");
            }
            catch (FormatException e)
            {
                ErrorHandler.collectError("Wrong or missing format descriptor " + path);
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                ErrorHandler.collectError("Could not load file " + path);
                Console.WriteLine(e);
            }
        }
        public static void saveRaceData(string path)
        {
            try
            {

                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                if (!File.Exists(path))
                    using (File.Create(path)) { }
                string jsonData = "RACE ARCHIVE\n" +
                    JsonSerializer.Serialize(ArchiveHandler.archiveRace);
                File.WriteAllText(path, jsonData);
            }
            catch (Exception e)
            {
                ErrorHandler.collectError("Could not save file " + path);
                Console.WriteLine(e);
            }
        }

        public static void loadRaceData(string path)
        {
            try
            {
                if (File.ReadAllLines(path)[0] == "RACE ARCHIVE")
                {
                    string jsonData = File.ReadAllText(path).Replace("RACE ARCHIVE", "");
                    ArchiveHandler.archiveRace = JsonSerializer.Deserialize<ArchiveRace>(jsonData);
                }
                else
                    throw new FormatException("No correct format descriptor found.");
            }
            catch (FormatException e)
            {
                ErrorHandler.collectError("Wrong or missing format descriptor " + path);
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                ErrorHandler.collectError("Could not load file " + path);
                Console.WriteLine(e);
            }
        }
    }
}

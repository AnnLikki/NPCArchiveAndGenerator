using Archives;
using Microsoft.Win32;
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
    // the error handler to be shown when needed, not every time
    // the error occures.
    public static class SnL
    {
        public static string NPCsSavePath { set; get; } = null;
        public static string racesSavePath { set; get; } = null;

        public const string TYPE_NPC = "TYPE_NPC";
        public const string TYPE_RACE = "TYPE_RACE";

        // These methods save and load the archives in their entirety using JSON files
        // while collecting any occuring errors.
        // The files are saved with a format decriptor that marks the type of
        // archive saved. An archive file can not be loaded in a different type of archive.

        // Saving archive depending on the provided type in a provided directory. 
        public static bool saveArchive(string type, string path)
        {
            if (type != TYPE_NPC && type != TYPE_RACE)
                throw new ArgumentException(type + " is not one of valid archive types.");
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                if (!File.Exists(path))
                    using (File.Create(path)) { }
                string jsonData = "";
                if (type == TYPE_NPC)
                {
                    jsonData = "NPC ARCHIVE\n" +
                        JsonSerializer.Serialize(ArchiveHandler.absoluteArchiveNPC);
                }
                else if (type == TYPE_RACE)
                {
                    jsonData = "RACE ARCHIVE\n" +
                        JsonSerializer.Serialize(ArchiveHandler.absoluteArchiveRace);
                }
                File.WriteAllText(path, jsonData);
                return true;
            }
            catch (Exception e)
            {
                ErrorHandler.collectError("Could not save file " + path);
                Console.WriteLine(e);
                return false;
            }
        }

        // Loading an archive depending on the provided type from a provided file.
        public static bool loadArchive(string type, string path)
        {
            if (type != TYPE_NPC && type != TYPE_RACE)
                throw new ArgumentException(type + " is not one of valid archive types.");
            try
            {
                if (type == TYPE_NPC)
                    if (File.ReadAllLines(path)[0] == "NPC ARCHIVE")
                    {
                        string jsonData = File.ReadAllText(path).Replace("NPC ARCHIVE", "");
                        ArchiveHandler.absoluteArchiveNPC = JsonSerializer.Deserialize<ArchiveNPC>(jsonData);
                    }
                    else
                        throw new FormatException("No correct format descriptor found.");
                else if (type == TYPE_RACE)

                    if (File.ReadAllLines(path)[0] == "RACE ARCHIVE")
                    {
                        string jsonData = File.ReadAllText(path).Replace("RACE ARCHIVE", "");
                        ArchiveHandler.absoluteArchiveRace = JsonSerializer.Deserialize<ArchiveRace>(jsonData);
                    }
                    else
                        throw new FormatException("No correct format descriptor found.");

                return true;
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
            return false;
        }

        // Choosing where to save by a dialog window.
        public static bool saveViaDialog(string type, string windowTitle)
        {
            if (type != TYPE_NPC && type != TYPE_RACE)
                throw new ArgumentException(type + " is not one of valid archive types.");

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files(*.json)|*.json";
            saveFileDialog.Title = windowTitle;
            if (saveFileDialog.ShowDialog() == false)
                return false;

            string filename = saveFileDialog.FileName;
            saveArchive(type, filename);

            if (!ErrorHandler.errorPopup())
                if (type == TYPE_NPC)
                    NPCsSavePath = filename;
                else if (type == TYPE_RACE)
                    racesSavePath = filename;
            return true;

        }

        // Choosing where to load from by a dialog window.
        public static bool openViaDialog(string type, string windowTitle)
        {
            if (type != TYPE_NPC && type != TYPE_RACE)
                throw new ArgumentException(type + " is not one of valid archive types.");

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.Title = windowTitle;
            if (openFileDialog.ShowDialog() == false)
                return false;

            string filename = openFileDialog.FileName;

            loadArchive(type, filename);

            if (!ErrorHandler.errorPopup())
                if (type == TYPE_NPC)
                    NPCsSavePath = filename;
                else if (type == TYPE_RACE)
                    racesSavePath = filename;
            return true;

        }

    }
}

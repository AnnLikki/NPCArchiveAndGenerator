using Archives;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using static Archives.Enums;
using Path = System.IO.Path;

namespace FileManager
{

    /// <summary>
    /// Handles working with files and directories.
    /// </summary>
    ///<remarks>
    /// <para>SnL means Save and Load.</para>
    /// <para>It catches errors and sends them to
    /// the error handler to be shown when needed, not every time
    /// the error occures.</para>
    ///</remarks>
    public static class SnL
    {
        public static string NPCsSavePath { set; get; } = null;
        public static string RacesSavePath { set; get; } = null;
        public static string BundlesSavePath { set; get; } = null;
        public static string ArchetypesSavePath { set; get; } = null;
        public static string dataSavePath { set; get; } = "NPCA&G_Data.json";

        public enum SaveType
        {
            NPC,
            Races,
            Bundles,
            Archetypes
        }

        public static bool saveData(string path)
        {
            path = Path.GetFullPath(path);
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                if (!File.Exists(path))
                    using (File.Create(path)) { }
                string jsonData = "";

                string[] paths = new string[] { NPCsSavePath, RacesSavePath, BundlesSavePath, ArchetypesSavePath };

                jsonData = "SETTINGS DATA\n" +
                    JsonSerializer.Serialize(paths);

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

        public static bool loadData(string path)
        {
            path = Path.GetFullPath(path);
            try
            {
                if (File.ReadAllLines(path)[0] == "SETTINGS DATA")
                {
                    string jsonData = File.ReadAllText(path).Replace("SETTINGS DATA", "");
                    string[] paths = JsonSerializer.Deserialize<string[]>(jsonData);

                    NPCsSavePath = paths[0];
                    RacesSavePath = paths[1];
                    BundlesSavePath = paths[2];
                    ArchetypesSavePath = paths[3];
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


        /// <summary>
        /// Saving archive depending on the provided type in a provided directory. 
        /// </summary>
        /// <remarks>
        /// <para>These methods save and load the archives in their entirety using JSON files
        /// while collecting any occuring errors.</para>
        /// <para>The files are saved with a format decriptor that marks the type of
        /// archive saved. An archive file can not be loaded in a different type of archive.</para>
        /// </remarks>
        public static bool saveArchive(SaveType type, string path)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                if (!File.Exists(path))
                    using (File.Create(path)) { }
                string jsonData = type + "\n";
                switch (type)
                {
                    case SaveType.NPC:
                        jsonData += JsonSerializer.Serialize(ArchiveHandler.absoluteArchiveNPC);
                        break;

                    case SaveType.Races:
                        jsonData += JsonSerializer.Serialize(ArchiveHandler.raceStorage);
                        break;

                    case SaveType.Bundles:
                        jsonData += JsonSerializer.Serialize(ArchiveHandler.bundleStorage);
                        break;

                    case SaveType.Archetypes:
                        jsonData += JsonSerializer.Serialize(ArchiveHandler.archetypeStorage);
                        break;
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
        /// <summary>
        /// Loading an archive depending on the provided type from a provided file.
        /// </summary>
        /// <remarks>
        /// <para>These methods save and load the archives in their entirety using JSON files
        /// while collecting any occuring errors.</para>
        /// <para>The files are saved with a format decriptor that marks the type of
        /// archive saved. An archive file can not be loaded in a different type of archive.</para>
        /// </remarks>
        public static bool loadArchive(SaveType type, string path)
        {
            if (path == null)
                return false;
            try
            {
                if (File.ReadAllLines(path)[0] != type.ToString())
                    throw new FormatException("No correct format descriptor found.");

                //string jsonData = File.ReadAllText(path).Replace(type.ToString(), "");
                string jsonData = File.ReadAllLines(path)[1];

                switch (type)
                {
                    case SaveType.NPC:
                        ArchiveHandler.absoluteArchiveNPC = JsonSerializer.Deserialize<ArchiveNPC>(jsonData);
                        break;
                    case SaveType.Races:
                        ArchiveHandler.raceStorage = JsonSerializer.Deserialize<RaceStorage>(jsonData);
                        break;
                    case SaveType.Bundles:
                        ArchiveHandler.bundleStorage = JsonSerializer.Deserialize<BundleStorage>(jsonData);
                        break;
                    case SaveType.Archetypes:
                        ArchiveHandler.archetypeStorage = JsonSerializer.Deserialize<ArchetypeStorage>(jsonData);
                        break;
                }

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

        /// <summary>
        /// Choosing where to save by a dialog window.
        /// </summary>
        public static bool saveViaDialog(SaveType type, string windowTitle)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files(*.json)|*.json";
            saveFileDialog.Title = windowTitle;
            if (saveFileDialog.ShowDialog() == false)
                return false;

            string filename = saveFileDialog.FileName;
            saveArchive(type, filename);

            if (!ErrorHandler.errorPopup())
                if (type == SaveType.NPC)
                    NPCsSavePath = filename;
                else if (type == SaveType.Races)
                    RacesSavePath = filename;
                else if (type == SaveType.Bundles)
                    BundlesSavePath = filename;
                else if (type == SaveType.Archetypes)
                    ArchetypesSavePath = filename;

            return true;

        }

        /// <summary>
        /// Choosing where to load from by a dialog window.
        /// </summary>
        /// <returns>true if a file has been chosen, false if not or canceled.</returns>
        public static bool openViaDialog(SaveType type, string windowTitle)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.Title = windowTitle;
            if (openFileDialog.ShowDialog() == false)
                return false;

            string filename = openFileDialog.FileName;

            loadArchive(type, filename);

            if (!ErrorHandler.errorPopup())
                if (type == SaveType.NPC)
                    NPCsSavePath = filename;
                else if (type == SaveType.Races)
                    RacesSavePath = filename;
                else if (type == SaveType.Bundles)
                    BundlesSavePath = filename;
                else if (type == SaveType.Archetypes)
                    ArchetypesSavePath = filename;

            return true;

        }

    }
}

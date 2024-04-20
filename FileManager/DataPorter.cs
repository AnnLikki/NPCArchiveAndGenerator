using Archives;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using static FileManager.SnL;
using System.Text.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using static Archives.Enums;

namespace FileManager
{
    public static class DataPorter
    {
        public static bool Export(IEnumerable<WeightedElement> data, string path)
        {
            try
            {
                string[] lines;
                lines = data.Select(element => element.ToString()).ToArray();
                       
                File.WriteAllLines(path, lines);
                return true;
            }
            catch (Exception e)
            {
                ErrorHandler.collectError("Could not export to file " + path);
                Console.WriteLine($"Error exporting data: {e.Message}");
                return false;
            }
        }

        public static IEnumerable<WeightedElement> Import(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                return lines.Select(line =>
                {
                    string[] parts = line.Split(';');

                    if (parts.Length < 1)
                    {
                        throw new FormatException($"Invalid line: {line}");
                    }

                    string value = parts[0];
                    int weight = parts.Length > 1 ? int.Parse(parts[1]) : 1;
                    Gender gender = Gender.Neutral;
                    if (parts.Length > 2 && Enum.TryParse(parts[2], out Gender parsedGender))
                    {
                        gender = parsedGender;
                    }

                    WeightedElement element = new WeightedElement(value, weight, gender);

                    return element;
                });
            }
            catch (Exception e)
            {
                ErrorHandler.collectError("Could not import file " + path);
                Console.WriteLine($"Error importing data: {e.Message}");
                return Enumerable.Empty<WeightedElement>();
            }
        }



        public static bool exportViaDialog(IEnumerable<WeightedElement> data, string windowTitle)
        {
            SaveFileDialog exportFileDialog = new SaveFileDialog();
            exportFileDialog.Filter = "Text files (*.txt)|*.txt";
            exportFileDialog.Title = windowTitle;
            if (exportFileDialog.ShowDialog() == false)
                return false;

            string filename = exportFileDialog.FileName;
            Export(data, filename);

            ErrorHandler.errorPopup();

            return true;

        }

        public static bool importViaDialog(string windowTitle, out IEnumerable<WeightedElement> data)
        {
            data = null;
            OpenFileDialog importFileDialog = new OpenFileDialog();
                importFileDialog.Filter = "Text files (*.txt)|*.txt";
            importFileDialog.Title = windowTitle;
            if (importFileDialog.ShowDialog() == false)
                return false;

            string filename = importFileDialog.FileName;

            data=Import(filename);

            ErrorHandler.errorPopup();
                
            return true;

        }

    }
}

using FileManager;
using NPCGenerator;
using System.Windows.Documents;

namespace NPCArchiveAndGenerator
{
    internal static class Controller
    {
        public static NPCsArchiveUC npcsArchiveUC;
        public static RacesArchiveUC racesArchiveUC;
        public static BundlesArchivesUC bundlesArchivesUC;
        public static ArchetypesArchivesUC archetypesArchivesUC;
        public enum Status
        {
            NPC,
            Race,
            Bundle,
            Archetype
        }

        public static bool safeMode = true;
        public static Status status = Status.NPC;

        public static string GetFileName()
        {
            switch (status)
            {
                case Status.NPC:
                    if (SnL.NPCsSavePath != null)
                        return SnL.NPCsSavePath;
                    else
                        return "Unsaved NPC Archive";
                case Status.Race:
                    if (SnL.RacesSavePath != null)
                        return SnL.RacesSavePath;
                    else
                        return "Unsaved Races Archive";
                case Status.Bundle:
                    if (SnL.BundlesSavePath != null)
                        return SnL.BundlesSavePath;
                    else
                        return "Unsaved Bundles Archive";
                case Status.Archetype:
                    if (SnL.ArchetypesSavePath != null)
                        return SnL.ArchetypesSavePath;
                    else
                        return "Unsaved Archetype Archive";
                default:
                    return "WTF";
            }
        }

        public static void UpdateFileName()
        {
            switch (status)
            {
                case Status.NPC:
                    npcsArchiveUC.fileNameLbl.Content = GetFileName();
                    break;
                case Status.Race:
                    racesArchiveUC.fileNameLbl.Content = GetFileName();
                    break;
                case Status.Bundle:
                    bundlesArchivesUC.fileNameLbl.Content = GetFileName();
                    break;
                case Status.Archetype:
                    archetypesArchivesUC.fileNameLbl.Content = GetFileName();
                    break;
                default:
                    return;
            }
        }


    }
}
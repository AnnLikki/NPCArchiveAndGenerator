using FileManager;

namespace NPCArchiveAndGenerator
{
    internal static class Controller
    {

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

    }
}

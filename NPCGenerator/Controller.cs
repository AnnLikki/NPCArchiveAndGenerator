using FileManager;
using NPCGenerator;

namespace NPCArchiveAndGenerator
{
    internal static class Controller
    {

        public enum Status
        {
            ArchiveNPC,
            ArchiveRace,
            ArchiveLA
        }

        public static bool safeMode = true;
        public static Status status = Status.ArchiveNPC;

        public static string GetFileName()
        {
            switch (status)
            {
                case Status.ArchiveNPC:
                    if (SnL.NPCsSavePath != null)
                        return SnL.NPCsSavePath;
                    else
                        return "Unsaved NPC Archive";
                case Status.ArchiveRace:
                    if (SnL.racesSavePath != null)
                        return SnL.racesSavePath;
                    else
                        return "Unsaved Races Archive";
                case Status.ArchiveLA:
                    return "NOT IMPLEMENTED"; // TODO: Finish
                default:
                    return "WTF";
            }
        }

    }
}

namespace Archives
{
    // A static class to store the global archives absoluteArchiveNPC and archiveRace.
    // Provides an easier to access from other classes.
    // In the future will be also handling Little Archives.
    public static class ArchiveHandler
    {
        public static ArchiveNPC absoluteArchiveNPC = new ArchiveNPC();
        public static ArchiveRace archiveRace = new ArchiveRace();

    }
}

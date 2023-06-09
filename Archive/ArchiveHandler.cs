namespace Archives
{
    // A static class to store the global archives archiveNPC and archiveRace.
    // Provides an easier to access from other classes.
    // In the future will be also handling Little Archives.
    public static class ArchiveHandler
    {
        public static ArchiveNPC archiveNPC = new ArchiveNPC();
        public static ArchiveRace archiveRace = new ArchiveRace();

    }
}

namespace Archives
{
    public static class ArchiveHandler
    {
        /// <summary>
        /// Archive that contains all NPCs.
        /// </summary>
        public static ArchiveNPC absoluteArchiveNPC = new ArchiveNPC();

        /// <summary>
        /// A collection of absolute archives that contain all Bundles. 
        /// </summary>
        public static BundleStorage bundleStorage = new BundleStorage();

        public static RaceStorage raceStorage = new RaceStorage();

        public static ArchetypeStorage archetypeStorage = new ArchetypeStorage();


    }
}

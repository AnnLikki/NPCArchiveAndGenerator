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

        /// <summary>
        /// An archetype that will be applied if no other archetype is used.
        /// </summary>
        public static Archetype defaultArchetype = new Archetype("Default Archetype");

    }
}

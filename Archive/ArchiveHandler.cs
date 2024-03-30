namespace Archives
{
    public static class ArchiveHandler
    {
        /// <summary>
        /// Archive that contains all NPCs.
        /// </summary>
        public static ArchiveNPC absoluteArchiveNPC = new ArchiveNPC();
        /// <summary>
        /// A collection of absolute archives that contain all Races and Bundles. 
        /// </summary>
        public static ArchiveStorage absoluteArchives = new ArchiveStorage();

        /// <summary>
        /// A collection of default archives that will be picked from if none other archives are specified in an archetype/there's no archetype.
        /// </summary>
        public static Kit defaultArchives = new Kit();
        /// <summary>
        /// Archive of integers with weights to pick from if none other ages are specified in an archetype/there's no archetype.
        /// </summary>
        public static AgeDistribution defaultAgeDistribution = new AgeDistribution();

    }
}

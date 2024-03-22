using System;
using System.Collections.Generic;

namespace Archives
{
    /// <summary>
    /// A static class to store the global archives and the tempopary archives.
    /// </summary>
    /// <remarks>
    /// Contains <c>absoluteArchiveNPC</c> and <c>absoluteArchiveRace</c>.
    /// Provides an easier to access from other classes.
    /// </remarks>
    public static class ArchiveHandler
    {
        /// <summary>
        /// Contains all of the created NPCs.
        /// </summary>
        public static ArchiveNPC absoluteArchiveNPC = new ArchiveNPC();

        /// <summary>
        /// Contains all of the created races.
        /// </summary>
        public static ArchiveRace absoluteArchiveRace = new ArchiveRace();


        public static ArchiveAgeRange defaultAgeRanges = new ArchiveAgeRange();

        public static ArchiveDictionary defaultArchives = new ArchiveDictionary();


        // TODO: temp saves

        ///// <summary>
        ///// Temporary NPC archive for autosaving.
        ///// </summary>
        //public static ArchiveNPC tempArchiveNPC = new ArchiveNPC();

        ///// <summary>
        ///// Temporary races archive for autosaving.
        ///// </summary>
        //public static ArchiveRace tempArchiveRace = new ArchiveRace();


    }
}

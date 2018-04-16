namespace Console.Events {
    
    /// <summary>
    /// A static class to define constants which can be used anywhere within the project.
    /// </summary>
    public static class ConsoleEvents {
        
        /// <summary>
        /// A readonly constant global event name for adding an output to the Console.
        /// </summary>
        public readonly static string OutputEvent = "Add Output";

        /// <summary>
        /// A readonly constant global event name for caching the Instance Id.
        /// </summary>
        public readonly static string CacheEvent = "Cache Id";

        /// <summary>
        /// A readonly constant global event for removing the Instance Id.
        /// </summary>
        public readonly static string RemoveEvent = "Remove Id";
        
        /// <summary>
        /// A readonly constant global event name for showing all the Ids to the console given a size.
        /// </summary>
        public readonly static string ShowIdEvent = "showids";
    }
}

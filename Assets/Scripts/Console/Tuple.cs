namespace DeveloperConsole {

    /// <summary>
    /// A structured pair of elements.
    /// </summary>
    public struct Tuple<T, U> {
        
        public T first;
        public U second;

        public Tuple(T first, U second) {
            this.first = first;
            this.second = second;
        }
        
        public static Tuple<T, U> Create(T first, U second) {
            return new Tuple<T, U>(first, second);
        }
    }
}

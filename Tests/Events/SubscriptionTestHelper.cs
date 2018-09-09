namespace Console.Tests {

    public class SubscriptionTestHelper {

        public int sum;
        public readonly string[] AddEvents;

        /// <summary>
        /// Returns the string within the AddEvents array.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[int key] {
            get { return AddEvents[key]; }
        }

        /// <summary>
        /// A basic constructor to allow for configurable testing with delegates.
        /// </summary>
        /// <param name="sum">The initial value of the sum.</param>
        public SubscriptionTestHelper(int sum) {
            AddEvents = new[] { "add", "add1", "add2", "add3", "add4" };
            this.sum = sum;
        }

        /// <summary>
        /// Use this to subscribe the Add method to the global event table.
        /// </summary>
        /// <param name="i">The key of the AddEvents array.</param>
        public void Subscribe(int i, int size) {
            var @event = AddEvents[i];
            switch (size) {
                case 4:
                    GlobalEventHandler.SubscribeEvent<int, int, int, int>(@event, Add);
                    return;
                case 3:
                    GlobalEventHandler.SubscribeEvent<int, int, int>(@event, Add);
                    return;
                case 2:
                    GlobalEventHandler.SubscribeEvent<int, int>(@event, Add);
                    return;
                case 1:
                    GlobalEventHandler.SubscribeEvent<int>(@event, Add);
                    return;
                default:
                    GlobalEventHandler.SubscribeEvent(@event, Add);
                    return;
            }
        }

        /// <summary>
        /// Use this to remove the Add method to the global event table.
        /// </summary>
        /// <param name="i">The key of the AddEvents array.</param>
        public void UnsubscribeOne(int i) {
            GlobalEventHandler.UnsubscribeEvent(AddEvents[i], Add);
        }

        private void Add() {
            sum++;
        }

        private void Add(int a) {
            sum += a;
        }

        private void Add(int a, int b) {
            sum += (a + b);
        }

        private void Add(int a, int b, int c) {
            sum += (a + b + c);
        }

        private void Add(int a, int b, int c, int d) {
            sum += (a + b + c + d);
        }
    }
}

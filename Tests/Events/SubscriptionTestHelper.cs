namespace Console.Tests {

    public class SubscriptionTestHelper {

        public readonly string AddEvent = "add";
        public int sum;

        /// <summary>
        /// A basic constructor to allow for configurable testing with delegates.
        /// </summary>
        /// <param name="sum">The initial value of the sum.</param>
        public SubscriptionTestHelper(int sum) {
            this.sum = sum;
        }

        /// <summary>
        /// Use this to subscribe the AddOne method to the global event table.
        /// </summary>
        public void Subscribe() {
            GlobalEventHandler.SubscribeEvent(AddEvent, AddOne);
        }

        /// <summary>
        /// Use this to remove the AddOne method to the global event table.
        /// </summary>
        public void Unsubscribe() {
            GlobalEventHandler.UnsubscribeEvent(AddEvent, AddOne);
        }

        private void AddOne() {
            sum++;
        }
    }
}

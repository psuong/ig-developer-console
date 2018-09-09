using NUnit.Framework;
using System;

namespace Console.Tests {

    public class GlobalEventHandlerTests {
 
        [SetUp, TearDown]
        public void SetUpAndTearDown() {
            GlobalEventHandler.UnsubscribeAll();
        }

        [Test]
        public void AddingAnAnonymousFunction() {
            var key = "test-event";
#pragma warning disable CS0219 
            Action action = () => { int sum = 1 + 1; };
#pragma warning restore CS0219

            GlobalEventHandler.SubscribeEvent(key, action);
            Assert.IsNotEmpty(GlobalEventHandler.GlobalEventTable, "No delegate was subscribed!");

            GlobalEventHandler.UnsubscribeEvent(key, action);
            var @delegate = GlobalEventHandler.GlobalEventTable[key] as Action;
            Assert.IsFalse(@delegate == action, "The delegate was not removed!");
        }

        [Test]
        public void SubscribingANoParamMethod() {
            var initialValue = 0;
            var result = GetResult(initialValue, 0);
            Assert.AreEqual(1, result, "Value mismatch!");
        }

        [Test]
        public void SubscribingAOneParamMethod() {
            var initialValue = 2;
            var result = GetResult(1, initialValue);
            Assert.AreEqual(initialValue + 1, result, "Value mismatch!");
        }

        [Test]
        public void SubscribingATwoParamMethod() {
            var initialValue = 1;
            var result = GetResult(initialValue, 0, 2, 3);
            Assert.AreEqual(5, result, "Value mismatch!");
        }

        [Test]
        public void SubscribeAThreeParamMethod() {
            var initialValue = 0;
            var result = GetResult(3, initialValue, 1, 2, 3);
            Assert.AreEqual(6, result, "Value mismatch!");
        }

        [Test]
        public void SubscribeAFourParamMethod() {
            var initialValue = 3;
            var result = GetResult(4, initialValue, 2, 2, 2, 2);
            Assert.AreEqual(11, result, "Value mismatch!");
        }

        private Tuple<int, SubscriptionTestHelper> CreateSubscriptionTestHelper(int value) {
            return Tuple.Create(value, new SubscriptionTestHelper(value));
        }

        private int GetResult(int index, int initialValue, params int[] valuesToAdd) {
            var tuple = CreateSubscriptionTestHelper(initialValue);
            var testHelper = tuple.Item2;
            Assert.AreEqual(tuple.Item1, tuple.Item2.sum, "Value mismatch!");

            testHelper.Subscribe(index, valuesToAdd.Length);
            var @event = testHelper[index];

            switch(valuesToAdd.Length) {
                case 4:
                    GlobalEventHandler.InvokeEvent(@event, valuesToAdd[0], valuesToAdd[1], valuesToAdd[2], valuesToAdd[3]);
                    break;
                case 3:
                    GlobalEventHandler.InvokeEvent(@event, valuesToAdd[0], valuesToAdd[1], valuesToAdd[2]);
                    break;
                case 2:
                    GlobalEventHandler.InvokeEvent(@event, valuesToAdd[0], valuesToAdd[1]);
                    break;
                case 1:
                    GlobalEventHandler.InvokeEvent(@event, valuesToAdd[0]);
                    break;
                default:
                    GlobalEventHandler.InvokeEvent(testHelper[index]);
                    break;
            }

            return testHelper.sum;
        }
    }
}

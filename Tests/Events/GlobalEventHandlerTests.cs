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
        public void CheckingIfADelegateWasInvoked() {
            var initialValue = 1;
            var testHelper = new SubscriptionTestHelper(initialValue);
            Assert.AreEqual(initialValue, testHelper.sum, "Value mismatch!");

            // Subscribe the event
            testHelper.Subscribe();

            GlobalEventHandler.InvokeEvent(testHelper.AddEvent);

            // Unsubscribe the event
            testHelper.Unsubscribe();

            Assert.AreEqual(initialValue + 1, testHelper.sum, "Value mismatch!");
        }

        [Test]
        public void SubscribingATwoParamMethod() {
            throw new NotImplementedException();
        }

        [Test]
        public void SubscribeAThreeParamMethod() {
            throw new NotImplementedException();
        }

        [Test]
        public void SubscribeAFourParamMethod() {
            throw new NotImplementedException();
        }
    }
}

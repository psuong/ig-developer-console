using NUnit.Framework;
using System;

namespace Console.Tests {

    public class GlobalEventHandlerTests {
 
        [SetUp]
        public void SetUp() {
            GlobalEventHandler.UnsubscribeAll();
        }

        [Test]
        public void AddingAnAnonymousFunction() {
            var key = "test-event";
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            Action action = () => { int sum = 1 + 1; };
#pragma warning restore CS0219 // Variable is assigned but its value is never used

            GlobalEventHandler.SubscribeEvent(key, action);
            Assert.IsNotEmpty(GlobalEventHandler.GlobalEventTable, "No delegate was subscribed!");

            GlobalEventHandler.UnsubscribeEvent(key, action);
            var @delegate = GlobalEventHandler.GlobalEventTable[key] as Action;
            Assert.IsFalse(@delegate == action, "The delegate was not removed!");
        }
    }
}

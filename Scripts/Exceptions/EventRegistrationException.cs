using System;

namespace Console {

    /// <summary>
    /// A custom exception to be thrown when registering an event.
    /// </summary>
    public class EventRegistrationException : Exception {
        
        public EventRegistrationException() { }
        
        public EventRegistrationException(string message) : base(message) { }

        public EventRegistrationException(string message, Exception err) : base(message, err) { }
    }
}

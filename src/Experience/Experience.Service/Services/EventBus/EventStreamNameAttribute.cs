using System;

namespace Experience.Service.Services.EventBus
{
    public class EventStreamNameAttribute : Attribute
    {
        public EventStreamNameAttribute(string collectionName)
        {
            EventStreamName = collectionName;
        }

        public string EventStreamName { get; private set; }
    }
}
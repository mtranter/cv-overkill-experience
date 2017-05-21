using System.Threading.Tasks;

namespace Experience.Service.Services.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventStreamNamingStrategy _eventStreamNamingStrategy;

        public EventBus(IEventDispatcher eventDispatcher, IEventStreamNamingStrategy eventStreamNamingStrategy)
        {
            _eventDispatcher = eventDispatcher;
            _eventStreamNamingStrategy = eventStreamNamingStrategy;
        }

        public Task Publish<T>(T @event)
        {
            var eventStreamName = _eventStreamNamingStrategy.GetEventStreamName(@event);
            return _eventDispatcher.Dispatch(eventStreamName, @event);
        }
    }
}
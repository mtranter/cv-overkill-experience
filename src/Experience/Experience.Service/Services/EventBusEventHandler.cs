using Experience.Service.Services.EventBus;

namespace Experience.Service.Services
{
    public class EventBusEventHandler : IEventListener
    {
        private readonly IEventBus _eventBus;

        public static EventBusEventHandler Initialize(IEventBus eventBus)
        {
            var retval = new EventBusEventHandler(eventBus);
            DomainEvents.Subscribe(retval);
            return retval;
        }

        private EventBusEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Handle<T>(T @event)
        {
            _eventBus.Publish(@event);
        }
    }
}
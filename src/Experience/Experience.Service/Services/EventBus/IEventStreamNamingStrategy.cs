namespace Experience.Service.Services.EventBus
{
    public interface IEventStreamNamingStrategy
    {
        string GetEventStreamName<T>(T @eventType);
    }
}
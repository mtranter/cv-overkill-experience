namespace Experience.Service.Services
{
    public interface IEventListener<T>
    {
        void Handle(T @event);
    }

    public interface IEventListener
    {
        void Handle<T>(T @event);
    }
}
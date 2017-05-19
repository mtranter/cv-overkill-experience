namespace Expereince.Services
{
    public interface IEventListener<T>
    {
        void Handle(T @event);
    }
}
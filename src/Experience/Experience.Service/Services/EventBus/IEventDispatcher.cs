using System.Threading.Tasks;

namespace Experience.Service.Services.EventBus
{
    public interface IEventDispatcher
    {
        Task Dispatch<T>(string eventStream, T @event);
    }
}
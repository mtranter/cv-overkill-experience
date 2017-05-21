using System.Threading.Tasks;

namespace Experience.Service.Services.EventBus
{
    public interface IEventBus
    {
        Task Publish<T>(T @event);
    }
}
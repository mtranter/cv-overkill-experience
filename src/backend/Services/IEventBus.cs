using System.Threading.Tasks;

namespace Expereince.Services
{
    public interface IEventBus
    {
        Task Publish<T>(T @event);
    }
}
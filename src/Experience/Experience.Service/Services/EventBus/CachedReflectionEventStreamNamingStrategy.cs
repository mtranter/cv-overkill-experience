using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Experience.Service.Services.EventBus
{
    public class CachedReflectionEventStreamNamingStrategy : IEventStreamNamingStrategy
    {
        private readonly ConcurrentDictionary<Type,string> _cache = new ConcurrentDictionary<Type, string>();

        public string GetEventStreamName<T>(T eventType)
        {
            var type = typeof(T);
            return _cache.GetOrAdd(type, GetEventStreamName);
        }

        private string GetEventStreamName(Type type)
        {
            var attr = type.GetTypeInfo().GetCustomAttribute<EventStreamNameAttribute>();
            if (attr != null)
            {
                return attr.EventStreamName;
            }
            else
            {
                return type.Name;
            }
        }
    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Expereince.Services
{
    public static class DomainEvents
    {
        private static ConcurrentDictionary<Type,List<dynamic>> Handlers = new ConcurrentDictionary<Type, List<dynamic>>();

        public static IDisposable Subscribe<T>(IEventListener<T> listener)
        {
            var list = Handlers.GetOrAdd(typeof(T), new List<dynamic>());
            var indexToRemove = list.Count;
            list.Add(listener);
            return Unsubscriber.Create(list, indexToRemove);
        }

        public static void Publish<T>(T @event)
        {
            var list = Handlers.GetOrAdd(typeof(T), new List<dynamic>());
            list.ForEach(e => e.Handle(@event));
        }

        class Unsubscriber : IDisposable
        {
            private readonly Action _dispose;

            public static Unsubscriber Create(List<object> list, int indexToRemove)
            {
                return new Unsubscriber(() => list.RemoveAt(indexToRemove));
            }

            public Unsubscriber(Action dispose)
            {
                _dispose = dispose;
            }

            public void Dispose()
            {
                _dispose();
            }
        }
    }
}
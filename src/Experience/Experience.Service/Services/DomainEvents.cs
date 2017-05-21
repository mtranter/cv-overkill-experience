using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Experience.Service.Services
{
    public static class DomainEvents
    {
        private static ConcurrentDictionary<Type,List<dynamic>> Handlers = new ConcurrentDictionary<Type, List<dynamic>>();
        private static List<IEventListener> GlobalHandlers = new List<IEventListener>();

        public static IDisposable Subscribe<T>(IEventListener<T> listener)
        {
            var list = Handlers.GetOrAdd(typeof(T), new List<dynamic>());
            var indexToRemove = list.Count;
            list.Add(listener);
            return Unsubscriber.Create(() => list.RemoveAt(indexToRemove));
        }

        public static IDisposable Subscribe(IEventListener listener)
        {
            var indexToRemove = GlobalHandlers.Count;
            GlobalHandlers.Add(listener);
            return Unsubscriber.Create(() => GlobalHandlers.RemoveAt(indexToRemove));
        }

        public static void Publish<T>(T @event)
        {
            var list = Handlers.GetOrAdd(typeof(T), new List<dynamic>());
            list.ForEach(e => e.Handle(@event));
            GlobalHandlers.ForEach(h => h.Handle(@event));
        }

        class Unsubscriber : IDisposable
        {
            private readonly Action _dispose;

            public static Unsubscriber Create(Action disposeAction)
            {
                return new Unsubscriber(disposeAction);
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
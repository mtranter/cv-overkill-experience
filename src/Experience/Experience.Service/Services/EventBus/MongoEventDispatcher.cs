using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Experience.Service.Services.EventBus
{
    public class MongoEventDispatcher : IEventDispatcher
    {
        private readonly IMongoDatabase _mongoDb;
        private readonly ISet<string> _existingCollections;

        public static  MongoEventDispatcher Initialize(IMongoDatabase mongoDb)
        {
            var collections = mongoDb.ListCollections();
            var collectionNames = collections.ToEnumerable().Select(bd => bd["name"].ToString());
            return new MongoEventDispatcher(mongoDb, new HashSet<string>(collectionNames));
        }

        private MongoEventDispatcher(IMongoDatabase mongoDb, ISet<string> existingCollections)
        {
            _mongoDb = mongoDb;
            _existingCollections = existingCollections;
        }


        public Task Dispatch<T>(string eventStream, T @event)
        {
            if (!_existingCollections.Contains(eventStream))
            {
                var options = new CreateCollectionOptions()
                {
                    Capped = true,
                    MaxDocuments = 1000,
                    MaxSize = 500
                };
                _mongoDb.CreateCollection(eventStream, options);
                _existingCollections.Add(eventStream);
            }
            return _mongoDb.GetCollection<MongoStreamsEvent<T>>(eventStream).InsertOneAsync(new MongoStreamsEvent<T>(@event));
        }

        class MongoStreamsEvent<TDomainEvent>
        {
            public MongoStreamsEvent(TDomainEvent @event)
            {
                Event = @event;
                EventType = @event.GetType().Name;
            }

            [BsonElement]
            public TDomainEvent Event { get; private set; }

            [BsonElement]
            public string EventType { get; private set; }
        }
    }
}
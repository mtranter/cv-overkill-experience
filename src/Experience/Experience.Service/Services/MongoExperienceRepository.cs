using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Experience.Service.Services
{
    public class MongoExperienceRepository : IExperienceRepository
    {
        private readonly IMongoDatabase _mongoDb;

        static MongoExperienceRepository()
        {
            BsonClassMap.RegisterClassMap<Models.Experience>();
        }

        public MongoExperienceRepository(IMongoDatabase mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public async Task<IEnumerable<Models.Experience>> ListAllAsync()
        {
            return (await GetCollection()
                .FindAsync(_ => true)).ToEnumerable();
        }

        public async Task<Models.Experience> GetExperience(Guid id)
        {
            return await (await GetCollection()
                .FindAsync(i => i.Id == id)).FirstOrDefaultAsync();
        }

        public Task InsertExperience(Models.Experience experience)
        {
            return GetCollection().InsertOneAsync(experience);
        }

        public Task UpdateExperience(Models.Experience experience)
        {
            var filter = Builders<Models.Experience>.Filter.Eq("Id", experience.Id.ToString());
            return GetCollection()
                .ReplaceOneAsync(filter, experience);
        }

        public Task DeleteExperience(Guid id)
        {
            var filter = Builders<Models.Experience>.Filter.Eq("Id", id.ToString());
            return GetCollection().DeleteOneAsync(filter);
        }

        private IMongoCollection<Models.Experience> GetCollection()
        {
            return _mongoDb.GetCollection<Models.Experience>("experience");
        }
    }
}
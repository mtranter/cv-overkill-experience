using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Expereince.Services
{
    public class MongoExperienceRepository : IExperienceRepository
    {
        private readonly IMongoDatabase _mongoDb;

        static MongoExperienceRepository()
        {
            BsonClassMap.RegisterClassMap<Experience.Models.Experience>();
        }

        public MongoExperienceRepository(IMongoDatabase mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public async Task<IEnumerable<Experience.Models.Experience>> ListAllAsync()
        {
            return (await _mongoDb.GetCollection<Experience.Models.Experience>("experience")
                .FindAsync(_ => true)).ToEnumerable();
        }

        public async Task<Experience.Models.Experience> GetExperience(Guid id)
        {
            return await (await _mongoDb.GetCollection<global::Experience.Models.Experience>("experience")
                .FindAsync(i => i.Id == id)).FirstOrDefaultAsync();
        }

        public Task InsertExperience(Experience.Models.Experience experience)
        {
            return _mongoDb.GetCollection<global::Experience.Models.Experience>("experience").InsertOneAsync(experience);
        }

        public Task UpdateExperience(global::Experience.Models.Experience experience)
        {
            var filter = Builders<global::Experience.Models.Experience>.Filter.Eq("Id", experience.Id.ToString());
            return _mongoDb.GetCollection<global::Experience.Models.Experience>("experience")
                .ReplaceOneAsync(filter, experience);
        }
    }
}
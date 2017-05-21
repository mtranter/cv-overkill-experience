using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Experience.Service.Services
{
    public interface IExperienceRepository
    {
        Task<IEnumerable<Models.Experience>> ListAllAsync();
        Task<Models.Experience> GetExperience(Guid id);
        Task InsertExperience(Models.Experience create);
        Task UpdateExperience(Models.Experience experience);
        Task DeleteExperience(Guid id);
    }
}
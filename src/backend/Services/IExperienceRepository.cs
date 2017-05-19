using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Experience = Experience.Models.Experience;

namespace Expereince.Services
{
    public interface IExperienceRepository
    {
        Task<IEnumerable<global::Experience.Models.Experience>> ListAllAsync();
        Task<global::Experience.Models.Experience> GetExperience(Guid id);
        Task InsertExperience(global::Experience.Models.Experience create);
        Task UpdateExperience(global::Experience.Models.Experience experience);
    }
}
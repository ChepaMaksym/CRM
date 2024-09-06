using CRM.DTO.Seed;
using CRM.Models;

namespace CRM.Services.Interface
{
    public interface ISeedService
    {
        Task<IEnumerable<Seed>> GetAllAsync();
        Task<Seed> GetByIdAsync(int id);
        Task<int> AddAsync(AddSeedDTO seed);
        Task UpdateAsync(UpdateSeedDTO seed);
        Task DeleteAsync(int id);
    }
}

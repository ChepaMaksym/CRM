using CRM.DTO.Harvest;
using CRM.Models;

namespace CRM.Services.Interface
{
    public interface IHarvestService
    {
        Task<IEnumerable<Harvest>> GetAllAsync();
        Task<Harvest> GetByIdAsync(int id);
        Task<int> AddAsync(AddHarvestDTO harvest);
        Task UpdateAsync(UpdateHarvestDTO harvest);
        Task DeleteAsync(int id);
    }
}

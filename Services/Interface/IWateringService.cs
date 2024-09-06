using CRM.DTO.Watering;
using CRM.Models;

namespace CRM.Services.Interface
{
    public interface IWateringService
    {
        Task<IEnumerable<Watering>> GetAllAsync();
        Task<Watering> GetByIdAsync(int id);
        Task<int> AddAsync(AddWateringDTO watering);
        Task UpdateAsync(UpdateWateringDTO watering);
        Task DeleteAsync(int id);
    }
}

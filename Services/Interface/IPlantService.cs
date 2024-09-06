using CRM.DTO.Plant;
using CRM.Models;

namespace CRM.Services.Interface
{
    public interface IPlantService
    {
        Task<IEnumerable<Plant>> GetAllAsync();
        Task<Plant> GetByIdAsync(int id);
        Task<int> AddAsync(AddPlantDTO plant);
        Task UpdateAsync(UpdatePlantDTO plant);
        Task DeleteAsync(int id);
    }
}

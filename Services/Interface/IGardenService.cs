using CRM.DTO.Garden;
using CRM.Models;

namespace CRM.Services.Interface
{
    public interface IGardenService
    {
        Task<IEnumerable<Garden>> GetAllAsync();
        Task<Garden> GetByIdAsync(int id);
        Task<int> AddAsync(AddGardenDTO garden);
        Task UpdateAsync(UpdateGardenDTO garden);
        Task DeleteAsync(int id);
    }
}

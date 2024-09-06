using CRM.DTO.Pot;
using CRM.Models;

namespace CRM.Services.Interface
{
    public interface IPotService
    {
        Task<IEnumerable<Pot>> GetAllAsync();
        Task<Pot> GetByIdAsync(int id);
        Task<int> AddAsync(AddPotDTO pot);
        Task UpdateAsync(UpdatePotDTO pot);
        Task DeleteAsync(int id);
    }
}

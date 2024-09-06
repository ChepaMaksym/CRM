using CRM.DTO.SoilType;
using CRM.Models;

namespace CRM.Services.Interface
{
    public interface ISoilTypeService
    {
        Task<IEnumerable<SoilType>> GetAllAsync();
        Task<SoilType> GetByIdAsync(int id);
        Task<int> AddAsync(AddSoilTypeDTO soilType);
        Task UpdateAsync(UpdateSoilTypeDTO soilType);
        Task DeleteAsync(int id);
    }
}

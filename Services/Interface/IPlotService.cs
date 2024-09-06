using CRM.DTO.Plot;
using CRM.Models;

namespace CRM.Services.Interface
{
    public interface IPlotService
    {
        Task<IEnumerable<Plot>> GetAllAsync();
        Task<Plot> GetByIdAsync(int id);
        Task<int> AddAsync(AddPlotDTO plot);
        Task UpdateAsync(UpdatePlotDTO plot);
        Task DeleteAsync(int id);
    }
}

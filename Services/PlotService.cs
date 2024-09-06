using CRM.DTO.Plot;
using CRM.Models;
using CRM.QueryManagers;
using CRM.QueryManagers.Tables;
using CRM.Services.Interface;
using Microsoft.Data.SqlClient;

namespace CRM.Services
{
    public class PlotService(string connectionString) : IPlotService
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<Plot>> GetAllAsync()
        {
            var plots = new List<Plot>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(PlotQueryManager.GetAllPlots, connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    plots.Add(new Plot
                    {
                        PlotId = (int)reader[PlotColumns.PlotId],
                        GardenId = (int)reader[PlotColumns.GardenId],
                        SoilTypeId = (int)reader[PlotColumns.SoilTypeId],
                        Name = (string)reader[PlotColumns.Name],
                        Size = (decimal)reader[PlotColumns.Size]
                    });
                }
            }
            return plots;
        }

        public async Task<Plot> GetByIdAsync(int id)
        {
            Plot? plot = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(PlotQueryManager.GetPlotById, connection);
                command.Parameters.AddWithValue(PlotQueryManager.PlotIdWithAt, id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    plot = new Plot
                    {
                        PlotId = (int)reader[PlotColumns.PlotId],
                        GardenId = (int)reader[PlotColumns.GardenId],
                        SoilTypeId = (int)reader[PlotColumns.SoilTypeId],
                        Name = (string)reader[PlotColumns.Name],
                        Size = (decimal)reader[PlotColumns.Size]
                    };
                }
            }
            return plot ?? throw new InvalidOperationException($"Plot with ID {id} not found."); ;
        }

        public async Task<int> AddAsync(AddPlotDTO plot)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(PlotQueryManager.InsertPlot, connection);
            command.Parameters.AddWithValue(PlotQueryManager.GardenIdWithAt, plot.GardenId);
            command.Parameters.AddWithValue(PlotQueryManager.SoilTypeIdWithAt, plot.SoilTypeId);
            command.Parameters.AddWithValue(PlotQueryManager.NameWithAt, plot.Name);
            command.Parameters.AddWithValue(PlotQueryManager.SizeWithAt, plot.Size);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result is int id ? id : throw new InvalidOperationException("Failed to retrieve the generated ID.");
        }

        public async Task UpdateAsync(UpdatePlotDTO plot)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(PlotQueryManager.UpdatePlot, connection);
            command.Parameters.AddWithValue(PlotQueryManager.GardenIdWithAt, plot.GardenId);
            command.Parameters.AddWithValue(PlotQueryManager.SoilTypeIdWithAt, plot.SoilTypeId);
            command.Parameters.AddWithValue(PlotQueryManager.NameWithAt, plot.Name);
            command.Parameters.AddWithValue(PlotQueryManager.SizeWithAt, plot.Size);
            command.Parameters.AddWithValue(PlotQueryManager.PlotIdWithAt, plot.PlotId);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Plot with ID {plot.PlotId} not found.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(PlotQueryManager.DeletePlot, connection);
            command.Parameters.AddWithValue(PlotQueryManager.PlotIdWithAt, id);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Plot with ID {id} not found.");
            }
        }
    }
}

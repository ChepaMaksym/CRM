using CRM.DTO.Watering;
using CRM.Models;
using CRM.QueryManagers;
using CRM.QueryManagers.Tables;
using CRM.Services.Interface;
using Microsoft.Data.SqlClient;

namespace CRM.Services
{
    public class WateringService(string connectionString) : IWateringService
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<Watering>> GetAllAsync()
        {
            var watering = new List<Watering>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(WateringQueryManager.GetAllWaterings, connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    watering.Add(new Watering
                    {
                        WateringId = (int)reader[WateringColumns.WateringId],
                        PlotId = (int)reader[WateringColumns.PlotId],
                        Date = (DateTime)reader[WateringColumns.Date],
                        Amount = (decimal)reader[WateringColumns.Amount]
                    });
                }
            }
            return watering;
        }

        public async Task<Watering> GetByIdAsync(int id)
        {
            Watering? watering = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(WateringQueryManager.GetWateringById, connection);
                command.Parameters.AddWithValue(WateringQueryManager.WateringIdWithAt, id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    watering = new Watering
                    {
                        WateringId = (int)reader[WateringColumns.WateringId],
                        PlotId = (int)reader[WateringColumns.PlotId],
                        Date = (DateTime)reader[WateringColumns.Date],
                        Amount = (decimal)reader[WateringColumns.Amount]
                    };
                }
            }
            return watering ?? throw new InvalidOperationException($"Watering with ID {id} not found."); ;
        }

        public async Task<int> AddAsync(AddWateringDTO watering)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(WateringQueryManager.InsertWatering, connection);
            command.Parameters.AddWithValue(WateringQueryManager.PlotIdWithAt, watering.PlotId);
            command.Parameters.AddWithValue(WateringQueryManager.DateWithAt, watering.Date);
            command.Parameters.AddWithValue(WateringQueryManager.AmountWithAt, watering.Amount);
            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();
            return result is int id ? id : throw new InvalidOperationException("Failed to retrieve the generated ID.");
        }

        public async Task UpdateAsync(UpdateWateringDTO watering)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(WateringQueryManager.UpdateWatering, connection);
            command.Parameters.AddWithValue(WateringQueryManager.PlotIdWithAt, watering.PlotId);
            command.Parameters.AddWithValue(WateringQueryManager.DateWithAt, watering.Date);
            command.Parameters.AddWithValue(WateringQueryManager.AmountWithAt, watering.Amount);
            command.Parameters.AddWithValue(WateringQueryManager.WateringIdWithAt, watering.WateringId);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Watering with ID {watering.WateringId} not found.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(WateringQueryManager.DateWithAt, connection);
            command.Parameters.AddWithValue(WateringQueryManager.WateringIdWithAt, id);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Watering with ID {id} not found.");
            }
        }
    }
}

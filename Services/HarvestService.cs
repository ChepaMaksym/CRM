using CRM.DTO.Harvest;
using CRM.Models;
using CRM.QueryManagers;
using CRM.QueryManagers.Tables;
using CRM.Services.Interface;
using Microsoft.Data.SqlClient;

namespace CRM.Services
{
    public class HarvestService(string connectionString) : IHarvestService
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<Harvest>> GetAllAsync()
        {
            var harvest = new List<Harvest>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(HarvestQueryManager.GetAllHarvests, connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    harvest.Add(new Harvest
                    {
                        HarvestId = (int)reader[HarvestColumns.HarvestId],
                        PlantId = (int)reader[HarvestColumns.PlantId],
                        Date = (DateTime)reader[HarvestColumns.Date],
                        QuantityKg = (decimal)reader[HarvestColumns.QuantityKg],
                        AverageWeightPerItem = (decimal)reader[HarvestColumns.AverageWeightPerItem],
                        NumberItems = (int)reader[HarvestColumns.NumberItems]
                    });
                }
            }
            return harvest;
        }

        public async Task<Harvest> GetByIdAsync(int id)
        {
            Harvest? harvest = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(HarvestQueryManager.GetHarvestById, connection);
                command.Parameters.AddWithValue(HarvestQueryManager.IdWithAt, id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    harvest = new Harvest
                    {
                        HarvestId = (int)reader[HarvestColumns.HarvestId],
                        PlantId = (int)reader[HarvestColumns.PlantId],
                        Date = (DateTime)reader[HarvestColumns.Date],
                        QuantityKg = (decimal)reader[HarvestColumns.QuantityKg],
                        AverageWeightPerItem = (decimal)reader[HarvestColumns.AverageWeightPerItem],
                        NumberItems = (int)reader[HarvestColumns.NumberItems]
                    };
                }
            }
            return harvest ?? throw new InvalidOperationException($"Harvest with ID {id} not found."); ;
        }

        public async Task<int> AddAsync(AddHarvestDTO harvest)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(HarvestQueryManager.InsertHarvest, connection);
            command.Parameters.AddWithValue(HarvestQueryManager.PlantIdWithAt, harvest.PlantId);
            command.Parameters.AddWithValue(HarvestQueryManager.DateWithAt, harvest.Date);
            command.Parameters.AddWithValue(HarvestQueryManager.QuantityKgWithAt, harvest.QuantityKg);
            command.Parameters.AddWithValue(HarvestQueryManager.AverageWeightPerItemWithAt, harvest.AverageWeightPerItem);
            command.Parameters.AddWithValue(HarvestQueryManager.NumberItemsWithAt, harvest.NumberItems);
            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();
            return result is int id ? id : throw new InvalidOperationException("Failed to retrieve the generated ID.");
        }

        public async Task UpdateAsync(UpdateHarvestDTO harvest)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(HarvestQueryManager.UpdateHarvest, connection);
            command.Parameters.AddWithValue(HarvestQueryManager.PlantIdWithAt, harvest.PlantId);
            command.Parameters.AddWithValue(HarvestQueryManager.DateWithAt, harvest.Date);
            command.Parameters.AddWithValue(HarvestQueryManager.QuantityKgWithAt, harvest.QuantityKg);
            command.Parameters.AddWithValue(HarvestQueryManager.AverageWeightPerItemWithAt, harvest.AverageWeightPerItem);
            command.Parameters.AddWithValue(HarvestQueryManager.NumberItemsWithAt, harvest.NumberItems);
            command.Parameters.AddWithValue(HarvestQueryManager.IdWithAt, harvest.HarvestId);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Harvest with ID {harvest.HarvestId} not found.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(HarvestQueryManager.DeleteHarvest, connection);
            command.Parameters.AddWithValue(HarvestQueryManager.IdWithAt, id);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Harvest with ID {id} not found.");
            }
        }
    }
}

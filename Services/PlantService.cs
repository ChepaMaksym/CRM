using CRM.DTO.Plant;
using CRM.Models;
using CRM.QueryManagers;
using CRM.QueryManagers.Tables;
using CRM.Services.Interface;
using Microsoft.Data.SqlClient;

namespace CRM.Services
{
    public class PlantService(string connectionString) : IPlantService
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<Plant>> GetAllAsync()
        {
            var plants = new List<Plant>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(PlantQueryManager.GetAllPlants, connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    plants.Add(new Plant
                    {
                        PlantId = (int)reader[PlantColumns.PlantId],
                        SeedId = (int)reader[PlantColumns.SeedId],
                        PotId = reader[PlantColumns.PotId] as int?,
                        PlotId = reader[PlantColumns.PlantId] as int?,
                        PlantingDate = (DateTime)reader[PlantColumns.PlantingDate],
                        TransplantDate = reader[PlantColumns.TransplantDate] as DateTime?,
                        HarvestDate = reader[PlantColumns.HarvestDate] as DateTime?
                    });
                }
            }
            return plants;
        }

        public async Task<Plant> GetByIdAsync(int id)
        {
            Plant? plant = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(PlantQueryManager.GetPlantById, connection);
                command.Parameters.AddWithValue(PlantQueryManager.IdWithAt, id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    plant = new Plant
                    {
                        PlantId = (int)reader[PlantColumns.PlantId],
                        SeedId = (int)reader[PlantColumns.SeedId],
                        PotId = reader[PlantColumns.PotId] as int?,
                        PlotId = reader[PlantColumns.PlantId] as int?,
                        PlantingDate = (DateTime)reader[PlantColumns.PlantingDate],
                        TransplantDate = reader[PlantColumns.TransplantDate] as DateTime?,
                        HarvestDate = reader[PlantColumns.HarvestDate] as DateTime?
                    };
                }
            }
            return plant ?? throw new InvalidOperationException($"Plant with ID {id} not found.");
        }

        public async Task<int> AddAsync(AddPlantDTO plant)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(PlantQueryManager.InsertPlant, connection);
            command.Parameters.AddWithValue(PlantQueryManager.SeedIdWithAt, plant.SeedId);
            command.Parameters.AddWithValue(PlantQueryManager.PotIdWithAt, plant.PotId);
            command.Parameters.AddWithValue(PlantQueryManager.PlotIdWithAt, plant.PlotId);
            command.Parameters.AddWithValue(PlantQueryManager.PlantingDateWithAt, plant.PlantingDate);
            command.Parameters.AddWithValue(PlantQueryManager.TransplantDateWithAt, plant.TransplantDate.HasValue ? plant.TransplantDate : DBNull.Value);
            command.Parameters.AddWithValue(PlantQueryManager.HarvestDateWithAt, plant.HarvestDate.HasValue ? plant.HarvestDate : DBNull.Value);
            
            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result is int id ? id : throw new InvalidOperationException("Failed to retrieve the generated ID.");

        }

        public async Task UpdateAsync(UpdatePlantDTO plant)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(PlantQueryManager.UpdatePlant, connection);
            command.Parameters.AddWithValue(PlantQueryManager.SeedIdWithAt, plant.SeedId);
            command.Parameters.AddWithValue(PlantQueryManager.PotIdWithAt, plant.PotId);
            command.Parameters.AddWithValue(PlantQueryManager.PlotIdWithAt, plant.PlotId);
            command.Parameters.AddWithValue(PlantQueryManager.PlantingDateWithAt, plant.PlantingDate);
            command.Parameters.AddWithValue(PlantQueryManager.TransplantDateWithAt, plant.TransplantDate.HasValue ? plant.TransplantDate : DBNull.Value);
            command.Parameters.AddWithValue(PlantQueryManager.HarvestDateWithAt, plant.HarvestDate.HasValue ? plant.HarvestDate : DBNull.Value);
            command.Parameters.AddWithValue(PlantQueryManager.IdWithAt, plant.PlantId);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Plant with ID {plant.PlantId} not found.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(PlantQueryManager.DeletePlant, connection);
            command.Parameters.AddWithValue(PlantQueryManager.IdWithAt, id);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Plant with ID {id} not found.");
            }
        }
    }
}

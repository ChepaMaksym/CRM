using CRM.DTO.Harvest;
using CRM.Models;
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
                var command = new SqlCommand("SELECT * FROM Harvest", connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    harvest.Add(new Harvest
                    {
                        HarvestId = (int)reader["harvest_id"],
                        PlantId = (int)reader["plant_id"],
                        Date = (DateTime)reader["date"],
                        QuantityKg = (decimal)reader["quantity_kg"],
                        AverageWeightPerItem = (decimal)reader["average_weight_per_item"],
                        NumberItems = (int)reader["number_items"]
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
                var command = new SqlCommand("SELECT * FROM Harvest WHERE harvest_id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    harvest = new Harvest
                    {
                        HarvestId = (int)reader["harvest_id"],
                        PlantId = (int)reader["plant_id"],
                        Date = (DateTime)reader["date"],
                        QuantityKg = (decimal)reader["quantity_kg"],
                        AverageWeightPerItem = (decimal)reader["average_weight_per_item"],
                        NumberItems = (int)reader["number_items"]
                    };
                }
            }
            return harvest ?? throw new InvalidOperationException($"Harvest with ID {id} not found."); ;
        }

        public async Task<int> AddAsync(AddHarvestDTO harvest)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(@"INSERT INTO Harvest (plant_id, date, quantity_kg, average_weight_per_item, number_items)
                                   VALUES (@plant_id, @date, @quantity_kg, @average_weight_per_item, @number_items)
                                   SELECT CAST(SCOPE_IDENTITY() AS INT);", connection);
            command.Parameters.AddWithValue("@plant_id", harvest.PlantId);
            command.Parameters.AddWithValue("@date", harvest.Date);
            command.Parameters.AddWithValue("@quantity_kg", harvest.QuantityKg);
            command.Parameters.AddWithValue("@average_weight_per_item", harvest.AverageWeightPerItem);
            command.Parameters.AddWithValue("@number_items", harvest.NumberItems);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result is int id ? id : throw new InvalidOperationException("Failed to retrieve the generated ID.");
        }

        public async Task UpdateAsync(UpdateHarvestDTO harvest)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(@"UPDATE Harvest 
                                   SET plant_id = @plant_id, date = @date, quantity_kg = @quantity_kg, 
                                       average_weight_per_item = @average_weight_per_item, number_items = @number_items 
                                   WHERE harvest_id = @id", connection);
            command.Parameters.AddWithValue("@plant_id", harvest.PlantId);
            command.Parameters.AddWithValue("@date", harvest.Date);
            command.Parameters.AddWithValue("@quantity_kg", harvest.QuantityKg);
            command.Parameters.AddWithValue("@average_weight_per_item", harvest.AverageWeightPerItem);
            command.Parameters.AddWithValue("@number_items", harvest.NumberItems);
            command.Parameters.AddWithValue("@id", harvest.HarvestId);

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
            var command = new SqlCommand("DELETE FROM Harvest WHERE harvest_id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Harvest with ID {id} not found.");
            }
        }
    }
}

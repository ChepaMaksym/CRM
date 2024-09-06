using CRM.DTO.Seed;
using CRM.Models;
using CRM.QueryManagers;
using CRM.QueryManagers.Tables;
using CRM.Services.Interface;
using Microsoft.Data.SqlClient;

namespace CRM.Services
{
    public class SeedService(string connectionString) : ISeedService
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<Seed>> GetAllAsync()
        {
            var seeds = new List<Seed>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(SeedQueryManager.GetAllSeeds, connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    seeds.Add(new Seed
                    {
                        SeedId = (int)reader[SeedColumns.SeedId],
                        Name = (string)reader[SeedColumns.Name],
                        Cost = (decimal)reader[SeedColumns.Cost]
                    });
                }
            }
            return seeds;
        }

        public async Task<Seed> GetByIdAsync(int id)
        {
            Seed? seed = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(SeedQueryManager.GetSeedById, connection);
                command.Parameters.AddWithValue(SeedQueryManager.SeedIdWithAt, id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    seed = new Seed
                    {
                        SeedId = (int)reader[SeedColumns.SeedId],
                        Name = (string)reader[SeedColumns.Name],
                        Cost = (decimal)reader[SeedColumns.Cost]
                    };
                }
            }
            return seed ?? throw new InvalidOperationException($"Seed with ID {id} not found."); ;
        }

        public async Task<int> AddAsync(AddSeedDTO seed)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(SeedQueryManager.InsertSeed, connection);
            command.Parameters.AddWithValue(SeedQueryManager.NameWithAt, seed.Name);
            command.Parameters.AddWithValue(SeedQueryManager.CostWithAt, seed.Cost);
            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();
            return result is int id ? id : throw new InvalidOperationException("Failed to retrieve the generated ID.");
        }

        public async Task UpdateAsync(UpdateSeedDTO seed)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(SeedQueryManager.UpdateSeed, connection);
            command.Parameters.AddWithValue(SeedQueryManager.NameWithAt, seed.Name);
            command.Parameters.AddWithValue(SeedQueryManager.CostWithAt, seed.Cost);
            command.Parameters.AddWithValue(SeedQueryManager.SeedIdWithAt, seed.SeedId);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Seed with ID {seed.SeedId} not found.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(SeedQueryManager.DeleteSeed, connection);
            command.Parameters.AddWithValue(SeedQueryManager.SeedIdWithAt, id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}

using CRM.DTO.Pot;
using CRM.Models;
using CRM.QueryManagers;
using CRM.QueryManagers.Tables;
using CRM.Services.Interface;
using Microsoft.Data.SqlClient;

namespace CRM.Services
{
    public class PotService(string connectionString) : IPotService
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<Pot>> GetAllAsync()
        {
            var pots = new List<Pot>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(PotQueryManager.GetAllPots, connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    pots.Add(new Pot
                    {
                        PotId = (int)reader[PotColumns.PotId],
                        SoilTypeId = (int)reader[PotColumns.SoilTypeId],
                        Type = (string)reader[PotColumns.Type]
                    });
                }
            }
            return pots;
        }

        public async Task<Pot> GetByIdAsync(int id)
        {
            Pot? pot = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(PotQueryManager.GetPotById, connection);
                command.Parameters.AddWithValue(PotQueryManager.PotIdWithAt, id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    pot = new Pot
                    {
                        PotId = (int)reader[PotColumns.PotId],
                        SoilTypeId = (int)reader[PotColumns.SoilTypeId],
                        Type = (string)reader[PotColumns.Type]
                    };
                }
            }
            return pot ?? throw new InvalidOperationException($"Pot with ID {id} not found."); ;
        }

        public async Task<int> AddAsync(AddPotDTO pot)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(PotQueryManager.InsertPot, connection);
            command.Parameters.AddWithValue(PotQueryManager.SoilTypeIdWithAt, pot.SoilTypeId);
            command.Parameters.AddWithValue(PotQueryManager.TypeWithAt, pot.Type);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result is int id ? id : throw new InvalidOperationException("Failed to retrieve the generated ID.");
        }

        public async Task UpdateAsync(UpdatePotDTO pot)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(PotQueryManager.UpdatePot, connection);
            command.Parameters.AddWithValue(PotQueryManager.SoilTypeIdWithAt, pot.SoilTypeId);
            command.Parameters.AddWithValue(PotQueryManager.TypeWithAt, pot.Type);
            command.Parameters.AddWithValue(PotQueryManager.PotIdWithAt, pot.PotId);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Pot with ID {pot.PotId} not found.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(PotQueryManager.DeletePot, connection);
            command.Parameters.AddWithValue(PotQueryManager.PotIdWithAt, id);
            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Pot with ID {id} not found.");
            }
        }
    }
}

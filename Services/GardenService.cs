using CRM.DTO.Garden;
using CRM.Models;
using CRM.Services.Interface;
using Microsoft.Data.SqlClient;

namespace CRM.Services
{
    public class GardenService(string connectionString) : IGardenService
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<Garden>> GetAllAsync()
        {
            var gardens = new List<Garden>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Gardens", connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    gardens.Add(new Garden
                    {
                        GardenId = (int)reader["garden_id"],
                        Size = (decimal)reader["size"]
                    });
                }
            }
            return gardens;
        }

        public async Task<Garden> GetByIdAsync(int id)
        {
            Garden? garden = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Gardens WHERE garden_id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    garden = new Garden
                    {
                        GardenId = (int)reader["garden_id"],
                        Size = (decimal)reader["size"]
                    };
                }
            }
            return garden ?? throw new InvalidOperationException($"Garden with ID {id} not found."); ;
        }

        public async Task<int> AddAsync(AddGardenDTO  garden)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(@"
                            INSERT INTO Gardens (size)
                            VALUES (@size)
                            SELECT CAST(SCOPE_IDENTITY() AS INT);", connection);
            command.Parameters.AddWithValue("@size", garden.Size);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result is int id ? id : throw new InvalidOperationException("Failed to retrieve the generated ID.");
        }

        public async Task UpdateAsync(UpdateGardenDTO garden)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("UPDATE Gardens SET size = @size WHERE garden_id = @id", connection);
            command.Parameters.AddWithValue("@size", garden.Size);
            command.Parameters.AddWithValue("@id", garden.GardenId);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();


            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Garden with ID {garden.GardenId} not found.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("DELETE FROM Gardens WHERE garden_id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Garden with ID {id} not found.");
            }
        }
    }
}

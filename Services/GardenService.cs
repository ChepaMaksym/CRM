using CRM.DTO.Garden;
using CRM.Models;
using CRM.QueryManagers;
using CRM.QueryManagers.Tables;
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
                var command = new SqlCommand(GardenQueryManager.GetAllGardens, connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    gardens.Add(new Garden
                    {
                        GardenId = (int)reader[GardenColumns.GardenId],
                        Size = (decimal)reader[GardenColumns.Size]
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
                var command = new SqlCommand(GardenQueryManager.GetGardenById, connection);
                command.Parameters.AddWithValue(GardenQueryManager.IdWithAt, id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    garden = new Garden
                    {
                        GardenId = (int)reader[GardenColumns.GardenId],
                        Size = (decimal)reader[GardenColumns.Size]
                    };
                }
            }
            return garden ?? throw new InvalidOperationException($"Garden with ID {id} not found."); ;
        }

        public async Task<int> AddAsync(AddGardenDTO  garden)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(GardenQueryManager.InsertGarden, connection);
            command.Parameters.AddWithValue(GardenQueryManager.SizeWithAt, garden.Size);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return result is int id ? id : throw new InvalidOperationException("Failed to retrieve the generated ID.");
        }

        public async Task UpdateAsync(UpdateGardenDTO garden)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(GardenQueryManager.UpdateGarden, connection);
            command.Parameters.AddWithValue(GardenQueryManager.SizeWithAt, garden.Size);
            command.Parameters.AddWithValue(GardenQueryManager.IdWithAt, garden.GardenId);
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
            var command = new SqlCommand(GardenQueryManager.DeleteGarden, connection);
            command.Parameters.AddWithValue(GardenQueryManager.IdWithAt, id);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Garden with ID {id} not found.");
            }
        }
    }
}

using CRM.DTO.SoilType;
using CRM.Models;
using CRM.QueryManagers;
using CRM.QueryManagers.Tables;
using CRM.Services.Interface;
using Microsoft.Data.SqlClient;

namespace CRM.Services
{
    public class SoilTypeService(string connectionString) : ISoilTypeService
    {
        private readonly string _connectionString = connectionString;

        public async Task<IEnumerable<SoilType>> GetAllAsync()
        {
            var soilTypes = new List<SoilType>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(SoilTypeQueryManager.GetAllSoilTypes, connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    soilTypes.Add(new SoilType
                    {
                        SoilTypeId = (int)reader[SoilTypeColumns.SoilTypeId],
                        SoilTypeName = (string)reader[SoilTypeColumns.SoilType]
                    });
                }
            }
            return soilTypes;
        }

        public async Task<SoilType> GetByIdAsync(int id)
        {
            SoilType? soilType = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(SoilTypeQueryManager.GetSoilTypeById, connection);
                command.Parameters.AddWithValue(SoilTypeQueryManager.SoilTypeIdWithAt, id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    soilType = new SoilType
                    {
                        SoilTypeId = (int)reader[SoilTypeColumns.SoilTypeId],
                        SoilTypeName = (string)reader[SoilTypeColumns.SoilType]
                    };
                }
            }
            return soilType ?? throw new InvalidOperationException($"SoilType with ID {id} not found."); ;
        }

        public async Task<int> AddAsync(AddSoilTypeDTO soilType)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(SoilTypeQueryManager.InsertSoilType, connection);
            command.Parameters.AddWithValue(SoilTypeQueryManager.SoilTypeNameWithAt, soilType.SoilTypeName);
            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();
            return result is int id ? id : throw new InvalidOperationException("Failed to retrieve the generated ID.");
        }

        public async Task UpdateAsync(UpdateSoilTypeDTO soilType)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(SoilTypeQueryManager.UpdateSoilType, connection);
            command.Parameters.AddWithValue(SoilTypeQueryManager.SoilTypeNameWithAt, soilType.SoilTypeName);
            command.Parameters.AddWithValue(SoilTypeQueryManager.SoilTypeIdWithAt, soilType.SoilTypeId);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Soil type with ID {soilType.SoilTypeId} not found.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(SoilTypeQueryManager.DeleteSoilType, connection);
            command.Parameters.AddWithValue(SoilTypeQueryManager.SoilTypeIdWithAt, id);
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException($"Soil type with ID {id} not found.");
            }
        }
    }
}

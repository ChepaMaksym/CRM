using CRM.QueryManagers.Tables;

namespace CRM.QueryManagers
{
    public static class SoilTypeQueryManager
    {
        public const string SoilTypeIdWithAt = $"@{SoilTypeColumns.SoilTypeId}";
        public const string SoilTypeNameWithAt = $"@{SoilTypeColumns.SoilType}";

        public const string GetAllSoilTypes = $"SELECT * FROM {SoilTypeColumns.TableName}";

        public const string GetSoilTypeById = $@"
            SELECT * FROM {SoilTypeColumns.TableName} 
            WHERE {SoilTypeColumns.SoilTypeId} = @{SoilTypeColumns.SoilTypeId}";

        public const string InsertSoilType = $@"
            INSERT INTO {SoilTypeColumns.TableName} 
            ({SoilTypeColumns.SoilType})
            VALUES (@{SoilTypeColumns.SoilType})
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string UpdateSoilType = $@"
            UPDATE {SoilTypeColumns.TableName}
            SET {SoilTypeColumns.SoilType} = @{SoilTypeColumns.SoilType}
            WHERE {SoilTypeColumns.SoilTypeId} = @{SoilTypeColumns.SoilTypeId}";

        public const string DeleteSoilType = $@"
            DELETE FROM {SoilTypeColumns.TableName} 
            WHERE {SoilTypeColumns.SoilTypeId} = @{SoilTypeColumns.SoilTypeId}";
    }

}

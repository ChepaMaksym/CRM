using CRM.QueryManagers.Tables;

namespace CRM.QueryManagers
{
    public static class SeedQueryManager
    {

        public const string SeedIdWithAt = $"@{SeedColumns.SeedId}";
        public const string NameWithAt = $"@{SeedColumns.Name}";
        public const string CostWithAt = $"@{SeedColumns.Cost}";

        public const string GetAllSeeds = $"SELECT * FROM {SeedColumns.TableName}";

        public const string GetSeedById = $@"
            SELECT * FROM {SeedColumns.TableName}
            WHERE {SeedColumns.SeedId} = @{SeedColumns.SeedId}";

        public const string InsertSeed = $@"
            INSERT INTO {SeedColumns.TableName} 
            ({SeedColumns.Name}, {SeedColumns.Cost})
            VALUES (@{SeedColumns.Name}, @{SeedColumns.Cost})
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string UpdateSeed = $@"
            UPDATE {SeedColumns.TableName}
            SET {SeedColumns.Name} = @{SeedColumns.Name}, 
                {SeedColumns.Cost} = @{SeedColumns.Cost}
            WHERE {SeedColumns.SeedId} = @{SeedColumns.SeedId}";

        public const string DeleteSeed = $@"
            DELETE FROM {SeedColumns.TableName}
            WHERE {SeedColumns.SeedId} = @{SeedColumns.SeedId}";

    }
}

using CRM.QueryManagers.Tables;

namespace CRM.QueryManagers
{
    public static class PotQueryManager
    {

        public const string PotIdWithAt = $"@{PotColumns.PotId}";
        public const string SoilTypeIdWithAt = $"@{PotColumns.SoilTypeId}";
        public const string TypeWithAt = $"@{PotColumns.Type}";

        public const string GetAllPots = $"SELECT * FROM {PotColumns.TableName}";

        public const string GetPotById = $@"
            SELECT * FROM {PotColumns.TableName}
            WHERE {PotColumns.PotId} = @{PotColumns.PotId}";
        public const string InsertPot = $@"
            INSERT INTO {PotColumns.TableName} 
            ({PotColumns.SoilTypeId}, {PotColumns.Type})
            VALUES (@{PotColumns.SoilTypeId}, @{PotColumns.Type})
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string UpdatePot = $@"
            UPDATE {PotColumns.TableName}
            SET {PotColumns.SoilTypeId} = @{PotColumns.SoilTypeId}, 
                {PotColumns.Type} = @{PotColumns.Type}
            WHERE {PotColumns.PotId} = @{PotColumns.PotId}";

        public const string DeletePot = $@"
            DELETE FROM {PotColumns.TableName}
            WHERE {PotColumns.PotId} = @{PotColumns.PotId}";
    }
}

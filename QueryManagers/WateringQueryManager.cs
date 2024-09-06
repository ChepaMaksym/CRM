using CRM.QueryManagers.Tables;

namespace CRM.QueryManagers
{
    public static class WateringQueryManager
    {
        public const string WateringIdWithAt = $"@{WateringColumns.WateringId}";
        public const string PlotIdWithAt = $"@{WateringColumns.PlotId}";
        public const string DateWithAt = $"@{WateringColumns.Date}";
        public const string AmountWithAt = $"@{WateringColumns.Amount}";

        public const string GetAllWaterings = $"SELECT * FROM {WateringColumns.TableName}";
        public const string GetWateringById = $@"
            SELECT * FROM {WateringColumns.TableName}
            WHERE {WateringColumns.WateringId} = @{WateringColumns.WateringId}";

        public const string InsertWatering = $@"
            INSERT INTO {WateringColumns.TableName} 
            ({WateringColumns.PlotId}, {WateringColumns.Date}, {WateringColumns.Amount})
            VALUES (@{WateringColumns.PlotId}, @{WateringColumns.Date}, @{WateringColumns.Amount})
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string UpdateWatering = $@"
            UPDATE {WateringColumns.TableName}
            SET {WateringColumns.PlotId} = @{WateringColumns.PlotId}, 
                {WateringColumns.Date} = @{WateringColumns.Date}, 
                {WateringColumns.Amount} = @{WateringColumns.Amount}
            WHERE {WateringColumns.WateringId} = @{WateringColumns.WateringId}";

        public const string DeleteWatering = $@"
            DELETE FROM {WateringColumns.TableName}
            WHERE {WateringColumns.WateringId} = @{WateringColumns.WateringId}";
    }
}

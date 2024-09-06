using CRM.QueryManagers.Tables;

namespace CRM.QueryManagers
{
    public static class GardenQueryManager
    {
        public const string IdWithAt = $"@{GardenColumns.GardenId}";
        public const string SizeWithAt = $"@{GardenColumns.Size}";

        public const string GetAllGardens = $"SELECT * FROM {GardenColumns.TableName}";

        public const string GetGardenById = $@"
        SELECT * FROM {GardenColumns.TableName} 
        WHERE {GardenColumns.GardenId} = {IdWithAt}";

        public const string InsertGarden = $@"
        INSERT INTO {GardenColumns.TableName} ({GardenColumns.Size}) 
        VALUES ({SizeWithAt})
        SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string UpdateGarden = $@"
        UPDATE {GardenColumns.TableName}
        SET {GardenColumns.Size} = {SizeWithAt} 
        WHERE {GardenColumns.GardenId} = {IdWithAt}";

        public const string DeleteGarden = $@"
        DELETE FROM {GardenColumns.TableName} 
        WHERE {GardenColumns.GardenId} = {IdWithAt}";
    }

}

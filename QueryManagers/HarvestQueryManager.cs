using CRM.QueryManagers.Tables;

namespace CRM.QueryManagers
{
    public static class HarvestQueryManager
    {
        public const string IdWithAt = $"@{HarvestColumns.HarvestId}";
        public const string PlantIdWithAt = $"@{HarvestColumns.PlantId}";
        public const string DateWithAt = $"@{HarvestColumns.Date}";
        public const string QuantityKgWithAt = $"@{HarvestColumns.QuantityKg}";
        public const string AverageWeightPerItemWithAt = $"@{HarvestColumns.AverageWeightPerItem}";
        public const string NumberItemsWithAt = $"@{HarvestColumns.NumberItems}";

        public const string GetAllHarvests = $"SELECT * FROM {HarvestColumns.TableName}";

        public const string GetHarvestById = $@"
        SELECT * FROM {HarvestColumns.TableName} 
        WHERE {HarvestColumns.HarvestId} = {IdWithAt}";

        public const string InsertHarvest = $@"
        INSERT INTO {HarvestColumns.TableName} (
            {HarvestColumns.PlantId}, 
            {HarvestColumns.Date}, 
            {HarvestColumns.QuantityKg}, 
            {HarvestColumns.AverageWeightPerItem}, 
            {HarvestColumns.NumberItems})
        VALUES (
            {PlantIdWithAt}, 
            {DateWithAt}, 
            {QuantityKgWithAt}, 
            {AverageWeightPerItemWithAt}, 
            {NumberItemsWithAt});
        SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string UpdateHarvest = $@"
        UPDATE {HarvestColumns.TableName}
        SET {HarvestColumns.PlantId} = {PlantIdWithAt}, 
            {HarvestColumns.Date} = {DateWithAt}, 
            {HarvestColumns.QuantityKg} = {QuantityKgWithAt}, 
            {HarvestColumns.AverageWeightPerItem} = {AverageWeightPerItemWithAt}, 
            {HarvestColumns.NumberItems} = {NumberItemsWithAt}
        WHERE {HarvestColumns.HarvestId} = {IdWithAt}";

        public const string DeleteHarvest = $@"
        DELETE FROM {HarvestColumns.TableName}
        WHERE {HarvestColumns.HarvestId} = {IdWithAt}";
    }

}

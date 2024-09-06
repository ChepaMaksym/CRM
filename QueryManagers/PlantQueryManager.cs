using CRM.QueryManagers.Tables;

namespace CRM.QueryManagers
{
    public static class PlantQueryManager
    {
        public const string IdWithAt = $"@{PlantColumns.PlantId}";
        public const string SeedIdWithAt = $"@{PlantColumns.SeedId}";
        public const string PotIdWithAt = $"@{PlantColumns.PotId}";
        public const string PlotIdWithAt = $"@{PlantColumns.PlotId}";
        public const string PlantingDateWithAt = $"@{PlantColumns.PlantingDate}";
        public const string TransplantDateWithAt = $"@{PlantColumns.TransplantDate}";
        public const string HarvestDateWithAt = $"@{PlantColumns.HarvestDate}";

        public const string GetAllPlants = $"SELECT * FROM {PlantColumns.TableName}";

        public const string GetPlantById = $@"
            SELECT * FROM {PlantColumns.TableName} 
            WHERE {PlantColumns.PlantId} = {IdWithAt}";

        public const string InsertPlant = $@"
            INSERT INTO {PlantColumns.TableName} 
            ({PlantColumns.SeedId}, {PlantColumns.PotId}, {PlantColumns.PlotId}, 
             {PlantColumns.PlantingDate}, {PlantColumns.TransplantDate}, {PlantColumns.HarvestDate}) 
            VALUES 
            ({SeedIdWithAt}, {PotIdWithAt}, {PlotIdWithAt}, 
             {PlantingDateWithAt}, {TransplantDateWithAt}, {HarvestDateWithAt})
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string UpdatePlant = $@"
            UPDATE {PlantColumns.TableName}
            SET 
                {PlantColumns.SeedId} = {SeedIdWithAt}, 
                {PlantColumns.PotId} = {PotIdWithAt}, 
                {PlantColumns.PlotId} = {PlotIdWithAt}, 
                {PlantColumns.PlantingDate} = {PlantingDateWithAt}, 
                {PlantColumns.TransplantDate} = {TransplantDateWithAt}, 
                {PlantColumns.HarvestDate} = {HarvestDateWithAt}
            WHERE {PlantColumns.PlantId} = {IdWithAt}";

        public const string DeletePlant = $@"
            DELETE FROM {PlantColumns.TableName} 
            WHERE {PlantColumns.PlantId} = {IdWithAt}";
    }
}

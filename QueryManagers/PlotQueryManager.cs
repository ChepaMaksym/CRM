using CRM.QueryManagers.Tables;

namespace CRM.QueryManagers
{
    public static class PlotQueryManager
    {
        public const string PlotIdWithAt = $"@{PlotColumns.PlotId}";
        public const string GardenIdWithAt = $"@{PlotColumns.GardenId}";
        public const string SoilTypeIdWithAt = $"@{PlotColumns.SoilTypeId}";
        public const string NameWithAt = $"@{PlotColumns.Name}";
        public const string SizeWithAt = $"@{PlotColumns.Size}";

        public const string GetAllPlots = $"SELECT * FROM {PlotColumns.TableName}";

        public const string GetPlotById = $@"
            SELECT * FROM {PlotColumns.TableName}
            WHERE {PlotColumns.PlotId} = @{PlotColumns.PlotId}";

        public const string InsertPlot = $@"
            INSERT INTO {PlotColumns.TableName} 
            ({PlotColumns.GardenId}, {PlotColumns.SoilTypeId}, {PlotColumns.Name}, {PlotColumns.Size})
            VALUES (@{PlotColumns.GardenId}, @{PlotColumns.SoilTypeId}, @{PlotColumns.Name}, @{PlotColumns.Size})
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

        public const string UpdatePlot = $@"
            UPDATE {PlotColumns.TableName}
            SET {PlotColumns.GardenId} = @{PlotColumns.GardenId}, 
                {PlotColumns.SoilTypeId} = @{PlotColumns.SoilTypeId}, 
                {PlotColumns.Name} = @{PlotColumns.Name}, 
                {PlotColumns.Size} = @{PlotColumns.Size}
            WHERE {PlotColumns.PlotId} = @{PlotColumns.PlotId}";

        public const string DeletePlot = $@"
            DELETE FROM {PlotColumns.TableName}
            WHERE {PlotColumns.PlotId} = @{PlotColumns.PlotId}";
    }

}

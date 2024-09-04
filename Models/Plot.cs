namespace CRM.Models
{
    public class Plot : AgroBase
    {
        public int PlotId { get; set; }
        public int GardenId { get; set; }
        public int SoilTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Size { get; set; }
    }
}

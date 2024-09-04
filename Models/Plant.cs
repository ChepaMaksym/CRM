namespace CRM.Models
{
    public class Plant : AgroBase
    {
        public int PlantId { get; set; }
        public int SeedId { get; set; }
        public int? PotId { get; set; }
        public int? PlotId { get; set; }
        public DateTime PlantingDate { get; set; }
        public DateTime? TransplantDate { get; set; }
        public DateTime? HarvestDate { get; set; }
    }
}

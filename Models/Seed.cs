namespace CRM.Models
{
    public class Seed : AgroBase
    {
        public int SeedId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Cost { get; set; }
    }
}

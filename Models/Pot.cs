namespace CRM.Models
{
    public class Pot : AgroBase
    {
        public int PotId { get; set; }
        public int SoilTypeId { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}

namespace CRM.Models
{
    public class Watering : AgroBase
    {
        public int WateringId { get; set; }
        public int PlotId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}

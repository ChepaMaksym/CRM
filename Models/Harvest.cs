using CRM.DTO.Harvest;

namespace CRM.Models
{
    public class Harvest : AgroBase
    {
        public int HarvestId { get; set; }
        public int PlantId { get; set; }
        public DateTime Date { get; set; }
        public decimal QuantityKg { get; set; }
        public decimal AverageWeightPerItem { get; set; }
        public int NumberItems { get; set; }
        public Harvest() { }
        public Harvest(int id, AddHarvestDTO addHarvest)
        {
            HarvestId = id;
            PlantId = addHarvest.PlantId;
            Date = addHarvest.Date;
            QuantityKg = addHarvest.QuantityKg;
            AverageWeightPerItem = addHarvest.AverageWeightPerItem;
            NumberItems = addHarvest.NumberItems;
        }
    }
}

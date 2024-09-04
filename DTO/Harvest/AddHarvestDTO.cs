using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Harvest
{
    public class AddHarvestDTO
    {
        [Required]
        public int PlantId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public decimal QuantityKg { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Average weight per item must be greater than 0.")]
        public decimal AverageWeightPerItem { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of items must be greater than 0.")]
        public int NumberItems { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Watering
{
    public class UpdateWateringDTO
    {
        [Required]
        public int WateringId { get; set; }

        [Required]
        public int PlotId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public double Amount { get; set; }
    }
}

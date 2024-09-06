using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Watering
{
    public class AddWateringDTO
    {
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

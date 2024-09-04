using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Plot
{
    public class UpdatePlotDTO
    {
        [Required]
        public int PlotId { get; set; }

        [Required]
        public int GardenId { get; set; }

        [Required]
        public int SoilTypeId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Size must be greater than 0.")]
        public int Size { get; set; }
    }
}

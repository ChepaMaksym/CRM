using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Plant
{
    public class UpdatePlantDTO
    {
        [Required]
        public int PlantId { get; set; }

        [Required]
        public int SeedId { get; set; }

        [Required]
        public int PotId { get; set; }

        [Required]
        public int PlotId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PlantingDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? TransplantDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? HarvestDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.SoilType
{
    public class UpdateSoilTypeDTO
    {
        [Required]
        public int SoilTypeId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}

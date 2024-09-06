using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.SoilType
{
    public class UpdateSoilTypeDTO
    {
        [Required]
        public int SoilTypeId { get; set; }

        [Required]
        [Range(5, 50, ErrorMessage = "Type length must be between 5 and 50 characters.")]
        public string SoilTypeName { get; set; } = string.Empty;
    }
}

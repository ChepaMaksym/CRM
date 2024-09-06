using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Pot
{
    public class UpdatePotDTO
    {
        [Required]
        public int PotId { get; set; }

        [Required]
        [Range(5, 50, ErrorMessage = "Type length must be between 5 and 50 characters.")]
        public string Type { get; set; } = string.Empty;
        [Required]
        public int SoilTypeId { get; set; }
    }
}

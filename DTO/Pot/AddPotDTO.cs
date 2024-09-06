using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Pot
{
    public class AddPotDTO
    {
        [Required]
        [Range(5, 50, ErrorMessage = "Type length must be between 5 and 50 characters.")]
        public string Type { get; set; }
        [Required]
        public int SoilTypeId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Seed
{
    public class UpdateSeedDTO
    {
        [Required]
        public int SeedId { get; set; }

        [Required]
        [Range(5, 50, ErrorMessage = "Name length must be between 5 and 50 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Cost { get; set; }
    }
}

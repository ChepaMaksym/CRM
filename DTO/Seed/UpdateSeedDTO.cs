using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Seed
{
    public class UpdateSeedDTO
    {
        [Required]
        public int SeedId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

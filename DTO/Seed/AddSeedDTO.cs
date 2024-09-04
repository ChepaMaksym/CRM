using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Seed
{
    public class AddSeedDTO
    {
        [Required]
        public string Name { get; set; }
    }
}

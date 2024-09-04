using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Pot
{
    public class UpdatePotDTO
    {
        [Required]
        public int PotId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}

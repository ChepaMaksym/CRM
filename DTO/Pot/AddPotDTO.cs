using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Pot
{
    public class AddPotDTO
    {
        [Required]
        public string Type { get; set; }
    }
}

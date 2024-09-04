using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.Garden
{
    public class AddGardenDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Size must be greater than 0.")]
        public int Size { get; set; }
    }
}

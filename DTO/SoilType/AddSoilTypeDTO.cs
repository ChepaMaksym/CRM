using System.ComponentModel.DataAnnotations;

namespace CRM.DTO.SoilType
{
    public class AddSoilTypeDTO
    {
        [Required]
        public string Type { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class CityDTO
    {
        public int ID { get; set; }
        [Required (ErrorMessage = "Name is a mandatory field")]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Only numerics are not allowed")]
        public string Name { get; set; }
        [Required]
        public string? Country { get; set; }
    }
}
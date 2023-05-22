using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Models.Dto
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Category { get; set; }

        [Required]
        public double Price { get; set; }
    }
}

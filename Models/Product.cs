using System.ComponentModel.DataAnnotations;
using TamansShop.Areas.Identity.Data;

namespace TamansShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string ImagePath { get; set; }
        
        [Required]
        public string UserId { get; set; }  // User's Id who added the product



    }
}
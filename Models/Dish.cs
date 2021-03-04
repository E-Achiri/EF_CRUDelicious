using System;
using System.ComponentModel.DataAnnotations;
namespace EF_CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }


        [Required]
        public string Name { get; set; }


        [Required]
        public string ChefName { get; set; }

        [Required]
        public int Tastiness { get; set; }

        [Required]
        [Range(1,5)]
        public int Calories { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
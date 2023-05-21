using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class FoodDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsVegetarian { get; set; }
        [Required]
        public bool IsLactoseFree { get; set; }
        [Required]
        public bool IsNutFree { get; set; }
        [Required]
        public bool IsAlcoholFree { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GameNightId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class GameNight
    {
        [Key]
        public int Id { get; set; }
        public WebsiteUser? Organizer { get; set; }
        public Address? Address { get; set; }
        [Required]
        public int MaxNumberOfPlayers { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public bool Is18Plus { get; set; }

        public bool VegetarianSnacks { get; set; }
        public bool LactoseFreeSnacks { get; set; }
        public bool NutFreeSnacks { get; set; }
        public bool AlcoholFreeDrinks { get; set; }
        public ICollection<WebsiteUser>? Participants { get; set; }
        
        public ICollection<BoardGame>? Games { get; set; }

        public ICollection<Food>? Food { get; set; }

    }
}

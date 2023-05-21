using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BoardGame
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Is18Plus { get; set; }
        
        [JsonIgnore]
        public WebsiteUser? User { get; set; }
        [Required]
        public string TypeOfGame { get; set; }
        [Required]
        public GameGenres Genre { get; set; }
        [JsonIgnore]
        public ICollection<GameNight>? GameNights { get; set; }

        public byte[]? Picture { get; set; }

    }
}

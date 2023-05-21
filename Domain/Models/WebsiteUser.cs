using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class WebsiteUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool IsVegetarian { get; set; }
        public bool NutAllergy { get; set; }
        public bool  LactoseIntolerant { get; set; }
        public bool AlcoholFree { get; set; }
        [JsonIgnore]
        public ICollection<GameNight> GameNight { get; set; }


    }
}

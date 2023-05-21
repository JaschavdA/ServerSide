using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Domain.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAddedToGameNight { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsLactoseFree { get; set; }
        public bool IsNutFree { get; set; }
        public bool IsAlcoholFree { get; set; }
        [JsonIgnore]
        public WebsiteUser User { get; set; }
        [JsonIgnore]
        public GameNight GameNight { get; set; }



    }
}

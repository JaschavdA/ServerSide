using Domain.Models;
using DomainServices.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BordspelAPI.Controllers
{

    [Route("REST/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {

        private readonly IWebsiteUserRepo websiteUserRepo;

        public FoodController(IWebsiteUserRepo websiteUserRepo)
        {
            this.websiteUserRepo = websiteUserRepo;
        }

        [HttpPost]
        public ActionResult<Food> Post(FoodDTO food)
        {

            
            Food foodToAdd = new Food()
            {
                Name = food.Name,
                IsVegetarian = food.IsVegetarian,
                IsNutFree = food.IsNutFree,
                IsLactoseFree = food.IsLactoseFree,
                IsAlcoholFree = food.IsAlcoholFree,
                IsAddedToGameNight = false,
                User = websiteUserRepo.GetWebsiteUserById(food.UserId)
        };
            try
            {
                websiteUserRepo.AddFoodOfUserToGameNight(food.GameNightId, foodToAdd);
                return Ok(foodToAdd);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

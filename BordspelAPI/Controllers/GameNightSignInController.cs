using System.Net;
using Domain.Models;
using DomainServices.IRepo;
using Microsoft.AspNetCore.Mvc;

namespace BordspelAPI.Controllers
{
    [ApiController]
    [Route("REST/[controller]")]
    public class GameNightSignInController : ControllerBase
    {

        private readonly IGameNightRepo _gameNightRepo;
        private readonly IWebsiteUserRepo _websiteUserRepo;


        public GameNightSignInController(IGameNightRepo gameNightRepo, IWebsiteUserRepo websiteUserRepo)
        {
            _gameNightRepo = gameNightRepo;
            _websiteUserRepo = websiteUserRepo;
        }

        [HttpPost]
        public ActionResult<GameNight> Post(GameNightSignInDTO signInData)
        {
            var user = _websiteUserRepo.GetWebsiteUserById(signInData.UserId);
            var gameNight = _gameNightRepo.GetGameNightById(signInData.GameNightId);
            var foodOfUser = _websiteUserRepo.GetFoodOfUserInGameNight(user.Email, gameNight.Id);
            if (foodOfUser.Count() > 0)
            {
                try
                {
                    _websiteUserRepo.SignUpGameNight(signInData.GameNightId, user);
                    return Ok(gameNight);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("De gebruiker moet minimaal een snack meenemen naar de spelavond, deze kun je toevoegen met een post op Food");
        }

    }
}
using Bordspelavond.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Bordspelavond.Data;
using Bordspelavond.IdentityObject;
using Domain;
using Domain.Models;
using DomainServices.IRepo;
using Microsoft.AspNetCore.Identity;


namespace Bordspelavond.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebsiteUserRepo _websiteUserRepo;
        private readonly IGameNightRepo _gameNightRepo;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, IWebsiteUserRepo websiteUserRepo, IGameNightRepo gameNightRepo)
        {
            _logger = logger;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._websiteUserRepo = websiteUserRepo;
            this._gameNightRepo = gameNightRepo;
        }
        public IActionResult Index()
        {
            IEnumerable<GameNight> gameNights = new List<GameNight>();
            if (_signInManager.IsSignedIn(User))
            {
                var user = _websiteUserRepo.GetWebsiteUserByEmail(_userManager.GetUserAsync(this.User).Result.Email);
                ViewBag.SignedUpGameGameNights = _websiteUserRepo.GetSignedUpGameNights(user.Email);
                ViewBag.WebUser = _websiteUserRepo.GetWebsiteUserByEmail(user.Email);
                gameNights =_gameNightRepo.GetAllGameNightsInFutureWhereNotOrganizerOrSignedUp(user);
                if (User.HasClaim("EighteenPlus", "EighteenPlus"))
                {
                    ViewBag.OrganizedGameNights = _websiteUserRepo.GetOrganizedGameNights(user.Email);
                    
                }
                else
                {
                    gameNights =
                        _gameNightRepo
                            .GetAllGameNightsInFutureWhereNotOrganizerOrSignedUpAndGameNightIsNot18Plus(user);
                }
            }
            else
            {
                gameNights = _gameNightRepo.GetAllGameNightsInFuture();
            }


            return View(gameNights);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
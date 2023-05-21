using Bordspelavond.IdentityObject;
using Domain.Models;
using DomainServices.IRepo;
using DomainServices.Logic;
using DomainServices.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;





namespace Bordspelavond.Controllers

{
    public class GameNightController : Controller
    {
        private readonly IGameNightRepo repo;
        private readonly IBoardGameRepo boardGameRepo;
        private readonly IWebsiteUserRepo websiteUserRepo;
        private readonly ILogger<GameNightController> logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public GameNightController(IGameNightRepo repo, IBoardGameRepo boardGameRepo,
            ILogger<GameNightController> logger, UserManager<AppUser> _userManager, IWebsiteUserRepo websiteUserRepo, SignInManager<AppUser> signInManager)
        {
            this.repo = repo;
            this.boardGameRepo = boardGameRepo;
            this.logger = logger;
            this._userManager = _userManager;
            this.websiteUserRepo = websiteUserRepo;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var gameNights = websiteUserRepo.GetSignedUpGameNights(_userManager.GetUserAsync(this.User).Result.Email);
            return View(gameNights);
        }

        
        public IActionResult AvailableGameNights()
        {
            IEnumerable<GameNight> gameNights = null;
            if (_signInManager.IsSignedIn(this.User))
            {
                var webUser = websiteUserRepo.GetWebsiteUserByEmail(_userManager.GetUserAsync(this.User).Result.Email);
                ViewBag.WebUser = webUser;
                if (User.HasClaim("EighteenPlus", "EighteenPlus"))
                {
                    gameNights = repo.GetAllGameNightsInFutureWhereNotOrganizerOrSignedUp(webUser);
                }
                else
                {
                    gameNights =
                        repo.GetAllGameNightsInFutureWhereNotOrganizerOrSignedUpAndGameNightIsNot18Plus(webUser);
                }
            }
            else
            {
                gameNights = repo.GetAllGameNightsInFuture();
            }
            
            

            

            return View(gameNights);
        }

        [Authorize(policy: "Organizer")]
        public IActionResult CreateGameNight()
        {
            return View();
        }

        [HttpPost]
        [Authorize(policy: "Organizer")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateGameNight(GameNight gameNight, string street, string city, string postalCode,
            int houseNumber)
        {
            if (ModelState.IsValid)
            {
                Address address = new Address()
                {
                    Street = street,
                    City = city,
                    PostalCode = postalCode,
                    HouseNumber = houseNumber
                };

                gameNight.Address = address;
                var AppUser = _userManager.GetUserAsync(this.User).Result;
                WebsiteUser user = websiteUserRepo.GetWebsiteUserByEmail(AppUser.Email);
                gameNight.Organizer = user;
                repo.SaveGameNight(gameNight);
                TempData["GameNightId"] = repo.GetMostRecentGameNightOfUserByEmail(AppUser.Email).Id;
                return RedirectToAction("AddGamesToGameNight");
            }

            return View();
        }

        [Authorize(policy: "Organizer")]
        public IActionResult AddGamesToGameNight()
        {
            if (TempData["GameNightId"] != null)
            {
                var gameNight = repo.GetGameNightById(Int32.Parse(TempData["GameNightId"].ToString()));
                ViewBag.GameNight = gameNight;
                var games = repo.GetBoardGamesNotInGameNight(gameNight);
                TempData.Keep();
                return View(games);

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(policy: "Organizer")]
        public IActionResult AddGamesToGameNight(int gameId, int gameNightId)
        {
            
            var gameNight = repo.GetGameNightById(gameNightId);
            repo.AddGameToGameNight(gameNightId, gameId);
            ViewBag.GameNight = gameNight;
            var games = repo.GetBoardGamesNotInGameNight(gameNight);
            return View(games);
        }

        [HttpPost]
        [Authorize(policy: "Organizer")]
        public IActionResult DeleteGameFromGameNight(int gameId, int gameNightId)
        {
            repo.DeleteGameFromGameNight(gameId, gameNightId);
            return RedirectToAction("AddGamesToGameNight");
        }

        [HttpPost]
        [Authorize(policy: "Organizer")]
        public IActionResult DeleteGameNight(int gameNightId, string controller, string action)
        {
            var gameNightToDelete = repo.GetGameNightById(gameNightId);
            if (gameNightToDelete.Participants.Count == 0)
            {
                repo.DeleteGameNight(gameNightToDelete);
            }

            return RedirectToAction(action, controller);
        }

        [HttpGet]
        [Authorize(policy: "Organizer")]
        public IActionResult EditGameNight(int gameNightId)
        {
            var gameNight = repo.GetGameNightById(gameNightId);
            return View(gameNight);
        }

        [HttpPost]
        [Authorize(policy: "Organizer")]
        public IActionResult EditGameNight(GameNight gameNight, string street, string city, string postalCode,
            int houseNumber, int gameNightId)
        {
            var testGameNight = repo.GetGameNightById(gameNightId);
            if (GameNightLogic.CanBeEditted(testGameNight))
            {
                gameNight.Address = GameNightLogic.CreateAddress(street, houseNumber, city, postalCode);
                TempData["GameNightId"] = gameNightId;
                gameNight.Id = gameNightId;
                repo.EditGameNight(gameNight);
                return RedirectToAction("AddGamesToGameNight");
            }

            ModelState.AddModelError("", "Spelavonden kunnen enkel aangepast worden als niemand voor de spelavond is aangemeld");
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Info(int gameNightId)
        {
            var gameNight = repo.GetGameNightById(gameNightId);
            ViewBag.IsSignedUpForGameNight =
                UserLogic.IsSignedUpForGameNight(_userManager.GetUserAsync(this.User).Result.Email, gameNight);
            return View(gameNight);
        }

        public IActionResult GameNightSnackInfo(int gameNightId, int foodId)
        {
            var gameNight = repo.GetGameNightById(gameNightId);
            Food snack = null;
            foreach (var food in gameNight.Food)
            {
                if (food.Id == foodId)
                {
                    snack = food;
                    break;
                }
            }

            return View(snack);
        }

        [Authorize]
        public IActionResult GameNightSignUp()
        {
            if (TempData["GameNightIdForSignup"] != null)
            {
                int gameNightId = Int32.Parse(TempData["GameNightIdForSignup"].ToString());
                var gameNight = repo.GetGameNightById(gameNightId);
                var user = websiteUserRepo.GetWebsiteUserByEmail(_userManager.GetUserAsync(this.User).Result.Email);
                ViewBag.DietRequirementsMet = UserLogic.DietRequirementsMatchGameNight(user, gameNight);
                var food = websiteUserRepo.GetFoodOfUserInGameNight(user.Email,
                    gameNightId);
                ViewBag.Food = food;
                ViewBag.CanSignUp = (food.Count() > 0);
                ViewBag.GameNightId = gameNightId;
                TempData.Keep();
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public IActionResult GameNightSignUp(int gameNightId)
        {
            var user = websiteUserRepo.GetWebsiteUserByEmail(_userManager.GetUserAsync(this.User).Result.Email);
            websiteUserRepo.SignUpGameNight(gameNightId, user);
            return RedirectToAction("Index", "Home");
        }
        //used so i can keep the gameNightId saved in tempdata during signup process
        [HttpGet]
        public IActionResult GameNightSignUpHelper(int gameNightId)
        {
            TempData["GameNightIdForSignup"] = gameNightId;
            return RedirectToAction("GameNightSignUp");
        }
        [HttpPost]
        public IActionResult AddFood(Food food, int gameNightId)
        {
            var user = websiteUserRepo.GetWebsiteUserByEmail(_userManager.GetUserAsync(this.User).Result.Email);
            food.User = user;
            websiteUserRepo.AddFoodOfUserToGameNight(gameNightId, food);
            TempData["GameNightIdForSignup"] = gameNightId;
            return RedirectToAction("GameNightSignUp");
        }
        [HttpPost]
        public IActionResult DeleteSignUp(int gameNightId, string action, string controller)
        {
            var userEmail = _userManager.GetUserAsync(this.User).Result.Email;
            websiteUserRepo.DeleteGameNightSignUp(gameNightId, userEmail);
            return RedirectToAction(action, controller);
        }
    }
}
